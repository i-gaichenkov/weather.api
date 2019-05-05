using Newtonsoft.Json;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap.Dto
{
    internal class ConditionsResponse
    {
        [JsonProperty("temp")]
        public double Temperature { get; set; }

        public int Humidity { get; set; }
    }
}