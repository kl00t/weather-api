namespace Weather.Api.Authentication;

/// <summary>
/// Applies Api Key Authentication via middleware to all requests in the Httpcontext pipeline.
/// Configured by adding 'app.UseMiddleware<ApiKeyAuthenticationMiddleware>();' to the Program.cs
/// CAN be applied to minimal api.
/// </summary>
public class ApiKeyAuthenticationMiddleware
{
    private readonly RequestDelegate _next;
	private readonly IConfiguration _configuration;

	public ApiKeyAuthenticationMiddleware(RequestDelegate next, IConfiguration configuration)
	{
		_next = next;
		_configuration = configuration;
	}

	public async Task InvokeAsync(HttpContext context)
	{
		if (!context.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
		{
			context.Response.StatusCode = 401;
			await context.Response.WriteAsync($"Header: {AuthConstants.ApiKeyHeaderName} is missing.");
			return;
		}

		var apiKey = _configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
		if (!apiKey.Equals(extractedApiKey))
		{
            context.Response.StatusCode = 401;
            await context.Response.WriteAsync("Invalid API Key.");
            return;
        }

		await _next(context);
	}
}