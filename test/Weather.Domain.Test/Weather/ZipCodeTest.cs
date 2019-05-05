using System;
using Weather.Domain.Weather;
using Xunit;

namespace Weather.Domain.Test.Weather
{
    public class ZipCodeTest
    {
        [Theory]
        [InlineData("12345")]
        [InlineData("123-45")]
        [InlineData("123-45AB")]
        public void ValidCode(string zipCodeValue)
        {
            // Act
            var zipCode = new ZipCode(zipCodeValue);
            
            // Assert
            Assert.Equal(zipCodeValue, zipCode.ToString());
        }
        
        [Theory]
        [InlineData("!123ABC%")]
        [InlineData("123=ABC")]
        public void InvalidCode_ThrowsException(string zipCodeValue)
        {
            // Act, Assert
            Assert.Throws<ArgumentException>(() => new ZipCode(zipCodeValue));
        }
    }
}