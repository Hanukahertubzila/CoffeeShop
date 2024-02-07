using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Domain;

namespace CoffeeShop.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BaseResponce<Order>> GetCurrentOrder(int id);

        Task<BaseResponce<List<Order>>> GetOrdersByUserId(int userId);

        Task<BaseResponce<bool>> Create(int userId);

        Task<BaseResponce<Order>> Update(Order order);

        Task<BaseResponce<bool>> Delete(int userId);
    }
}
