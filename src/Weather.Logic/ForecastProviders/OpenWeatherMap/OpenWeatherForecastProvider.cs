using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weather.Domain.Weather;
using Weather.Logic.ForecastProviders.OpenWeatherMap.Dto;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap
{
    public class OpenWeatherForecastProvider : IForecastProvider
    {
        private readonly IOpenWeatherHttpClient _httpClient;
        public OpenWeatherForecastProvider(IOpenWeatherHttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }
        
        public async Task<Forecast> Get5DaysForecastAsync(string city, CountryCode countryCode)
        {
            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(city));
            }

            StringBuilder paramValueBuilder = new StringBuilder(city);
            if (!string.IsNullOrWhiteSpace(countryCode.Value))
            {
                paramValueBuilder.Append($",{countryCode.Value}");
            }

            return await QueryAndConvertAsync(("q", paramValueBuilder.ToString()));
        }

        public async Task<Forecast> Get5DaysForecastAsync(ZipCode zipCode, CountryCode countryCode)
        {
            if (countryCode == CountryCode.Empty)
            {
                throw new ArgumentException("Country code is required");
            }
            
            return await QueryAndConvertAsync(("zip", $"{zipCode.Code},{countryCode.Value}"));
        }

        public async Task<Forecast> Get5DaysForecastAsync(int cityId)
        {
            if (cityId <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(cityId));
            }
            
            return await QueryAndConvertAsync(("id", cityId.ToString()));
        }

        private async Task<Forecast> QueryAndConvertAsync(params (string name, string value)[] requestParameters)
        {
            try
            {
                OpenWeatherMapResponse responseDto = await _httpClient.SendGetRequest<OpenWeatherMapResponse>(requestParameters);

                if (responseDto == null)
                {
                    throw new ForecastException("Weather information unavailable");
                }
                
                return ConvertResponse(responseDto);
            }
            catch (RequestFailedException e)
            {
                throw new ForecastException("Weather information unavailable", e);
            }
        }

        private static Forecast ConvertResponse(OpenWeatherMapResponse response)
        {
            var dayForecasts = response.List.GroupBy(item => ConvertUnixTimeToDateTime(item.UnixTime).Date)
                .Select(gr => ToDayForecast(gr.Key, gr))
                .ToList();

            var firstDayForecast = dayForecasts.First();
            dayForecasts.RemoveAt(0);

            DayForecast currentWeather = new DayForecast(firstDayForecast.DateTime, firstDayForecast.Temperature,
                firstDayForecast.Humidity, firstDayForecast.WindSpeed,
                response.List.First().Weather.Select(w => new WeatherInfo(w.Id, w.Main, w.Description)).First());

            return new Forecast(
                new City(response.City.Id, response.City.Name, new CountryCode(response.City.Country)), 
                currentWeather,
                dayForecasts);
        }

        private static DayForecast ToDayForecast(DateTime date, IEnumerable<ForecastResponse> forecastItems)
        {
            var weather = forecastItems.First().Weather.First();
            
            return new DayForecast(date, 
                forecastItems.Average(forecast => forecast.Main.Temperature),
                forecastItems.Average(forecast => forecast.Main.Humidity),
                forecastItems.Average(forecast => forecast.Wind.Speed),
                new WeatherInfo(weather.Id, weather.Main, weather.Description));
        }

        private static DateTime ConvertUnixTimeToDateTime(long unixTime)
        {
            // Unix timestamp is seconds past epoch
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTime);
            return dtDateTime;
        }
    }
}