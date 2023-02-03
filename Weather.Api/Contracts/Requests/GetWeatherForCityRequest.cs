namespace Acme.Weather.Api.Contracts.Requests;

public class GetWeatherForCityRequest
{
	public GetWeatherForCityRequest(string city)
	{
		City = city;
	}

    public string City { get; }
}