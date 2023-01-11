using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Entities;
using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public RestaurantController(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurants = _context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .ToList();

            var result = _mapper.Map<List<RestaurantDto>>(restaurants);

            return new OkObjectResult(result);
        }

        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> GetById(int id)
        {
            var restaurant = _context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) { return new NotFoundResult(); }

            var result = _mapper.Map<RestaurantDto>(restaurant);

            return new OkObjectResult(result);
        }
    }
}