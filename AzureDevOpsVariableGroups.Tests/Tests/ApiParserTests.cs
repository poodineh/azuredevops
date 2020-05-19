using NUnit.Framework;
using System.IO;
using System;
using System.Linq;
using AzureDevOpsVariableGroups.Api;

namespace AzureDevOpsVariableGroups.Tests
{
    [TestFixture]
    public class ApiParserTests 
    {

        [Test]
        public void GetTeamProjects_ReturnsKvpTeamProjects()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/adogetteamsprojects.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var teamprojects = apiparser.GetTeamProjects(apiresponse);             

            //Assert
            Assert.IsTrue(teamprojects.Count == 2);
        }  

         [Test]
        public void GetTeamProjects_ReturnsTeamProjectIdAndName()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/adogetteamsprojects.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var teamprojects = apiparser.GetTeamProjects(apiresponse); 

            //Assert            
            Assert.IsTrue(teamprojects.FirstOrDefault().Key == "f4dc9323-f761-4c6d-b9e3-40a0b66dde41");
            Assert.IsTrue(teamprojects.FirstOrDefault().Value == "Project1");
        }   

        [Test]
        public void GetVariableGroup_Existing_ReturnsTrue()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/GetVariableGroupResponse.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var pipelinevariable = apiparser.GetVariableGroup(apiresponse);  
            
            //Assert
            Assert.IsTrue(pipelinevariable.Key == "77");        
            Assert.IsTrue(pipelinevariable.Value == true);   
        }  

        [Test]
        public void GetVariableGroup_NotExisting_ReturnsFalse()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/GetVariableGroupDoesNotExistResponse.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var pipelinevariable = apiparser.GetVariableGroup(apiresponse);  
            
            //Assert
            Assert.IsTrue(pipelinevariable.Key == String.Empty);        
            Assert.IsTrue(pipelinevariable.Value == false);   
        }  

        [Test]
        public void AuthorizeVariableGroup_ReturnsIdOfVariableGroup()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/AuthorizeVariableGroupResponse.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var pipelinevariable = apiparser.AuthorizeVariableGroup(apiresponse, "77");  
            Assert.IsTrue(pipelinevariable.Key == "77");        
            Assert.IsTrue(pipelinevariable.Value == true);   
        }   

        
        [Test]
        public void CreateVariableGroup_ReturnsIdOfVariableGroup()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/CreateVariableGroupResponse.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var pipelinevariable = apiparser.CreateVariableGroup(apiresponse);  
            Assert.IsTrue(pipelinevariable.Key == "77"); 
        }     

        [Test]
        public void CreateVariableGroup_ReturnsIdOfTeamProjectAsValue()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/CreateVariableGroupResponse.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var pipelinevariable = apiparser.CreateVariableGroup(apiresponse);  
            Assert.IsTrue(pipelinevariable.Value == "f4dc9323-f761-4c6d-b9e3-40a0b66dde41"); 
        }
         
        [Test]
        public void UpdateVariableGroup_ReturnsIdOfVariableGroup()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/UpdateVariableGroupResponse.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var pipelinevariable = apiparser.UpdateVariableGroup(apiresponse);  
            Assert.IsTrue(pipelinevariable.Key == "77"); 
        }     

        [Test]
        public void UpdateVariableGroup_ReturnsIdOfTeamProjectAsValue()
        {
            //Arrange
            string apiresponse = String.Empty;
            using (var reader = File.OpenText(@"./TestData/UpdateVariableGroupResponse.json"))
            {
                apiresponse = reader.ReadToEnd();
            };
            var apiparser = new ApiParser();   

            //Act
            var pipelinevariable = apiparser.UpdateVariableGroup(apiresponse);  
            Assert.IsTrue(pipelinevariable.Value == "f4dc9323-f761-4c6d-b9e3-40a0b66dde41"); 
        }         
    }
}