using System;
using System.Collections.Generic;

namespace Weather.Domain.Weather
{
    public class Forecast
    {
        public City City { get; }

        public DayForecast CurrentWeather { get; set; }
        
        public IReadOnlyCollection<DayForecast> DailyForecasts { get; }

        public Forecast(City city, DayForecast currentWeather, IReadOnlyCollection<DayForecast> dailyForecasts)
        {
            City = city ?? throw new ArgumentNullException(nameof(city));
            DailyForecasts = dailyForecasts ?? throw new ArgumentNullException(nameof(dailyForecasts));
            CurrentWeather = currentWeather ?? throw new ArgumentNullException(nameof(currentWeather));
        }
    }
}