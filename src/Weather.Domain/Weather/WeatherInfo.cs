using System;

namespace Weather.Domain.Weather
{
    public class WeatherInfo
    {
        public WeatherCondition WeatherCondition { get; }

        public string WeatherDescription { get; }

        public WeatherInfo(WeatherCondition weatherCondition, string weatherDescription)
        {
            WeatherCondition = weatherCondition;
            WeatherDescription = weatherDescription ?? String.Empty;
        }
    }
}