using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.IO;
using System;
using AzureDevOpsVariableGroups.Api;
using AzureDevOpsVariableGroups.Tests.Abstractions;

namespace AzureDevOpsVariableGroups.Tests
{
    [TestFixture]
    public class AdoHttpClientTests 
    {

        [Test]
        public  void CreateDockerServiceConnection_ReturnsOk()
        {
            //Act
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(new MockHttpHandlerCreateVariableGroup());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            string mockedrequestbody = String.Empty;
            using (var reader = File.OpenText(@"./TestData/CreateVariableGroupRequest.json"))
            {
                mockedrequestbody = reader.ReadToEnd();
            };
            string response = String.Empty;

            //Act and Assert
            Assert.DoesNotThrowAsync(async () => 
               response =  await adohttpclient.SendRequest("https://dev.azure.com/dondeetan/40a5c8c6-58ba-4a61-a160-344e2e432345/_apis/distributedtask/variablegroups?api-version=5.1-preview.1", HttpMethod.Patch, mockedrequestbody));       
        }
        
        [Test]
        public  void CreateDockerSerivceEndpoints_ThrowsException()
        {
            //Act
            var mockFactory = new Mock<IHttpClientFactory>();
            //Intentionally return a 400 response which yields not OK response
            var client = new HttpClient(new MockHttpHandlerThrowsException());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            string mockedrequestbody = String.Empty;
            using (var reader = File.OpenText(@"./TestData/CreateVariableGroupRequest.json"))
            {
                mockedrequestbody = reader.ReadToEnd();
            };

            //Act and Assert
            Assert.ThrowsAsync<HttpRequestException>(async () => await adohttpclient.SendRequest("https://dev.azure.com/itsals/40a5c8c6-58ba-4a61-a160-344e2e432345/_apis/distributedtask/variablegroups?api-version=5.1-preview.1", HttpMethod.Patch, mockedrequestbody));             
        }

        [Test]
        public void GetTeamProjects_ReturnsOk()
        {
            //Arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(new MockHttpHandlerGetTeamProjects());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            string mockedrequestbody = String.Empty;
            string response = String.Empty;

            //Act
            Assert.DoesNotThrowAsync(async () => 
               response =  await adohttpclient.SendRequest("https://dev.azure.com/itsals/_apis/projects?api-version=5.1&$top=200", HttpMethod.Get));     

            //Assert
            Assert.IsTrue(response.Contains("f4dc9323-f761-4c6d-b9e3-40a0b66dde41"));
        } 
    }
}