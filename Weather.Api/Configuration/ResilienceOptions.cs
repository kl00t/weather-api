namespace Acme.Weather.Api.Configuration;

public class ResilienceOptions
{
    public int RetryDelay { get; set; }

    public int RetryCount { get; set; }

    public enum ResiliencePolicy
    {
        TransientHttpErrorPolicy
    }
}