using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using ui;
using ui.Pages;
using UI.Tests.Utilities;

namespace UI.Tests
{
    public class IndexPageTests
    {

        private WebApplicationFactory<Startup> _factory;
        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [Test]
        public async Task IndexPage_Navigation_DisplaysWeatherForecasts()
        {
            // Arrange
            //Mock HttpClient
            var mockhttpclientfactory = new Mock<IHttpClientFactory>();
            var httpclient = new HttpClient(new MockHttpMessageHandler())
            {
                BaseAddress = new Uri("http://localhostmock:8080/")
            };
            mockhttpclientfactory.Setup( mockfactory => mockfactory.CreateClient(It.IsAny<string>())).Returns(httpclient);

            //Mock Logger
            var mockLogger = new Mock<ILogger<IndexModel>>();
            mockLogger.Setup(logger => logger.IsEnabled(LogLevel.None));

            //Act
            var pagemodel = new IndexModel(mockLogger.Object, mockhttpclientfactory.Object);
            await pagemodel.OnGet();                      
            
            // Assert
            Assert.IsTrue(pagemodel.WeatherForecasts.Count() == 5);         
        }

        [Test]
        public async Task IndexPage_ForeCastSummaryResult_ReturnsValidResponse()
        {
            // Arrange
            //Mock HttpClient
            var mockhttpclientfactory = new Mock<IHttpClientFactory>();
            var httpclient = new HttpClient(new MockHttpMessageHandler())
            {
                BaseAddress = new Uri("http://localhostmock:8080/")
            };
            mockhttpclientfactory.Setup( mockfactory => mockfactory.CreateClient(It.IsAny<string>())).Returns(httpclient);

            //Mock Logger
            var mockLogger = new Mock<ILogger<IndexModel>>();
            mockLogger.Setup(logger => logger.IsEnabled(LogLevel.None));

            //Act
            var pagemodel = new IndexModel(mockLogger.Object, mockhttpclientfactory.Object);
            await pagemodel.OnGet();                      
            
            // Assert
            Assert.IsTrue(pagemodel.WeatherForecasts.FirstOrDefault().Summary == "Hot");         
        }
    }
}