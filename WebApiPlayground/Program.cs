using WebApiPlayground.Entities;
using WebApiPlayground.Models.Dtos;
using WebApiPlayground.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

RegisterServices(builder.Services);

var app = builder.Build();

SeedDatabase(app);

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.UseDeveloperExceptionPage();

app.MapControllers();

app.Run();

static void RegisterServices(IServiceCollection services)
{
    services.AddAutoMapper(typeof(RestaurantDto).Assembly);

    services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
    //services.AddScoped<IWeatherForecastService, WeatherForecastService>(); - one service instance per request
    //services.AddTransient<IWeatherForecastService, WeatherForecastService>(); - one instance per every injection

    services.AddScoped<IRestaurantService, RestaurantService>();
    services.AddDbContext<RestaurantDbContext>();
    services.AddScoped<RestaurantsSeeder>();
}

static void SeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var seeder = scope.ServiceProvider
            .GetRequiredService<RestaurantsSeeder>();
        seeder.Seed();
    }
}