using Microsoft.AspNetCore.Mvc;
using System.Text;
using WebApiPlayground.Models;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _service;

        public WeatherForecastController(
            IWeatherForecastService service,
            ILogger<WeatherForecastController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost("generate")]
        public IActionResult Generate(int resultsCount, [FromBody] TemperaturesRange temperaturesRange)
        {
            if (resultsCount < 1 || temperaturesRange.Minimum > temperaturesRange.Maximum) { return BadRequest(); }

            return new OkObjectResult(_service.Get(resultsCount, temperaturesRange.Minimum, temperaturesRange.Maximum));
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            return _service.Get();
        }

        [HttpPost("params/{fromRoute}")]
        public ActionResult<string> Params(
            [FromRoute] string fromRoute,
            [FromQuery] int fromQuery,
            [FromBody] string fromBody)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"From route: {fromRoute}");
            sb.AppendLine($"From query: {fromQuery}");
            sb.AppendLine($"From body: {fromBody}");

            return sb.ToString();
        }
    }
}