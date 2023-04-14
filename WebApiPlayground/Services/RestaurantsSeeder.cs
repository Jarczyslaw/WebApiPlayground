﻿using Microsoft.EntityFrameworkCore;
using WebApiPlayground.Entities;

namespace WebApiPlayground.Services
{
    public class RestaurantsSeeder
    {
        private readonly RestaurantDbContext _context;

        public RestaurantsSeeder(RestaurantDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            if (_context.Database.CanConnect())
            {
                var pendingMigrations = _context.Database.GetPendingMigrations();
                if (pendingMigrations.Any())
                {
                    _context.Database.Migrate();
                }

                if (!_context.Restaurants.Any())
                {
                    _context.Restaurants.AddRange(GetRestaurants());
                    _context.SaveChanges();
                }

                if (!_context.Roles.Any())
                {
                    _context.Roles.AddRange(GetRoles());
                    _context.SaveChanges();
                }
            }
        }

        private IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description =
                        "KFC (short for Kentucky Fried Chicken) is an American fast food restaurant chain headquartered in Louisville, Kentucky, that specializes in fried chicken.",
                    ContactEmail = "contact@kfc.com",
                    HasDelivery = true,
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nashville Hot Chicken",
                            Price = 10.30M,
                        },

                        new Dish()
                        {
                            Name = "Chicken Nuggets",
                            Price = 5.30M,
                        },
                    },
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Długa 5",
                        PostalCode = "30-001"
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald Szewska",
                    Category = "Fast Food",
                    Description =
                        "McDonald's Corporation (McDonald's), incorporated on December 21, 1964, operates and franchises McDonald's restaurants.",
                    ContactEmail = "contact@mcdonald.com",
                    HasDelivery = true,
                    Address = new Address()
                    {
                        City = "Kraków",
                        Street = "Szewska 2",
                        PostalCode = "30-001"
                    }
                }
            };

            return restaurants;
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role
                {
                    Name = "User",
                },
                new Role
                {
                    Name = "Manager",
                },
                new Role
                {
                    Name = "Admin"
                }
            };

            return roles;
        }
    }
}