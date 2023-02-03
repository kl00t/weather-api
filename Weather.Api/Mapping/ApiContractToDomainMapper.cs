using Acme.Weather.Api.Contracts.Responses;
using Acme.Weather.Api.Models;

namespace Acme.Weather.Api.Mapping;

public static class ApiContractToDomainMapper
{
    public static GetWeatherResponse ToGetWeatherResponse(this OpenWeatherMapApiResponse response)
    {
        return new GetWeatherResponse
        {
            Location = response.ToLocation(),
            Temperature = response.ToTemperature(),
            Weather = response.ToWeather()
        };
    }

    public static Contracts.Responses.Location ToLocation(this OpenWeatherMapApiResponse response)
    {
        return new Contracts.Responses.Location
        {
            Name = response.Name,
            Latitude = response.Coordinates.Latitude,
            Longitude = response.Coordinates.Longitude
        };
    }

    public static Contracts.Responses.Temperature ToTemperature(this OpenWeatherMapApiResponse response)
    {
        return new Contracts.Responses.Temperature
        {
            CurrentTemperature = response.Temperature.Current,
            RealFeel = response.Temperature.FeelsLike,
            High = response.Temperature.Maximum,
            Low = response.Temperature.Minimum
        };
    }

    public static Contracts.Responses.Weather ToWeather(this OpenWeatherMapApiResponse response)
    {
        return new Contracts.Responses.Weather
        {
            Description = response.Weather.FirstOrDefault().Description,
            Icon = response.Weather.FirstOrDefault().IconId,
        };
    }
}
