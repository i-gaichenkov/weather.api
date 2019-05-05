using System.Threading.Tasks;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap
{
    public interface IOpenWeatherHttpClient
    {
        Task<TResponse> SendGetRequest<TResponse>(params (string name, string value)[] queryParameters);
    }
}