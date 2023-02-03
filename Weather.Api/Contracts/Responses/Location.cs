namespace Acme.Weather.Api.Contracts.Responses;

public class Location
{
    public string Name { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
}