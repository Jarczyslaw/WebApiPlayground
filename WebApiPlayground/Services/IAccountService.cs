using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Services
{
    public interface IAccountService
    {
        string GenerateJwt(LoginDto dto);

        void RegisterUser(RegisterUserDto user);
    }
}