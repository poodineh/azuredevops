using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AzureDevOpsVariableGroups.Api
{
    public class ApiParser
    {
        public List<KeyValuePair<string,string>> GetTeamProjects(string apiresponse)
        {
            var parsedapiresponse = JObject.Parse(apiresponse);
            var teamprojects = (JArray)parsedapiresponse["value"];
            var projectswithid = new List<KeyValuePair<string,string>>();
            foreach (JObject teamproject in teamprojects)
            {
                var kvp = new KeyValuePair<string,string>
                (
                    (string)teamproject.GetValue("id"),
                    (string)teamproject.GetValue("name")
                );
                projectswithid.Add(kvp);
            }
            return projectswithid;            
        }

        public KeyValuePair<string,bool> GetVariableGroup(string apiresponse)
        {
            var parsedapiresponse = JObject.Parse(apiresponse);
            //Note: We only Query the hardCoded name of PipelineVariables. Thus, the count will either be 1 or 0. Direct cast is sufficient
            var pipelinevariableexist = (bool)parsedapiresponse["count"];
            KeyValuePair<string,bool> kvp;
            if (pipelinevariableexist)
            {
                var pipelinevariableinfo = (JArray)parsedapiresponse["value"];                
                //There's only 1 value in the array of name PipelineVariables
                var entry = (JObject)pipelinevariableinfo.First;                
                kvp = new KeyValuePair<string, bool>((string)entry.GetValue("id"),true);
            }
            else
                kvp = new KeyValuePair<string, bool>(String.Empty,false);
            return kvp;            
        }

        public KeyValuePair<string,bool> AuthorizeVariableGroup(string apiresponse, string pipelinevariableid)
        {
            var parsedapiresponse = JObject.Parse(apiresponse);
            //Authorized resources should at least be greater than 1
            var authorizedsourcescount = (int)parsedapiresponse["count"];
            KeyValuePair<string,bool> kvp;
            if (authorizedsourcescount > 0)
            {
                //Query JArray with type variablegroup and specific pipelinevariableid. This ensures its truly authorized
                var variablegroups = (JArray)parsedapiresponse["value"];
                var pipelevariable = (JToken)variablegroups.FirstOrDefault(resource => resource.Value<string>("type") == "variablegroup" && resource.Value<string>("id") == pipelinevariableid);
                kvp = new KeyValuePair<string, bool>((string)pipelevariable["id"],true);
            }
            else
                kvp = new KeyValuePair<string, bool>(String.Empty,false);
            return kvp;                
        }

        
        public KeyValuePair<string,string> CreateVariableGroup(string apiresponse)
        {
            var parsedapiresponse = JObject.Parse(apiresponse);
            var parsedid = (string)parsedapiresponse["id"];
            var parsedteamprojectid = (string)parsedapiresponse["variableGroupProjectReferences"][0]["projectReference"]["id"];    
            KeyValuePair<string,string> kvp = new KeyValuePair<string, string>(parsedid,parsedteamprojectid);
            return kvp;                
        }

        public KeyValuePair<string,string> UpdateVariableGroup(string apiresponse)
        {
            var parsedapiresponse = JObject.Parse(apiresponse);
            var parsedid = (string)parsedapiresponse["id"];
            var parsedteamprojectid = (string)parsedapiresponse["variableGroupProjectReferences"][0]["projectReference"]["id"];    
            KeyValuePair<string,string> kvp = new KeyValuePair<string, string>(parsedid,parsedteamprojectid);
            return kvp;                
        }
    }
}
