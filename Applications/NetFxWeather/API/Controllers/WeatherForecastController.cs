using AlaskaAir.Demo.CloudFramework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AlaskaAir.Demo.CloudFramework.Controllers
{
    public class WeatherForecastController : ApiController
    {
        public WeatherForecastController()
        {
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get(int NumberOfForecasts)
        {
            return WeatherForecastHelper.GenerateWeatherForecast(NumberOfForecasts);
        }


    }
}
