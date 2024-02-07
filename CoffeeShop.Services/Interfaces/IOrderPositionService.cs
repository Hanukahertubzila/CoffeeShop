using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;

namespace CoffeeShop.Services.Interfaces
{
    public interface IOrderPositionService
    {
        Task<BaseResponce<bool>> Create(OrderPositionViewModel model);

        Task<BaseResponce<bool>> Delete(int id);

        Task<BaseResponce<List<OrderPosition>>> GetPositionsByOrderId(int orderId);

        Task<BaseResponce<OrderPosition>> GetPositionById(int id);
    }
}
