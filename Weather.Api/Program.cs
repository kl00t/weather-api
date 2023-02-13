using Acme.Weather.Api.Extensions;
using Serilog;
using Weather.Api.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
builder.Services.AddWeatherService();
builder.Services.AddResilienceStrategy(builder.Configuration);
builder.Services.AddOpenWeatherMapApiClient(builder.Configuration);

var app = builder.Build();

app.Logger.LogInformation("The Weather.Api has started at {Date}.", DateTime.UtcNow);

app.UseHttpsRedirection();

app.UseMiddleware<ApiKeyAuthenticationMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();