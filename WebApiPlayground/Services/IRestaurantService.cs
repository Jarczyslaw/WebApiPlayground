using WebApiPlayground.Entities;

namespace WebApiPlayground.Services
{
    public interface IRestaurantService
    {
        void AddRestaurant(Restaurant restaurant);

        Task Delete(int id);

        List<Restaurant> GetAll();

        Restaurant GetById(int id);

        Task Update(int id, Restaurant restaurant);
    }
}