using CoffeeShop.Domain.Enums;

namespace CoffeeShop.Domain
{
    public class BaseResponce<T>
    {
        public string Description { get; set; }

        public StatusCode StatusCode { get; set; }

        public T Data { get; set; }
    }
}
