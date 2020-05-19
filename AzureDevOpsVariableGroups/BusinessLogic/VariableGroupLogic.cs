using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System;
using AzureDevOpsVariableGroups.Api;
using AzureDevOpsVariableGroups.Entities;

namespace AzureDevOpsVariableGroups.BusinessLogic
{
    public class VariableGroupLogic : IVariableGroupLogic
    {    
        private readonly IAdoHttpClient _adohttpclient;

        private IConfiguration _configuration;
        private readonly ApiParser _apiparser;
        private readonly PipelineVariables _pipelinevariable;

        public VariableGroupLogic(IAdoHttpClient adohttpclient)
        {
            _adohttpclient = adohttpclient;
            _apiparser = new ApiParser();
            //TODO: Replace IConfiguration build with IOptions<T> later. 
            SetConfigAndEnvironment();
            //Place holder for using environment variables in the future
            //string _secretkey = Environment.GetEnvironmentVariable("SecretKey");    
            //Get SecretKey Key from config
            string _secretkey = _configuration["AppSettings:SecretKey"];
            string _userid = _configuration["AppSettings:UserId"];                     
                     
            //Set PipelineVariable in ctor creation
            _pipelinevariable = new PipelineVariables
            {
               Description = "Pipeline Variables used across templates which includes secret keys",
               Name = "PipelineVariables",
               Type = "Vsts",
               Variables = new Variables {
                   UserId = new VariableGroupProviderData
                   {
                       IsSecret = true,
                       Value = _userid
                   },
                   SecretKey = new VariableGroupProviderData
                   {
                       IsSecret = true,
                       Value = _secretkey
                   }
               }
            };            
        }
        public async Task<KeyValuePair<string,bool>> GetVariableGroup(string teamprojectid)
        {   
            var uri = $"https://dev.azure.com/itsals/{teamprojectid}/_apis/distributedtask/variablegroups?groupname=PipelineVariables&api-version=5.1-preview.1";
            var response = await _adohttpclient.SendRequest(uri,HttpMethod.Get);

            //User Parser to get JOjbects only needed for the work
            var variablegroup = _apiparser.GetVariableGroup(response);
            return variablegroup;
        }

        public async Task<KeyValuePair<string,bool>> AuthorizeVariableGroup(string teamprojectid, string pipelinevariableid)
        {
            var authorizedpipelinevariables = new AuthorizeVariableGroup[1];
            authorizedpipelinevariables[0] = new AuthorizeVariableGroup
            {
                Name = "PipelineVariables",
                Type = "variablegroup",
                Authorized = true,
                Id = pipelinevariableid
            };

            var serializebody = JsonSerializer.Serialize<AuthorizeVariableGroup[]>(authorizedpipelinevariables);
            var uri = $"https://dev.azure.com/itsals/{teamprojectid}/_apis/build/authorizedresources?api-version=5.1-preview.1";
            var response = await _adohttpclient.SendRequest(uri,HttpMethod.Patch,serializebody);
            //User Parser to get JOjbects only needed for the work
            var variablegroup = _apiparser.AuthorizeVariableGroup(response, pipelinevariableid);
            return variablegroup;
        }

        public async Task<KeyValuePair<string,string>> CreateVariableGroup(string teamprojectid)
        {            
            var serializebody = JsonSerializer.Serialize<PipelineVariables>(_pipelinevariable);
            var uri = $"https://dev.azure.com/itsals/{teamprojectid}/_apis/distributedtask/variablegroups?api-version=5.1-preview.1";
            var response = await _adohttpclient.SendRequest(uri,HttpMethod.Post,serializebody);
            //User Parser to get JOjbects only needed for the work
            var variablegroup = _apiparser.CreateVariableGroup(response);
            return variablegroup;
        } 
        public async Task<KeyValuePair<string,string>> UpdateVariableGroup(string teamprojectid, string pipelinevariableid)
        {            
            var serializebody = JsonSerializer.Serialize<PipelineVariables>(_pipelinevariable);
            var uri = $"https://dev.azure.com/itsals/{teamprojectid}/_apis/distributedtask/variablegroups/{pipelinevariableid}?api-version=5.1-preview.1";
            var response = await _adohttpclient.SendRequest(uri,HttpMethod.Put,serializebody);
            //User Parser to get JOjbects only needed for the work
            var variablegroup = _apiparser.UpdateVariableGroup(response);
            return variablegroup;
        } 

        private void SetConfigAndEnvironment () 
        {
             _configuration = new ConfigurationBuilder()
                .SetBasePath (Directory.GetCurrentDirectory ())
                .AddJsonFile ("appsettings.json", optional : false, reloadOnChange : true)
                .Build ();
        }         
    }
}