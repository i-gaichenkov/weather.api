using System;
using System.Text.RegularExpressions;

namespace Weather.Domain.Weather
{
    public class ZipCode
    {
        private static readonly Regex ValidationRegex = new Regex(@"^[\w\d-]+$");

        public string Code { get; }

        public ZipCode(string code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(code));

            ValidateCode(code);
            Code = code;
        }

        public override string ToString()
        {
            return Code;
        }

        public override int GetHashCode()
        {
            return (Code != null ? Code.GetHashCode() : 0);
        }

        protected bool Equals(ZipCode other)
        {
            return string.Equals(Code, other.Code);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((ZipCode) obj);
        }

        private static void ValidateCode(string code)
        {
            if (!ValidationRegex.IsMatch(code))
            {
                throw new ArgumentException($"'{code}' isn't a valid ZIP code");
            }
        }
    }
}