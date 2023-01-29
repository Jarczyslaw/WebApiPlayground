using NLog.Web;
using WebApiPlayground.Entities;
using WebApiPlayground.Middleware;
using WebApiPlayground.Models.Dtos;
using WebApiPlayground.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.WebHost.UseNLog();

RegisterServices(builder.Services);

var app = builder.Build();

SeedDatabase(app);

// Configure the HTTP request pipeline.
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<ExecutionTimeMiddleware>();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

static void RegisterServices(IServiceCollection services)
{
    services.AddAutoMapper(typeof(RestaurantDto).Assembly);

    services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
    //services.AddScoped<IWeatherForecastService, WeatherForecastService>(); - one service instance per request
    //services.AddTransient<IWeatherForecastService, WeatherForecastService>(); - one instance per every injection

    services.AddScoped<IRestaurantService, RestaurantService>();
    services.AddScoped<IDishService, DishService>();

    services.AddDbContext<RestaurantDbContext>();
    services.AddScoped<RestaurantsSeeder>();
    services.AddScoped<ErrorHandlingMiddleware>();
    services.AddScoped<ExecutionTimeMiddleware>();
    services.AddSwaggerGen();
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