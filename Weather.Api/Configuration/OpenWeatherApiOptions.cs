namespace Acme.Weather.Api.Configuration;

public class OpenWeatherApiOptions
{
    public string BaseAddress { get; set; }

    public string OpenWeatherMapApiKey { get; set; }

    public string ClientName { get; set; }

    public string Units { get; set; }

    public string Language { get; set; }
}