using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Moq;
using Newtonsoft.Json;
using Weather.Domain.Weather;
using Weather.Logic.ForecastProviders;
using Weather.Logic.ForecastProviders.OpenWeatherMap;
using Weather.Logic.ForecastProviders.OpenWeatherMap.Dto;
using Xunit;

namespace Weather.Logic.Test.ForecastProviders.OpenWeatherMap
{
    public class OpenWeatherForecastProviderTest
    {
        [Fact]
        public async Task Get5DaysForecastAsync_ByCityName_ReturnsForecast()
        {
            // Arrange
            OpenWeatherForecastProvider provider = CreateOpenWeatherForecastProvider();

            // Act
            var forecast = await provider.Get5DaysForecastAsync("London", new CountryCode("GB"));
            
            // Assert
            Assert.Equal(6940463, forecast.City.Id);
            Assert.Equal("Altstadt", forecast.City.Name);
            Assert.Equal("none", forecast.City.CountryCode.Value);
            Assert.NotEmpty(forecast.DailyForecasts);
            
            Assert.Equal(new DateTime(2017, 02, 16, 12, 00, 00, DateTimeKind.Utc), forecast.DailyForecasts.First().DateTime);
            Assert.Equal(75, forecast.DailyForecasts.First().Humidity);
            Assert.Equal(286.67, forecast.DailyForecasts.First().Temperature);
        }

        [Fact]
        public async Task Get5DaysForecastAsync_ServiceUnavailable_ThrowsException()
        {
            // Arrange
            var clientMock = new Mock<IOpenWeatherHttpClient>();
            var requestFailedException = new RequestFailedException("Service unavailable", "500 - Internal Server Error");
            
            clientMock.Setup(m => m.SendGetRequest<OpenWeatherMapResponse>(It.IsAny<(string, string)>()))
                .ThrowsAsync(requestFailedException);
            
            var provider = new OpenWeatherForecastProvider(clientMock.Object);
            
            // Act, Assert
            var exception = await Assert.ThrowsAsync<ForecastException>(() => provider.Get5DaysForecastAsync(42));
            Assert.Equal(requestFailedException, exception.InnerException);
        }

        private static OpenWeatherForecastProvider CreateOpenWeatherForecastProvider()
        {
            var clientMock = new Mock<IOpenWeatherHttpClient>();
            OpenWeatherMapResponse response =
                JsonConvert.DeserializeObject<OpenWeatherMapResponse>(File.ReadAllText(Path.Combine("ForecastProviders", "OpenWeatherMap", "forecast.json")));

            clientMock.Setup(m => m.SendGetRequest<OpenWeatherMapResponse>(It.IsAny<(string, string)>()))
                .ReturnsAsync(response);

            return new OpenWeatherForecastProvider(clientMock.Object);
        }
    }
}