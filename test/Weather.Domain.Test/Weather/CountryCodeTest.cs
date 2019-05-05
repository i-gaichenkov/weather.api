using System;
using Weather.Domain.Weather;
using Xunit;

namespace Weather.Domain.Test.Weather
{
    public class CountryCodeTest
    {
        [Fact]
        public void ValidCode()
        {
            // Arrange
            const string code = "DE";
            
            // Act
            var countryCode = new CountryCode(code);
            
            // Assert
            Assert.Equal(code, countryCode.ToString());
        }
        
        [Fact]
        public void InvalidCode_ThrowsException()
        {
            // Arrange
            const string code = "DE-12";
            
            // Act, Assert
            Assert.Throws<ArgumentException>(() => new CountryCode(code));
            
        }
    }
}