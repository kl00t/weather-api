using System.Text.Json.Serialization;

namespace Acme.Weather.Api.Models;

public class OpenWeatherMapApiResponse
{
    /// <summary>
    /// City geo location
    /// </summary>
    [JsonPropertyName("coord")]
    public Coordinates Coordinates { get; set; }

    /// <summary>
    /// Weather condition codes
    /// </summary>
    [JsonPropertyName("weather")]
    public Weather[] Weather { get; set; }

    /// <summary>
    /// Weather condition details.
    /// </summary>
    [JsonPropertyName("main")]
    public Temperature Temperature { get; set; }

    /// <summary>
    /// Visibility, meter. The maximum value of the visibility is 10km
    /// </summary>
    [JsonPropertyName("visibility")]
    public int Visibility { get; set; }

    /// <summary>
    /// City name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; set; }
}

////{
////  "coord": {
////    "lon": 10.99,
////    "lat": 44.34
////  },
////  "weather": [
////    {
////      "id": 501,
////      "main": "Rain",
////      "description": "moderate rain",
////      "icon": "10d"
////    }
////  ],
////  "base": "stations",
////  "main": {
////    "temp": 298.48,
////    "feels_like": 298.74,
////    "temp_min": 297.56,
////    "temp_max": 300.05,
////    "pressure": 1015,
////    "humidity": 64,
////    "sea_level": 1015,
////    "grnd_level": 933
////  },
////  "visibility": 10000,
////  "wind": {
////    "speed": 0.62,
////    "deg": 349,
////    "gust": 1.18
////  },
////  "rain": {
////    "1h": 3.16
////  },
////  "clouds": {
////    "all": 100
////  },
////  "dt": 1661870592,
////  "sys": {
////    "type": 2,
////    "id": 2075663,
////    "country": "IT",
////    "sunrise": 1661834187,
////    "sunset": 1661882248
////  },
////  "timezone": 7200,
////  "id": 3163858,
////  "name": "Zocca",
////  "cod": 200
////}                        