using System.Threading.Tasks;
using Weather.Domain.Weather;

namespace Weather.Logic.ForecastProviders
{
    public interface IForecastProvider
    {
        Task<Forecast> Get5DaysForecastAsync(string city, CountryCode countryCode);
        
        Task<Forecast> Get5DaysForecastAsync(ZipCode zipCode, CountryCode countryCode);
        
        Task<Forecast> Get5DaysForecastAsync(int cityId);
    }
}