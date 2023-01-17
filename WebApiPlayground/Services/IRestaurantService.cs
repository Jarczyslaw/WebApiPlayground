using WebApiPlayground.Entities;

namespace WebApiPlayground.Services
{
    public interface IRestaurantService
    {
        void AddRestaurant(Restaurant restaurant);

        bool Delete(int id);

        List<Restaurant> GetAll();

        Restaurant GetById(int id);

        bool Update(int id, Restaurant restaurant);
    }
}