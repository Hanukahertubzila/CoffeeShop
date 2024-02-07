using CoffeeShop.Domain.Entity;

namespace CoffeeShop.DAL.Interfaces
{
    public interface IOrderRepository : IBaseRepository<Order>
    {
        Task<List<Order>> GetOrdersByUserId(int userId);

        Task<Order?> GetCurrentOrder(int userId);
    }
}
