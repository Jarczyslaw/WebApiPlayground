using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApiPlayground.Entities;
using WebApiPlayground.Models.Dtos;
using WebApiPlayground.Services;

namespace WebApiPlayground.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IRestaurantService _service;

        public RestaurantController(IRestaurantService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult Create([FromBody] CreateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var restaurant = _mapper.Map<Restaurant>(dto);

            _service.AddRestaurant(restaurant);

            return new CreatedResult($"/api/restaurant/{restaurant.Id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var isDeleted = _service.Delete(id);
            if (isDeleted) { return NoContent(); }

            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _service.GetAll();

            var result = _mapper.Map<List<RestaurantDto>>(restaurants);

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetById(int id)
        {
            var restaurant = _service.GetById(id);

            if (restaurant == null) { return new NotFoundResult(); }

            var result = _mapper.Map<RestaurantDto>(restaurant);

            return new OkObjectResult(result);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, [FromBody] UpdateRestaurantDto dto)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }

            var restaurant = _mapper.Map<Restaurant>(dto);

            var isUpdated = _service.Update(id, restaurant);
            if (isUpdated) { return NoContent(); }

            return NotFound();
        }
    }
}