namespace Weather.Logic.ForecastProviders.OpenWeatherMap
{
    public class OpenWeatherOptions
    {
        public string Endpoint { get; set; }
        public string ApiKey { get; set; }
        public string Forecast5RelPath { get; set; }
    }
}