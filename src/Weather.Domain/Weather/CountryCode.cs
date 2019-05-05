using System;
using System.Text.RegularExpressions;

namespace Weather.Domain.Weather
{
    public class CountryCode
    {
        private static readonly Regex ValidationRegex = new Regex(@"^\w+$");
        
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
            
            ValidateCode(value);
            Value = value;
        }

        public override string ToString()
        {
            return Value;
        }

        public override int GetHashCode()
        {
            return (Value != null ? Value.GetHashCode() : 0);
        }

        protected bool Equals(CountryCode other)
        {
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CountryCode) obj);
        }

        private static void ValidateCode(string code)
        {
            if (!ValidationRegex.IsMatch(code))
            {
                throw new ArgumentException($"'{code}' isn't a valid country code");
            }
        }
    }
}