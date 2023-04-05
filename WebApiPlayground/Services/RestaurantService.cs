using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Configuration;
using WebApiPlayground.Entities;
using WebApiPlayground.Exceptions;

namespace WebApiPlayground.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly RestaurantDbContext _context;
        private readonly IUserContextService _userContextService;

        public RestaurantService(RestaurantDbContext context, IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _context = context;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            restaurant.CreatedUserId = _userContextService.UserId;

            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var restaurant = _context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

            var authorizationResult
                = await _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete));

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _context.Restaurants.Remove(restaurant);
            _context.SaveChanges();
        }

        public List<Restaurant> GetAll()
        {
            var restaurants = _context.Restaurants
               .Include(x => x.Address)
               .Include(x => x.Dishes)
               .ToList();

            return restaurants;
        }

        public Restaurant GetById(int id)
        {
            var restaurant = _context.Restaurants
                .Include(x => x.Address)
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

            return restaurant;
        }

        public async Task Update(int id, Restaurant restaurant)
        {
            var current = _context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

            var authorizationResult
                = await _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update));

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            current.Description = restaurant.Description;
            current.Name = restaurant.Name;
            current.HasDelivery = restaurant.HasDelivery;

            _context.SaveChanges();
        }
    }
}