using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AzureDevOpsVariableGroups.Api;

namespace AzureDevOpsVariableGroups.BusinessLogic
{
    public class AdoTeamProjectsLogic
    {    
        private readonly IAdoHttpClient _adohttpclient;
        private ApiParser _apiparser;

        public AdoTeamProjectsLogic(IAdoHttpClient adohttpclient)
        {
            _adohttpclient = adohttpclient;
            _apiparser = new ApiParser();
        }
        //Busines Logic Requirements: 
        //1) Get All ADO Team Projects
        public async Task<List<KeyValuePair<string,string>>> GetTeamProjects()
        {   
            var uri = "https://dev.azure.com/itsals/_apis/projects?api-version=5.1&$top=200";
            var response = await _adohttpclient.SendRequest(uri,HttpMethod.Get);

            //User Parser to get JOjbects only needed for the work
            var teamprojects = _apiparser.GetTeamProjects(response);
            return teamprojects;
        }        
    }
}