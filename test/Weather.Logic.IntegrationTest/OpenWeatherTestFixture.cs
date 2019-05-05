using Microsoft.Extensions.Configuration;

namespace Weather.Logic.IntegrationTest
{
    public class OpenWeatherTestFixture
    {
        public IConfigurationRoot Configuration { get; }
        
        public OpenWeatherTestFixture()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.secrets.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }
    }
}