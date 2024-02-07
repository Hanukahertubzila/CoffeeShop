namespace CoffeeShop.Domain.Entity
{
    public class Profile
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public int Bonuses { get; set; }
    }
}
