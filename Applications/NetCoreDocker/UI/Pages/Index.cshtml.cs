using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ui.Entities;

namespace ui.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IHttpClientFactory _httpclientfactory;

        public IEnumerable<WeatherForecast> WeatherForecasts;

        public IndexModel(ILogger<IndexModel> logger, IHttpClientFactory httpclientfactory)
        {
            _logger = logger;
            _httpclientfactory = httpclientfactory;
        }
        public async Task OnGet()
        {
            //Get Data From API
            var httpclient = _httpclientfactory.CreateClient("api");
            var response = await httpclient.GetAsync("api/weatherforecast");
            var stringcontent = await response.Content.ReadAsStringAsync();
            _logger.LogInformation("API Response", stringcontent);
            WeatherForecasts = JsonConvert.DeserializeObject<IEnumerable<WeatherForecast>>(stringcontent);
        }
    }
}
