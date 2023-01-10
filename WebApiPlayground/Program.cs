using WebApiPlayground.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

RegisterServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();

static void RegisterServices(IServiceCollection services)
{
    services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
    //services.AddScoped<IWeatherForecastService, WeatherForecastService>(); - one service instance per request
    //services.AddTransient<IWeatherForecastService, WeatherForecastService>(); - one instance per every injection
}