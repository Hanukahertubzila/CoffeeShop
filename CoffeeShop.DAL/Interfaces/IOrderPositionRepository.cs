using CoffeeShop.Domain.Entity;

namespace CoffeeShop.DAL.Interfaces
{
    public interface IOrderPositionRepository : IBaseRepository<OrderPosition>
    {
        Task<List<OrderPosition>> GetByOrderId(int orderId);

        Task<OrderPosition> GetById(int id);
    }
}
