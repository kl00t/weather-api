using Acme.Weather.Api.Models;

namespace Acme.Weather.Api.Clients;

public interface IWeatherClient
{
    Task<OpenWeatherMapApiResponse> GetCurrentWeatherForCity(string city);

    Task<OpenWeatherMapApiResponse> GetCurrentWeatherForLocation(decimal latitude, decimal longitude);
}