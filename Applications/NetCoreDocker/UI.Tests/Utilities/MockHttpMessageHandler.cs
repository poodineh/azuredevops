using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;

namespace UI.Tests.Utilities
{
    public class MockHttpMessageHandler : HttpMessageHandler
    {
        
        public static HttpMessageHandler GetHttpMessageHandler()
        {
            var httpMessageHandler = new Mock<HttpMessageHandler>();

            // Setup Protected method on HttpMessageHandler mock.
            httpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<string>(),
                    ItExpr.IsAny<CancellationToken>()
                    
                )
                .ReturnsAsync((HttpRequestMessage request, CancellationToken token) =>
                {
                    string mockeddata;
                    using (var reader = File.OpenText(@"./TestData/apiresponse.json"))
                    {
                        mockeddata = reader.ReadToEnd();
                    }
                    var response = new HttpResponseMessage()
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(mockeddata)
                    };
                    return response;
                });
                return httpMessageHandler.Object;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {          
            string mockeddata;
            using (var reader = File.OpenText(@"./TestData/apiresponse.json"))
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