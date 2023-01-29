using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Entities;
using WebApiPlayground.Exceptions;

namespace WebApiPlayground.Services
{
    public class DishService : IDishService
    {
        private readonly RestaurantDbContext _context;

        public DishService(RestaurantDbContext context)
        {
            _context = context;
        }

        public int AddDish(int restaurantId, Dish dish)
        {
            var restaurant = _context.Restaurants
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

            dish.RestaurantId = restaurant.Id;
            _context.Dishes.Add(dish);
            _context.SaveChanges();

            return dish.Id;
        }

        public void DeleteDishes(int restaurantId)
        {
            var restaurant = GetRestaurantAndDishes(restaurantId);

            _context.Dishes.RemoveRange(restaurant.Dishes);
            _context.SaveChanges();
        }

        public Dish GetById(int restaurantId, int dishId)
        {
            var dish = _context.Dishes
               .FirstOrDefault(x => x.Id == dishId && x.RestaurantId == restaurantId);

            if (dish == null) { throw new NotFoundException("Dish not found"); }

            return dish;
        }

        public List<Dish> GetByRestaurantId(int restaurantId)
        {
            var restaurant = _context.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

            return restaurant.Dishes;
        }

        private Restaurant GetRestaurantAndDishes(int restaurantId)
        {
            var restaurant = _context.Restaurants
                .Include(x => x.Dishes)
                .FirstOrDefault(x => x.Id == restaurantId);

            if (restaurant == null) { throw new NotFoundException("Restaurant not found"); }

            return restaurant;
        }
    }
}