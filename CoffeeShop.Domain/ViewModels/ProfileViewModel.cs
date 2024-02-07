using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Domain.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите дату рождения")]
        public DateTime BirthDate { get; set; }

        public int Bonuses { get; set; }

        [Required(ErrorMessage = "Введите адрес")]
        [MinLength(5)]
        [MaxLength(50)]
        public string Address { get; set; }
    }
}
