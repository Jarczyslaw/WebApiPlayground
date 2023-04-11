using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WebApiPlayground.Entities;
using WebApiPlayground.Exceptions;
using WebApiPlayground.Models;
using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _context;
        private readonly IMapper _mapper;

        public RestaurantService(RestaurantDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {
            var baseQuery = _context.Restaurants
               .Include(x => x.Address)
               .Include(x => x.Dishes)
               .Where(x => string.IsNullOrEmpty(query.SearchPhrase)
                    || x.Name.ToLower().Contains(query.SearchPhrase.ToLower())
                    || x.Description.ToLower().Contains(query.SearchPhrase.ToLower()));

            var totalCount = baseQuery.Count();

            if (string.IsNullOrEmpty(query.SortBy))
            {
                var columns = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    [nameof(Restaurant.Name)] = x => x.Name,
                    [nameof(Restaurant.Description)] = x => x.Description,
                    [nameof(Restaurant.Category)] = x => x.Category,
                };

                var selectedColumn = columns[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.Ascending
                    ? baseQuery.OrderBy(selectedColumn)
                    : baseQuery.OrderByDescending(selectedColumn);
            }

            var restaurants = baseQuery
               .Skip(query.PageSize * (query.PageNumber - 1))
               .Take(query.PageSize)
               .ToList();

            var result = _mapper.Map<List<RestaurantDto>>(restaurants);

            return new PageResult<RestaurantDto>(result, totalCount, query.PageSize, query.PageNumber);
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