using System;

namespace Weather.Logic.ForecastProviders
{
    public class ForecastException : Exception
    {
        public ForecastException(string message)
            : base(message)
        {
            
        }
        
        public ForecastException(string message, Exception inner)
            : base(message, inner)
        {
            
        }
    }
}