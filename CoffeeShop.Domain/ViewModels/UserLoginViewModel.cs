using CoffeeShop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Domain.ViewModels
{
    public class UserLoginViewModel
    {
        [Required(ErrorMessage = "Введите Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
