using Acme.Weather.Api.Configuration;
using Acme.Weather.Api.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Polly;
using Polly.CircuitBreaker;
using Polly.Extensions.Http;

namespace Acme.Weather.Api.Clients;

public class OpenWeatherClient : IWeatherClient
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly OpenWeatherApiOptions _openWeatherApiOptions;
    private readonly ILogger<OpenWeatherClient> _logger;
    private readonly IMemoryCache _weatherCache = new MemoryCache(new MemoryCacheOptions());
    private readonly AsyncCircuitBreakerPolicy<HttpResponseMessage> _circuitBreakerPolicy =
        Policy<HttpResponseMessage>
        .Handle<HttpRequestException>()
        .OrTransientHttpError()
        .AdvancedCircuitBreakerAsync(0.5, TimeSpan.FromSeconds(10), 10, TimeSpan.FromSeconds(15));

    public OpenWeatherClient(
        IHttpClientFactory httpClientFactory, 
        IOptionsMonitor<OpenWeatherApiOptions> openWeatherApiOptions,
        ILogger<OpenWeatherClient> logger)
    {
        _httpClientFactory = httpClientFactory;
        _openWeatherApiOptions = openWeatherApiOptions.CurrentValue;
        _logger = logger;
    }

    public async Task<OpenWeatherMapApiResponse> GetCurrentWeatherForCity(string city)
    {
        _logger.LogInformation("Getting weather for {city}.", city);
        var httpClient = _httpClientFactory.CreateClient(_openWeatherApiOptions.ClientName);

        if (_circuitBreakerPolicy.CircuitState is CircuitState.Open or CircuitState.Isolated)
        {
            return _weatherCache.Get<OpenWeatherMapApiResponse>(city);
        }

        var response = await _circuitBreakerPolicy.ExecuteAsync(() => httpClient.GetAsync(
            $"weather?q={city}&appid={_openWeatherApiOptions.OpenWeatherMapApiKey}&units={_openWeatherApiOptions.Units}&lang={_openWeatherApiOptions.Language}"));
        var weatherResponse = await response.Content.ReadFromJsonAsync<OpenWeatherMapApiResponse>();
        _weatherCache.Set(city, weatherResponse);
        return weatherResponse;
    }

    public async Task<OpenWeatherMapApiResponse> GetCurrentWeatherForLocation(decimal latitude, decimal longitude)
    {
        _logger.LogInformation("Getting weather for {latitude}, {longitude}.", latitude, longitude);
        var httpClient = _httpClientFactory.CreateClient(_openWeatherApiOptions.ClientName);
        var response = await httpClient.GetAsync(
            $"weather?lat={latitude}&lon={longitude}&appid={_openWeatherApiOptions.OpenWeatherMapApiKey}&units={_openWeatherApiOptions.Units}&lang={_openWeatherApiOptions.Language}");
        return await response.Content.ReadFromJsonAsync<OpenWeatherMapApiResponse>();
    }
}