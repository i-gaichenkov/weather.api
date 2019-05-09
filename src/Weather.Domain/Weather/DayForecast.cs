using System;
using System.Collections.Generic;

namespace Weather.Domain.Weather
{
    public class DayForecast
    {
        public DateTime DateTime { get; }
        
        public double Temperature { get; }

        public double Humidity { get; }

        public double WindSpeed { get; }
        
        public WeatherInfo Weather { get; }

        public DayForecast(DateTime dateTime, double temperature, double humidity, double windSpeed, WeatherInfo weather)
        {
            if (humidity < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(humidity));
            }

            if (windSpeed < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(windSpeed));
            }

            DateTime = dateTime;
            Temperature = temperature;
            Humidity = humidity;
            WindSpeed = windSpeed;
            Weather = weather ?? throw new ArgumentNullException(nameof(weather));
        }
    }
}