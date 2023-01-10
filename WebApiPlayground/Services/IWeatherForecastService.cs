using WebApiPlayground.Models;

namespace WebApiPlayground.Services
{
    public interface IWeatherForecastService
    {
        public IEnumerable<WeatherForecast> Get(
            int resultsCount = 5,
            int minTemperature = -20,
            int maxTemperature = 55);
    }
}