namespace CoffeeShop.Domain.ViewModels
{
    public class BasketOrderPositionViewModel
    {
        public int Id { get; set; }

        public int Quantity { get; set;}

        public decimal Price { get; set;}

        public string Name { get; set;}
    }
}
