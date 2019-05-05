using System;

namespace Weather.Domain.Weather
{
    public class City
    {
        public int Id { get; }
        
        public string Name { get; }
        
        public CountryCode CountryCode { get; }

        public City(int id, string name, CountryCode countryCode)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));
            }
            
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            Id = id;
            Name = name;
            CountryCode = countryCode ?? throw new ArgumentNullException(nameof(countryCode));
        }
    }
}