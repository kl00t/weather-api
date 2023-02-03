namespace Acme.Weather.Api.Contracts.Responses;

public class GetWeatherResponse
{
    public Location Location { get; set; }

    public Weather Weather { get; set; }

    public Temperature Temperature { get; set; }
}