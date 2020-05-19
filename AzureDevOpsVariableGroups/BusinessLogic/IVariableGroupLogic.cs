using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace  AzureDevOpsVariableGroups.BusinessLogic
{
    public interface IVariableGroupLogic
    {
        Task<KeyValuePair<string,bool>> GetVariableGroup(string teamprojectid);
        
        Task<KeyValuePair<string,bool>> AuthorizeVariableGroup(string teamprojectid, string pipelinevariableid);
      
        Task<KeyValuePair<string,string>> CreateVariableGroup(string teamprojectid);
      
        Task<KeyValuePair<string,string>> UpdateVariableGroup(string teamprojectid, string pipelinevariableid);
         
    }
}