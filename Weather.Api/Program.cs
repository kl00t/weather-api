using Acme.Weather.Api.Extensions;
using Serilog;

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

app.Logger.LogInformation("The Acme.Weather.Api has started at {Date}.", DateTime.UtcNow);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();