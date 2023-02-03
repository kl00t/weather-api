namespace Acme.Weather.Api.Contracts.Requests;

public class GetWeatherForLocationRequest
{
    public GetWeatherForLocationRequest(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
    }

    public decimal Latitude { get; }

    public decimal Longitude { get; }
}