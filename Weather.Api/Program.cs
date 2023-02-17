using Acme.Weather.Api.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using Weather.Api;
using Weather.Api.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddOptions<ExampleOptions>()
    .Bind(builder.Configuration.GetSection(ExampleOptions.SectionName))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Logging.ClearProviders();
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
builder.Logging.AddSerilog(logger);

builder.Services.AddControllers();
//builder.Services.AddScoped<ApiKeyAuthenticationFilter>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        Description = "The API key has access to the API",
        Type = SecuritySchemeType.ApiKey,
        Name = AuthConstants.ApiKeyHeaderName,
        In = ParameterLocation.Header,
        Scheme = "ApiKeyScheme"
    });

    var scheme = new OpenApiSecurityScheme
    {
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "ApiKey"
        },
        In = ParameterLocation.Header
    };

    var requirement = new OpenApiSecurityRequirement
    {
        { scheme, new List<string>() }
    };

    c.AddSecurityRequirement(requirement);
});

builder.Services.AddWeatherService();
builder.Services.AddResilienceStrategy(builder.Configuration);
builder.Services.AddOpenWeatherMapApiClient(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Logger.LogInformation("The Weather.Api has started at {Date}.", DateTime.UtcNow);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("hello", (IOptions<ExampleOptions> opt) => opt.Value);

app.Run();