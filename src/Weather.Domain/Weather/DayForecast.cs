using System;
using System.Collections.Generic;

namespace Weather.Domain.Weather
{
    public class DayForecast
    {
        public DateTime DateTime { get; }
        
        public double Temperature { get; }

        public int Humidity { get; }

        public IReadOnlyCollection<WeatherInfo> Weather { get; }

        public DayForecast(DateTime dateTime, double temperature, int humidity, IReadOnlyCollection<WeatherInfo> weather)
        {
            if (humidity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(humidity));
            }
            
            DateTime = dateTime;
            Temperature = temperature;
            Humidity = humidity;
            Weather = weather ?? throw new ArgumentNullException(nameof(weather));
        }
    }
}