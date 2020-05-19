using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AzureDevOpsVariableGroups.Api
{
    public class AdoHttpClient : IAdoHttpClient
    {
        private readonly IHttpClientFactory _httpclientfactory;
        private IConfiguration _configuration;
        public AdoHttpClient(IHttpClientFactory httpclientfactory)
        {
            _httpclientfactory = httpclientfactory;
            SetConfigAndEnvironment();
        }
        public async Task<String> SendRequest(string requesturi, HttpMethod httpMethod, String serializedpostbody = null)
        {
            try
            {
                //Placeholder for using Environment Variables
                //string _azuredevopspat = Environment.GetEnvironmentVariable("AzureDevOpsPAT");
                string _azuredevopspat = _configuration["AppSettings:AzureDevOpsPAT"]; 
                var requestMessage = new HttpRequestMessage
                {
                    Method = httpMethod,
                    RequestUri = new Uri(requesturi),
                    Content = serializedpostbody != null
                        ? new StringContent(serializedpostbody, Encoding.UTF8, "Application/Json")
                        : null
                };
                var httpclient = _httpclientfactory.CreateClient();
                httpclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/Json"));
                httpclient.DefaultRequestHeaders.Add("User-Agent", "AdoRequest");
                httpclient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic",
                    Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes($"{string.Empty}:{_azuredevopspat}")));
                var response = httpclient.SendAsync(requestMessage);
                return await EnsureSuccessStatusCodeAsync(response.Result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task<string> EnsureSuccessStatusCodeAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Response Error: Status Code: {response.StatusCode}");               
            } 
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