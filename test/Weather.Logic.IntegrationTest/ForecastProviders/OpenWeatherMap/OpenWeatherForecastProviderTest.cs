using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Weather.Domain.Weather;
using Weather.Logic.ForecastProviders.OpenWeatherMap;
using Xunit;

namespace Weather.Logic.IntegrationTest.ForecastProviders.OpenWeatherMap
{
    public class OpenWeatherForecastProviderTest : IClassFixture<OpenWeatherTestFixture>
    {
        private string ApiKey => _openWeatherTestFixture.Configuration["ApiKey"];
        private const int MunichCityId = 160004251;

        private readonly OpenWeatherTestFixture _openWeatherTestFixture;

        public OpenWeatherForecastProviderTest(OpenWeatherTestFixture testFixture)
        {
            _openWeatherTestFixture = testFixture;
        }

        [Fact]
        public async Task Get5DaysForecastAsync_ByCityName_ReturnsForecast()
        {
            // Arrange
            OpenWeatherForecastProvider provider = CreateOpenWeatherForecastProvider();

            // Act
            var forecast = await provider.Get5DaysForecastAsync("London", new CountryCode("GB"));
            
            // Assert
            Assert.Equal(2643743, forecast.City.Id);
            Assert.Equal("London", forecast.City.Name);
            Assert.Equal("GB", forecast.City.CountryCode.Value);
            Assert.NotEmpty(forecast.DailyForecasts);
            Assert.NotNull(forecast.CurrentWeather);
            Assert.Equal(4, forecast.DailyForecasts.Count);
        }

        [Fact]
        public async Task Get5DaysForecastAsync_ByZipCode_ReturnsForecast()
        {
            // Arrange
            OpenWeatherForecastProvider provider = CreateOpenWeatherForecastProvider();

            // Act
            var forecast = await provider.Get5DaysForecastAsync(new ZipCode("81827"), new CountryCode("DE"));
            
            // Assert
            Assert.Equal(MunichCityId, forecast.City.Id);
            Assert.Equal("Munich", forecast.City.Name);
            Assert.Equal("DE", forecast.City.CountryCode.Value);
            Assert.NotEmpty(forecast.DailyForecasts);
            Assert.NotNull(forecast.CurrentWeather);
            Assert.Equal(4, forecast.DailyForecasts.Count);
        }
        
        [Fact]
        public async Task Get5DaysForecastAsync_ByCityId_ReturnsForecast()
        {
            // Arrange
            OpenWeatherForecastProvider provider = CreateOpenWeatherForecastProvider();

            // Act
            var forecast = await provider.Get5DaysForecastAsync(MunichCityId);
            
            // Assert
            Assert.Equal(MunichCityId, forecast.City.Id);
            Assert.Equal("Munich", forecast.City.Name);
            Assert.Equal("DE", forecast.City.CountryCode.Value);
            Assert.NotEmpty(forecast.DailyForecasts);
            Assert.NotNull(forecast.CurrentWeather);
            Assert.Equal(4, forecast.DailyForecasts.Count);
        }

        private OpenWeatherForecastProvider CreateOpenWeatherForecastProvider()
        {
            var options = Options.Create(new OpenWeatherOptions()
            {
                ApiKey = ApiKey,
                Endpoint = "https://api.openweathermap.org",
                Forecast5RelPath = "/data/2.5/forecast"
            });

            var provider = new OpenWeatherForecastProvider(new OpenWeatherHttpClient(options));
            return provider;
        }
    }
}