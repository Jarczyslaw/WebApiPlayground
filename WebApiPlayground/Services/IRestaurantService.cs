using WebApiPlayground.Entities;

namespace WebApiPlayground.Services
{
    public interface IRestaurantService
    {
        void AddRestaurant(Restaurant restaurant);

        void Delete(int id);

        List<Restaurant> GetAll();

        Restaurant GetById(int id);

        void Update(int id, Restaurant restaurant);
    }
}