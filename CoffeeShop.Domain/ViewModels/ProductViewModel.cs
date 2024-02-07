using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Domain.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [MinLength(1)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите описание")]
        [MinLength(1)]
        [MaxLength(255)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Введите адрес изображения")]
        [MinLength(1)]
        [MaxLength(50)]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "Введите название категории")]
        [MinLength(1)]
        [MaxLength(50)]
        public string Category { get; set; }

        [Required(ErrorMessage = "Введите цену")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Введите скидку")]
        [Range(0, 100)]
        public int Discount { get; set; }

        [Required(ErrorMessage = "Введите вес")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "Введите единицы измерения")]
        public string Units { get; set; }

        public bool InStock { get; set; }

        [Required(ErrorMessage = "Введите количество белка")]
        public double Proteins { get; set; }

        [Required(ErrorMessage = "Введите количество жиров")]
        public double Fats { get; set; }

        [Required(ErrorMessage = "Введите количество углеводов")]
        public double Carbohydrates { get; set; }

        public bool Lactose { get; set; }

        public bool Caffeine { get; set; }

        public bool Gluten { get; set; }

        public bool Sugar { get; set; }
    }
}
