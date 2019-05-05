namespace Weather.Logic.ForecastProviders.OpenWeatherMap.Dto
{
    internal class CityResponse
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Country { get; set; }
    }
}