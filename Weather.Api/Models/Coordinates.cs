using System.Text.Json.Serialization;

namespace Acme.Weather.Api.Models;

public class Coordinates
{
    /// <summary>
    /// City geo location, latitude
    /// </summary>
    [JsonPropertyName("lat")]
    public decimal Latitude { get; set; }

    /// <summary>
    /// City geo location, longitude
    /// </summary>
    [JsonPropertyName("lon")]
    public decimal Longitude { get; set; }
}