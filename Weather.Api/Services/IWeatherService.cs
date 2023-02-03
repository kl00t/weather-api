using Acme.Weather.Api.Contracts.Requests;
using Acme.Weather.Api.Contracts.Responses;

namespace Acme.Weather.Api.Services;

public interface IWeatherService
{
    Task<GetWeatherResponse> GetCurrentWeatherForCity(GetWeatherForCityRequest request);

    Task<GetWeatherResponse> GetCurrentWeatherForLocation(GetWeatherForLocationRequest request);
}