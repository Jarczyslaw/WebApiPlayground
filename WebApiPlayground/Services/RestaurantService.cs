using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Entities;
using WebApiPlayground.Exceptions;

namespace WebApiPlayground.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;

        public RestaurantService(RestaurantDbContext context)
        {
            _context = context;
        }

        public void AddRestaurant(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var restaurant = _context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

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

        public void Update(int id, Restaurant restaurant)
        {
            var current = _context.Restaurants
                .FirstOrDefault(x => x.Id == id);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

            current.Description = restaurant.Description;
            current.Name = restaurant.Name;
            current.HasDelivery = restaurant.HasDelivery;

            _context.SaveChanges();
        }
    }
}