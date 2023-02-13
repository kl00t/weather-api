using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Weather.Api.Authentication;

/// <summary>
/// Api Key Authentication Attribute Filter.
/// Applied on a controller to authentication that the api key is in the header and is valid.
/// CANNOT be applied to minimal api.
/// </summary>
[AttributeUsage(AttributeTargets.All)]
public class ApiKeyAuthenticationFilter : Attribute, IAuthorizationFilter
{
    public ApiKeyAuthenticationFilter()
    {
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName, out var extractedApiKey))
        {
            context.Result = new UnauthorizedObjectResult($"Header: {AuthConstants.ApiKeyHeaderName} is missing.");
            return;
        }

        var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var apiKey = configuration.GetValue<string>(AuthConstants.ApiKeySectionName);
        if (!apiKey.Equals(extractedApiKey))
        {

            context.Result = new UnauthorizedObjectResult("Invalid API Key.");
            return;
        }
    }
}