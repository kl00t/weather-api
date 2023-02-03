using System.Text.Json.Serialization;

namespace Acme.Weather.Api.Models;

public class Temperature
{
    /// <summary>
    /// Temperature. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
    /// </summary>
    [JsonPropertyName("temp")]
    public decimal Current { get; set; }

    /// <summary>
    /// Temperature. This temperature parameter accounts for the human perception of weather. Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
    /// </summary>
    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; set; }

    /// <summary>
    /// Minimum temperature at the moment. This is minimal currently observed temperature (within large megalopolises and urban areas). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
    /// </summary>
    [JsonPropertyName("temp_min")]
    public decimal Minimum { get; set; }

    /// <summary>
    /// Maximum temperature at the moment. This is maximal currently observed temperature (within large megalopolises and urban areas). Unit Default: Kelvin, Metric: Celsius, Imperial: Fahrenheit.
    /// </summary>
    [JsonPropertyName("temp_max")]
    public decimal Maximum { get; set; }
}