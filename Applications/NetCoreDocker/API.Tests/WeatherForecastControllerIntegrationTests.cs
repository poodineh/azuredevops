using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using NUnit.Framework;

namespace API.Tests
{
    public class WeatherForecastControllerIntegrationTests
    {
        private WebApplicationFactory<Startup> _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [Test]
        public async Task WeatherControllerAsSut_ReturnContentType_AsApplicationJson()
        {        
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("http://localhost:8080/api/weatherforecast");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.IsTrue(response.Content.Headers.ContentType.ToString() == "application/json; charset=utf-8");
        }

        [Test]
        public async Task WeatherControllerAsSut_ReturnsWeatherForecasts_WithFiveResults()
        {        
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("http://localhost:8080/api/weatherforecast");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var results = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<WeatherForecast>>(results);
            Assert.IsTrue(data.Count == 5);
        }
    }
}