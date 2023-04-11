using WebApiPlayground.Entities;
using WebApiPlayground.Models;
using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Services
{
    public interface IRestaurantService
    {
        void AddRestaurant(Restaurant restaurant);

        void Delete(int id);

        PageResult<RestaurantDto> GetAll(RestaurantQuery query);

        Restaurant GetById(int id);

        void Update(int id, Restaurant restaurant);
    }
}