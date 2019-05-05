using System;

namespace Weather.Logic.ForecastProviders.OpenWeatherMap
{
    public class RequestFailedException : Exception
    {
        public RequestFailedException(string message, string response)
            : base(BuildMessage(message, response))
        {
            
        }
        
        private static string BuildMessage(string message, string response)
        {
            return $"{message}{Environment.NewLine}{response}";
        }
    }
}