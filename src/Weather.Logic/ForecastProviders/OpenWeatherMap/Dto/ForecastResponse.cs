using System.Collections.Generic;
using Newtonsoft.Json;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap.Dto
{
    internal class ForecastResponse
    {
        [JsonProperty("dt")]
        public long UnixTime { get; set; }

        public ConditionsResponse Main { get; set; }

        public IReadOnlyCollection<WeatherResponse> Weather { get; set; }

        [JsonProperty("dt_txt")]
        public string TimeStampString { get; set; }
    }
}