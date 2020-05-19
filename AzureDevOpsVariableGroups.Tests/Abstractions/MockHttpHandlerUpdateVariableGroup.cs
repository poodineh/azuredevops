using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsVariableGroups.Tests.Abstractions
{
    public class MockHttpHandlerUpdateVariableGroup : HttpMessageHandler
    {
        protected override  async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            string mockeddata;
            using (var reader = File.OpenText(@"./TestData/UpdateVariableGroupResponse.json"))
            {
                mockeddata = reader.ReadToEnd();
            }
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(mockeddata)
            };
            await Task.Delay(1).ConfigureAwait(continueOnCapturedContext: false); //Simulate the request time slightly
            return response;
        }
    }
}