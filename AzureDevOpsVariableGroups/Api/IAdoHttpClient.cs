using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace  AzureDevOpsVariableGroups.Api
{
    public interface IAdoHttpClient
    {
         Task<String> SendRequest(string requesturi, HttpMethod httpMethod, String serializedpostbody = null);
    }
}