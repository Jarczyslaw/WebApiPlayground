using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Text;
using WebApiPlayground.Configuration;
using WebApiPlayground.Entities;
using WebApiPlayground.Middleware;
using WebApiPlayground.Models.Dtos;
using WebApiPlayground.Models.Validators;
using WebApiPlayground.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.WebHost.UseNLog();

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);

RegisterServices(builder.Services, authenticationSettings);

var app = builder.Build();

SeedDatabase(app);

// Configure the HTTP request pipeline.
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseMiddleware<ErrorHandlingMiddleware>()
    .UseMiddleware<ExecutionTimeMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();

static void RegisterServices(IServiceCollection services, AuthenticationSettings authenticationSettings)
{
    services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme
            = x.DefaultScheme
            = x.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = authenticationSettings.JwtIssuer,
            ValidAudience = authenticationSettings.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey))
        };
    });

    services.AddAutoMapper(typeof(RestaurantDto).Assembly);

    services.AddSingleton<IWeatherForecastService, WeatherForecastService>();
    services.AddSingleton(authenticationSettings);
    //services.AddScoped<IWeatherForecastService, WeatherForecastService>(); - one service instance per request
    //services.AddTransient<IWeatherForecastService, WeatherForecastService>(); - one instance per every injection

    services.AddScoped<IRestaurantService, RestaurantService>();
    services.AddScoped<IDishService, DishService>();
    services.AddScoped<IAccountService, AccountService>();

    services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

    services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();

    services.AddDbContext<RestaurantDbContext>();
    services.AddScoped<RestaurantsSeeder>();
    services.AddScoped<ErrorHandlingMiddleware>();
    services.AddScoped<ExecutionTimeMiddleware>();
    services.AddScoped<IUserContextService, UserContextService>();
    services.AddHttpContextAccessor();
    services.AddSwaggerGen();

    services.AddAuthorization(x =>
    {
        x.AddPolicy("HasNationality", y => y.RequireClaim("Nationality", "Polish"));
        x.AddPolicy("AtLeast20", y => y.AddRequirements(new MinimumAgeRequirement(20)));
    });
    services.AddScoped<IAuthorizationHandler, MinimumAgeRequirementHandler>();
    services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
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