using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using ui;
using UI.Tests.Utilities;

namespace UI.Tests
{
    public class IndexPageIntegrationTests
    {

        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public async Task IndexPage_Navigation_ReturnsAtLeastOneForecast()
        {
            // Arrange
            //Mock HttpClient
            var mockwebfactory = new MockWebApplicationFactory<Startup>();
            
            //Act
            var client = mockwebfactory.CreateClient();
            var response = await client.GetAsync("/");
            var htmldocument = await HtmlHelpers.GetDocumentAsync(response);
            var weatherforecastfromhtml = htmldocument.All.FirstOrDefault(element => element.LocalName == "dt").OuterHtml;


            // Assert
            Assert.IsTrue(!String.IsNullOrEmpty(weatherforecastfromhtml));         
        }

         [Test]
        public async Task IndexPage_Forecast_ReturnsDateTimeAsDT()
        {
            // Arrange
            //Mock HttpClient
            var mockwebfactory = new MockWebApplicationFactory<Startup>();
            
            //Act
            var client = mockwebfactory.CreateClient();
            var response = await client.GetAsync("/");
            var htmldocument = await HtmlHelpers.GetDocumentAsync(response);
            var weatherforecastfromhtml = htmldocument.All.FirstOrDefault(element => element.LocalName == "dt").OuterHtml;

            // Assert
            Assert.IsTrue(weatherforecastfromhtml == "<dt>1/8/2020</dt>");         
        }
    }
}