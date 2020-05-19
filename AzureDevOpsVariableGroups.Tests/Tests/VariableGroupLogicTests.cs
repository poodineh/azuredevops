using NUnit.Framework;
using Moq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using AzureDevOpsVariableGroups.Tests.Abstractions;
using AzureDevOpsVariableGroups.Api;
using AzureDevOpsVariableGroups.BusinessLogic;

namespace VariableGroupLogicTests.Tests
{
    [TestFixture]
    public class VariableGroupLogicTests 
    {
        
        [Test]
        public async Task BusinessLogic_GetTeamProjects_ReturnsOk()
        {
            //Act
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(new MockHttpHandlerGetTeamProjects());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            var adoteamprojectslogic = new AdoTeamProjectsLogic(adohttpclient);

            //Arrange
            var teamprojects = await adoteamprojectslogic.GetTeamProjects();

            //Act
            Assert.IsTrue(teamprojects.FirstOrDefault().Key == "f4dc9323-f761-4c6d-b9e3-40a0b66dde41");
            Assert.IsTrue(teamprojects.FirstOrDefault().Value == "Project1");       
        }        

        [Test]
        public async Task BusinessLogic_GetVariableGroup_ReturnsOk()
        {
            //Arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(new MockHttpHandlerGetExistingVariableGroup());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            IVariableGroupLogic variablegrouplogic = new VariableGroupLogic(adohttpclient);

            //Act
            //Go through the team projects
            var existingpipelinevariables = await variablegrouplogic.GetVariableGroup("faketeamprojecid0000");

            //Assert
            Assert.IsTrue(existingpipelinevariables.Key == "77");
            Assert.IsTrue(existingpipelinevariables.Value == true);
        } 

        [Test]
        public async Task BusinessLogic_AuthorizeVariableGroup_ReturnsOk()
        {
            //Arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(new MockHttpHandlerAuthorizeVariableGroup());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            IVariableGroupLogic variablegrouplogic = new VariableGroupLogic(adohttpclient);

            //Act
            //Patch service docker registry so it's authorized by all pipelines
            var existingpipelinevariables = await variablegrouplogic.AuthorizeVariableGroup("faketeamprojecid0000","77");

            //Assert
            Assert.IsTrue(existingpipelinevariables.Key == "77");
            Assert.IsTrue(existingpipelinevariables.Value == true);
        } 

        [Test]
        public async Task BusinessLogic_CreateVariableGroup_ReturnsOk()
        {
            //Arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(new MockHttpHandlerCreateVariableGroup());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            IVariableGroupLogic variablegrouplogic = new VariableGroupLogic(adohttpclient);

            //Act
            //Patch service docker registry so it's authorized by all pipelines
            var existingpipelinevariables = await variablegrouplogic.CreateVariableGroup("faketeamprojecid0000");

            //Assert
            Assert.IsTrue(existingpipelinevariables.Key == "77");
            Assert.IsTrue(existingpipelinevariables.Value == "f4dc9323-f761-4c6d-b9e3-40a0b66dde41");
        }


        [Test]
        public async Task BusinessLogic_UpdateVariableGroup_ReturnsOk()
        {
            //Arrange
            var mockFactory = new Mock<IHttpClientFactory>();
            var client = new HttpClient(new MockHttpHandlerUpdateVariableGroup());
            mockFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            IHttpClientFactory factory = mockFactory.Object;
            var adohttpclient = new AdoHttpClient(factory);
            IVariableGroupLogic variablegrouplogic = new VariableGroupLogic(adohttpclient);

            //Act
            //Patch service docker registry so it's authorized by all pipelines
            var existingpipelinevariables = await variablegrouplogic.UpdateVariableGroup("faketeamprojecid0000","77");

            //Assert
            Assert.IsTrue(existingpipelinevariables.Key == "77");
            Assert.IsTrue(existingpipelinevariables.Value == "f4dc9323-f761-4c6d-b9e3-40a0b66dde41");            
        }
    }
}