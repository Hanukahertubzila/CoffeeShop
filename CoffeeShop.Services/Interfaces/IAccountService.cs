using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using System.Security.Claims;

namespace CoffeeShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponce<ClaimsIdentity>> Register(UserRegisterViewModel model);

        Task<BaseResponce<ClaimsIdentity>> Login(UserLoginViewModel model);

        Task<BaseResponce<User>> GetUserByEmail(string email);
    }
}
