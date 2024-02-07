using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.Helpers;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace CoffeeShop.Services.Implementations
{
    public class AccountService : IAccountService
    {
        public AccountService(ILogger<IAccountService> logger, IUserRepository repository)
        {
            _logger = logger;
            _userRepository = repository;
        }

        private readonly ILogger<IAccountService> _logger;
        private readonly IUserRepository _userRepository;

        public async Task<BaseResponce<ClaimsIdentity>> Register(UserRegisterViewModel model)
        {
            try
            {
                var user = await _userRepository.GetByEmail(model.Email);
                if (user != null)
                {
                    return new BaseResponce<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.UserAlreadyExists,
                        Description = "Пользователь уже существует"
                    };
                }

                user = new User()
                {
                    Email = model.Email,
                    Password = HashPasswordHelper.HashPassword(model.Password)
                };

                await _userRepository.Create(user);

                var result = Authenticate(user);
                return new BaseResponce<ClaimsIdentity>
                {
                    StatusCode = StatusCode.Success,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[Register] : " + ex.Message);
                return new BaseResponce<ClaimsIdentity>()
                {
                    StatusCode = StatusCode.ServerInternalError,
                    Description = "[Register] : " + ex.Message
                };
            }
        }

        public async Task<BaseResponce<ClaimsIdentity>> Login(UserLoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetByEmail(model.Email);
                if (user == null)
                {
                    return new BaseResponce<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.UserDoesntExist,
                        Description = "Пользователь не найден"
                    };
                }

                if (user.Password != HashPasswordHelper.HashPassword(model.Password))
                {
                    return new BaseResponce<ClaimsIdentity>()
                    {
                        StatusCode = StatusCode.IncorrectPassword,
                        Description = "Неверный пароль"
                    };
                }

                var result = Authenticate(user);

                return new BaseResponce<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[Login] : " + ex.Message);
                return new BaseResponce<ClaimsIdentity>()
                {
                    StatusCode = StatusCode.ServerInternalError,
                    Description = "[Login] : " + ex.Message
                };
            }
        }

        public async Task<BaseResponce<User>> GetUserByEmail(string email)
        {
            try
            {
                var user = await _userRepository.GetByEmail(email);
                if (user == null)
                {
                    return new BaseResponce<User>()
                    {
                        StatusCode = StatusCode.NullRecieved,
                        Description = "Not found"
                    };
                }
                return new BaseResponce<User>()
                {
                    Data = user,
                    StatusCode = StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetUserByEmail] : " + ex.Message);
                return new BaseResponce<User>()
                {
                    StatusCode = StatusCode.ServerInternalError,
                    Description = "[GetUserByEmail] : " + ex.Message
                };
            }
        }

        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString())
            };
            return new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }
    }
}
