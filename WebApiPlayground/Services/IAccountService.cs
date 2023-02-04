using WebApiPlayground.Models.Dtos;

namespace WebApiPlayground.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto user);
    }
}