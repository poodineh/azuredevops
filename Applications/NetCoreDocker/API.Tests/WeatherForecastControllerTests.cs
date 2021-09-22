using System;
using System.Linq;
using API.Controllers;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace API.Tests
{
    public class WeatherForecastControllerTests
    {

        [Test]
        public void WeatherController_ReturnAsIEnumerable_WithFiveResults()
        {
            // Arrange
            //PlaceHolder for mocking data
            // var mockRepo = new Mock<SomeDepedendency>();
            // mockRepo.Setup(repo => repo.ListAsync())
            //     .ReturnsAsync(GetTestSessions());
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            mockLogger.Setup(logger => logger.IsEnabled(LogLevel.None));
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.GetWeatherForecasts();

            // Assert
            Assert.IsTrue(result.ToList().Count == 5);
        }

        [Test]
        public void WeatherController_WeatherForecastSummary_ReturnsNotStringEmpty()
        {
            //Arrange
            var mockLogger = new Mock<ILogger<WeatherForecastController>>();
            mockLogger.Setup(logger => logger.IsEnabled(LogLevel.None));
            var controller = new WeatherForecastController(mockLogger.Object);

            // Act
            var result = controller.GetWeatherForecasts();

            // Assert
            Assert.IsTrue(!String.IsNullOrEmpty(result.ToList().FirstOrDefault().Summary));
        }
    }
}