namespace CoffeeShop.Domain.Entity
{
    public class Product
    {
        public int Id {  get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageURL {  get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public int Discount { get; set; }

        public int Weight { get; set; }

        public string Units { get; set; }

        public bool InStock { get; set; }

        public double Proteins { get; set; }

        public double Fats { get; set; }

        public double Carbohydrates { get; set; }

        public bool Lactose { get; set; }
         
        public bool Caffeine { get; set; }

        public bool Gluten { get; set; }

        public bool Sugar { get; set; }

    }
}
