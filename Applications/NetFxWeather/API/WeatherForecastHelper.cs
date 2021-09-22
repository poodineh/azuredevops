using AlaskaAir.Demo.CloudFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlaskaAir.Demo.CloudFramework
{
    public class WeatherForecastHelper
    {
        public static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public static IEnumerable<WeatherForecast> GenerateWeatherForecast(int NumberOfForecasts)
        {
            var rng = new Random();
            return Enumerable.Range(1, NumberOfForecasts).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}