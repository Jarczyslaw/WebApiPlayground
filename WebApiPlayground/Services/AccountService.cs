using WebApiPlayground.Entities;
using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Services
{
    public class AccountService : IAccountService
    {
        private readonly RestaurantDbContext _context;

        public AccountService(RestaurantDbContext context)
        {
            _context = context;
        }

        public void RegisterUser(RegisterUserDto user)
        {
            var newUser = new User
            {
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Nationality = user.Nationality,
                RoleId = user.RoleId,
            };

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}