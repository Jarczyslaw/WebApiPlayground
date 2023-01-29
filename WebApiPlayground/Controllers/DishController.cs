using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Entities;
using WebApiPlayground.Models.Dtos;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    [Route("api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly ILogger<DishController> _logger;
        private readonly IMapper _mapper;
        private readonly IDishService _service;

        public DishController(
            IDishService service,
            IMapper mapper,
            ILogger<DishController> logger)
        {
            _service = service;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public ActionResult CreateDish([FromRoute] int restaurantId, [FromBody] CreateDishDto dto)
        {
            var dish = _mapper.Map<Dish>(dto);
            _service.AddDish(restaurantId, dish);

            return new CreatedResult($"/api/restaurant/{restaurantId}/dish/{dish.Id}", null);
        }

        [HttpDelete]
        public ActionResult DeleteDishes([FromRoute] int restaurantId)
        {
            _service.DeleteDishes(restaurantId);
            _logger.LogInformation($"Dishes cleared");

            return NoContent();
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> GetDish([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            var restaurant = _service.GetById(restaurantId, dishId);
            var result = _mapper.Map<DishDto>(restaurant);

            return new OkObjectResult(result);
        }

        [HttpGet]
        public ActionResult<DishDto> GetDishes([FromRoute] int restaurantId)
        {
            var dishes = _service.GetByRestaurantId(restaurantId);
            var result = _mapper.Map<List<DishDto>>(dishes);

            return new OkObjectResult(result);
        }
    }
}