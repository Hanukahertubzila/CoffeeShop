using CoffeeShop.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Domain.ViewModels
{
    public class UserRegisterViewModel
    {
        [Required(ErrorMessage = "Введите email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [MaxLength(20, ErrorMessage = "Слишком длинный пароль")]
        [MinLength(5, ErrorMessage = "Слишком слабый пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Пароли не совпадают")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirmation { get; set; }
    }
}
