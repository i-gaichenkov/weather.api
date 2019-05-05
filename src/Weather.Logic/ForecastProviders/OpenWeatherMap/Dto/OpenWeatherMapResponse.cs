using System.Collections.Generic;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap.Dto
{
    internal class OpenWeatherMapResponse
    {
        public CityResponse City { get; set; }

        public IEnumerable<ForecastResponse> List { get; set; }
    }
}