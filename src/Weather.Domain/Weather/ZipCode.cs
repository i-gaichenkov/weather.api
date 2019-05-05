using System;

namespace Weather.Domain.Weather
{
    public class ZipCode
    {
        //TODO: add format validation
        public string Code { get; }

        public ZipCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(code));
            
            Code = code;
        }
    }
}