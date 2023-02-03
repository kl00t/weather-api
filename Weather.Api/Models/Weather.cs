using System.Text.Json.Serialization;

namespace Acme.Weather.Api.Models;

public class Weather
{
    /// <summary>
    /// Weather condition id
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; set; }

    /// <summary>
    /// Group of weather parameters (Rain, Snow, Extreme etc.)
    /// </summary>
    [JsonPropertyName("main")]
    public string Main { get; set; }

    /// <summary>
    /// Weather condition within the group
    /// </summary>
    [JsonPropertyName("description")]
    public string Description { get; set; }

    /// <summary>
    /// Weather icon id
    /// </summary>
    [JsonPropertyName("icon")]
    public string IconId { get; set; }
}