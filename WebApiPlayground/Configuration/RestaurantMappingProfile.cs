using AutoMapper;
using WebApiPlayground.Entities;
using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Configuration
{
    public class RestaurantMappingProfile : Profile
    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(x => x.City, x => x.MapFrom(y => y.Address.City))
                .ForMember(x => x.Street, x => x.MapFrom(y => y.Address.Street))
                .ForMember(x => x.PostalCode, x => x.MapFrom(y => y.Address.PostalCode));

            CreateMap<Dish, DishDto>();
        }
    }
}