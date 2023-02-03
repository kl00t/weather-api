namespace Acme.Weather.Api.Contracts.Responses;

public class Temperature
{
    public decimal CurrentTemperature { get; set; }

    public decimal RealFeel { get; set; }

    public decimal High { get; set; }

    public decimal Low { get; set; }
}
