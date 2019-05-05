using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Options;
using Weather.Logic.Extensions;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap
{
    public class OpenWeatherHttpClient : IOpenWeatherHttpClient
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        private readonly OpenWeatherOptions _options;

        public OpenWeatherHttpClient(IOptions<OpenWeatherOptions> openWeatherOptionsAccessor)
        {
            _options = openWeatherOptionsAccessor.Value;
        }
        
        public async Task<TResponse> SendGetRequest<TResponse>(params (string name, string value)[] queryParameters)
        {
            if (queryParameters.Length == 0)
            {
                throw new ArgumentException("At least one query parameter must be provided");
            }

            var builder = new UriBuilder(_options.Endpoint) { Path = _options.Forecast5RelPath };

            var parametersList = new List<(string name, string value)>(queryParameters)
            {
                ("appid", _options.ApiKey), 
                ("units", "metric")
            };

            builder.Query = string.Join("&",
                parametersList.Select(tuple =>
                    $"{HttpUtility.UrlEncode(tuple.name)}={HttpUtility.UrlEncode(tuple.value)}"));

            string url = builder.ToString();
            var response = await HttpClient.GetAsync(url);
            await ValidateResponseStatusAsync(url, response);

            return await response.ReadAsync<TResponse>();
        }
        
        private static async Task ValidateResponseStatusAsync(string url, HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                throw new RequestFailedException($"Failed to fetch data from {url}", responseString);
            }
        }
    }
}