using System;

namespace Weather.Domain.Weather
{
    public class CountryCode
    {
        public static readonly CountryCode Empty = new CountryCode();
        public string Value { get; }

        private CountryCode()
        {
            Value = string.Empty;
        }

        public CountryCode(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(value));
            
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}