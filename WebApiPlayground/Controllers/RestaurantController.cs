using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiPlayground.Entities;
using WebApiPlayground.Models;
using WebApiPlayground.Models.Dtos;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly ILogger<RestaurantController> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantService _service;

        public RestaurantController(
            IRestaurantService service,
            IMapper mapper,
            ILogger<RestaurantController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateRestaurantDto dto)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var restaurant = _mapper.Map<Restaurant>(dto);

            _service.AddRestaurant(restaurant);

            return new CreatedResult($"/api/restaurant/{restaurant.Id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            _service.Delete(id);
            _logger.LogInformation($"Restaurant {id} deleted");

            return NoContent();
        }

        [HttpGet]
        public ActionResult<PageResult<RestaurantDto>> GetAll([FromQuery] RestaurantQuery query)
        {
            var result = _service.GetAll(query);

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetById(int id)
        {
            var restaurant = _service.GetById(id);
            var result = _mapper.Map<RestaurantDto>(restaurant);

            return new OkObjectResult(result);
        }

        [HttpGet("error")]
        public ActionResult GetError()
        {
            throw new Exception("Exception from server");
        }

        [HttpGet("long")]
        public async Task<ActionResult> LongAction()
        {
            await Task.Delay(2000);
            return new OkResult();
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] UpdateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);

            _service.Update(id, restaurant);
            return NoContent();
        }
    }
}