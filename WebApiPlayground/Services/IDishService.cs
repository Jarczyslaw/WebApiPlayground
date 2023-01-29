using WebApiPlayground.Entities;

namespace WebApiPlayground.Services
{
    public interface IDishService
    {
        int AddDish(int restaurantId, Dish dish);

        void DeleteDishes(int restaurantId);

        Dish GetById(int restaurantId, int dishId);

        List<Dish> GetByRestaurantId(int restaurantId);
    }
}