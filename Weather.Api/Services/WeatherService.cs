using Acme.Weather.Api.Clients;
using Acme.Weather.Api.Contracts.Requests;
using Acme.Weather.Api.Contracts.Responses;
using Acme.Weather.Api.Mapping;

namespace Acme.Weather.Api.Services;

public class WeatherService : IWeatherService
{
    private readonly ILogger<WeatherService> _logger;
    private readonly IWeatherClient _weatherClient;

    public WeatherService(
        ILogger<WeatherService> logger,
        IWeatherClient weatherClient)
    {
        _logger = logger;
        _weatherClient = weatherClient;
    }

    public async Task<GetWeatherResponse> GetCurrentWeatherForCity(GetWeatherForCityRequest request)
    {
        _logger.LogInformation("Calling {service}", nameof(WeatherService.GetCurrentWeatherForCity));
        var response = await _weatherClient.GetCurrentWeatherForCity(request.City);
        return response.ToGetWeatherResponse();
    }

    public async Task<GetWeatherResponse> GetCurrentWeatherForLocation(GetWeatherForLocationRequest request)
    {
        _logger.LogInformation("Calling {service}", nameof(WeatherService.GetCurrentWeatherForLocation));
        var response = await _weatherClient.GetCurrentWeatherForLocation(request.Latitude, request.Longitude);
        return response.ToGetWeatherResponse();
    }
}