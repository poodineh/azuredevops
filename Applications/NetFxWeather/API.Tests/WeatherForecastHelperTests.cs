using NUnit.Framework;
using System;
using System.Linq;

namespace AlaskaAir.Demo.CloudFramework.Tests
{
    [TestFixture]
    public class WeatherForecastHelperTests
    {
        public string[] WeatherSummaries;

        [SetUp]
        public void Init()
        {
            WeatherSummaries = WeatherForecastHelper.Summaries;
        }

        [Test]
        [Category("SampleTest")]
        [TestCase(1)]
        [TestCase(4)]
        [TestCase(6)]
        public void GenerateWeatherForecastReturnsRequestedForcasts(int requestNumberOfForecasts)
        {
            //Arrange

            //Act
            var weather = WeatherForecastHelper.GenerateWeatherForecast(requestNumberOfForecasts);

            //Assert
            Assert.IsTrue(weather.ToList().Count == requestNumberOfForecasts);
        }

        [Test]
        [Category("SampleTest")]
        [TestCase(5)]
        public void GenerateWeatherForecastReturnsExpectedOptions(int requestNumberOfForecasts)
        {
            //Arrange

            //Act
            var weather = WeatherForecastHelper.GenerateWeatherForecast(requestNumberOfForecasts);

            //Assert
            Assert.IsNotNull(weather);

            var missingWeatherPatterns = weather.Select(x => x.Summary).Except(WeatherSummaries);
            Assert.IsTrue(!missingWeatherPatterns.Any());
        }
    }
}
