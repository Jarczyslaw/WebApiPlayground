using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApiPlayground.Configuration;
using WebApiPlayground.Entities;
using WebApiPlayground.Exceptions;
using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Services
{
    public class AccountService : IAccountService
    {
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly RestaurantDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountService(
            AuthenticationSettings authenticationSettings,
            RestaurantDbContext context,
            IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
        }

        public string GenerateJwt(LoginDto dto)
        {
            var user = _context.Users
                .Include(x => x.Role)
                .FirstOrDefault(x => x.Email == dto.Email);

            if (user == null) { throw new BadRequestException("Invalid user of password"); }

            var validationResult = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);

            if (validationResult == PasswordVerificationResult.Failed) { throw new BadRequestException("Invalid user of password"); }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
                new Claim(ClaimTypes.Role, user.Role.Name),
                new Claim("DateOfBirth", user.DateOfBirth?.ToString("yyyy-MM-dd")),
                new Claim("Nationality", user.Nationality),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(
                _authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                null,
                expires,
                credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
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

            newUser.Password = _passwordHasher.HashPassword(newUser, user.Password);

            _context.Users.Add(newUser);
            _context.SaveChanges();
        }
    }
}