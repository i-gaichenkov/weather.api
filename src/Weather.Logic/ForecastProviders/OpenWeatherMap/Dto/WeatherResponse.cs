using Weather.Domain.Weather;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap.Dto
{
    internal class WeatherResponse
    {
        /// <summary>
        /// Weather condition id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Group of weather parameters (Rain, Snow, Extreme etc.)
        /// </summary>
        public WeatherCondition Main { get; set; }

        /// <summary>
        /// Weather condition within the group
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Weather icon id
        /// </summary>
        public string Icon { get; set; }
    }
}