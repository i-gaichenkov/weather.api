using System;

namespace Weather.Domain.Weather
{
    public class WeatherInfo
    {
        public int Id { get; }
        
        public WeatherCondition WeatherCondition { get; }

        public string WeatherDescription { get; }

        public WeatherInfo(int id, WeatherCondition weatherCondition, string weatherDescription)
        {
            Id = id;
            WeatherCondition = weatherCondition;
            WeatherDescription = weatherDescription ?? String.Empty;
        }
    }
}