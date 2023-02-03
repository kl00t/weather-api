using Acme.Weather.Api.Clients;
using Acme.Weather.Api.Configuration;
using Acme.Weather.Api.Services;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Polly.Registry;
using static Acme.Weather.Api.Configuration.ResilienceOptions;

namespace Acme.Weather.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddResilienceStrategy(this IServiceCollection services, IConfiguration configuration)
    {
        var resilienceOptions = new ResilienceOptions();
        configuration.GetSection(nameof(ResilienceOptions)).Bind(resilienceOptions);

        var localApiResilienceDelay = Backoff.DecorrelatedJitterBackoffV2(
            TimeSpan.FromSeconds(resilienceOptions.RetryDelay), 
            resilienceOptions.RetryCount);

        var registry = new PolicyRegistry
        {
            {
                ResiliencePolicy.TransientHttpErrorPolicy.ToString(),
                HttpPolicyExtensions
                    .HandleTransientHttpError()
                    .WaitAndRetryAsync(localApiResilienceDelay)
            }
        };

        services.AddPolicyRegistry(registry);
    }

    public static void AddOpenWeatherMapApiClient(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IWeatherClient, OpenWeatherClient>();

        var openWeatherApiOptions = new OpenWeatherApiOptions();
        var openWeatherApiOptionsSection = configuration.GetSection(nameof(OpenWeatherApiOptions));
        openWeatherApiOptionsSection.Bind(openWeatherApiOptions);
        services.Configure<OpenWeatherApiOptions>(openWeatherApiOptionsSection);

        services.AddHttpClient("openweathermap", client =>
        {
            client.BaseAddress = new Uri(openWeatherApiOptions.BaseAddress);
        })
            .AddPolicyHandlerFromRegistry(ResiliencePolicy.TransientHttpErrorPolicy.ToString());
    }

    public static void AddWeatherService(this IServiceCollection services)
    {
        services.AddSingleton<IWeatherService, WeatherService>();
    }
}