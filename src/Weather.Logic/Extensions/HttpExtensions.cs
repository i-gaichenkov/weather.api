using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Weather.Logic.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<TResponse> ReadAsync<TResponse>(this HttpResponseMessage response)
        {
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }
    }
}