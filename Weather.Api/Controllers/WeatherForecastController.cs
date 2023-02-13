using Acme.Weather.Api.Contracts.Requests;
using Acme.Weather.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Weather.Api.Authentication;

namespace Acme.Weather.Api.Controllers;

[ApiController]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IWeatherService _weatherService;

    public WeatherForecastController(
        ILogger<WeatherForecastController> logger,
        IWeatherService weatherService)
    {
        _logger = logger;
        _weatherService = weatherService;
    }

    [HttpGet("weather/{city}")]
    [ApiKeyAuthenticationFilter]
    public async Task<IActionResult> GetWeatherForecast(string city)
    {
        _logger.LogInformation("Getting weather for {city}.", city);
        var weatherResponse = await _weatherService.GetCurrentWeatherForCity(new GetWeatherForCityRequest(city));
        return weatherResponse is not null ? Ok(weatherResponse) : NotFound();
    }

    [HttpGet("weather")]
    [ApiKeyAuthenticationFilter]
    public async Task<IActionResult> GetWeatherForecast([FromQuery]decimal lat, [FromQuery]decimal lon)
    {
        _logger.LogInformation("Getting weather for {latitude}, {longitude}.", lat, lon);
        var weather = await _weatherService.GetCurrentWeatherForLocation(new GetWeatherForLocationRequest(lat, lon));
        return weather is not null ? Ok(weather) : NotFound();
    }
}