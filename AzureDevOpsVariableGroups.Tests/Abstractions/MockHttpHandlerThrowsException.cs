using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AzureDevOpsVariableGroups.Tests.Abstractions
{
    public class MockHttpHandlerThrowsException : HttpMessageHandler
    {
        protected override  async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("Error From Mocked Service")
            };
            await Task.Delay(1).ConfigureAwait(continueOnCapturedContext: false); //Simulate the request time slightly
            return response;
        }
    }
}