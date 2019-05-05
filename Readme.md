# Weather API
Sample ASP.NET Core weather API backend using OpenWeatherMap as a source

# Before you run
Replace `<ApiKey>` placeholder in the appsettings.json configuration file with a real api key provided by OpenWeatherMap

`Weather.Logic.IntegrationTest` test project is missing a configuration file with the api key as well. Create a configuration file `appsettings.secrets.json` with the following content in the project folder:
```
{
  "ApiKey": "<api key>"
}
```
