using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Weather.Domain.Weather;
using Weather.Logic.ForecastProviders;

namespace Weather.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IForecastProvider _forecastProvider;

        public WeatherController(IForecastProvider forecastProvider)
        {
            _forecastProvider = forecastProvider;
        }

        [HttpGet("city")]
        public async Task<Forecast> GetForecastByCityName([FromQuery] string cityName,
            [FromQuery] string countryCode)
        {
            return await _forecastProvider.Get5DaysForecastAsync(cityName, new CountryCode(countryCode));
        }
    }
}