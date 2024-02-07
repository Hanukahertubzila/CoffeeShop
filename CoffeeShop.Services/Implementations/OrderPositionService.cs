using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.Services.Implementations
{
    public class OrderPositionService : IOrderPositionService
    {
        public OrderPositionService(ILogger<IOrderPositionService> logger, IOrderPositionRepository repository)
        {
            _logger = logger;
            _orderPositionRepsoitory = repository;
        }

        private readonly ILogger<IOrderPositionService> _logger;
        private readonly IOrderPositionRepository _orderPositionRepsoitory;

        public async Task<BaseResponce<bool>> Create(OrderPositionViewModel model)
        {
            try
            {
                var position = new OrderPosition()
                {
                    OrderId = model.OrderId,
                    ProductId = model.ProductId,
                    Quantity = model.Quantity
                };
                var data = await _orderPositionRepsoitory.Create(position);
                return new BaseResponce<bool>()
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.Success 
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[Create] : " + ex);
                return new BaseResponce<bool>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[Create] : " + ex
                };
            }
        }

        public async Task<BaseResponce<bool>> Delete(int id)
        {
            try
            {
                var position = await _orderPositionRepsoitory.GetById(id);
                if (position == null)
                {
                    return new BaseResponce<bool>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Position was not found"
                    };
                }
                var data = await _orderPositionRepsoitory.Delete(position);
                return new BaseResponce<bool>()
                {
                    Data = data,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[Delete] : " + ex);
                return new BaseResponce<bool>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[Delete] : " + ex
                };
            }
        }

        public async Task<BaseResponce<List<OrderPosition>>> GetPositionsByOrderId(int orderId)
        {
            try
            {
                var positions = await _orderPositionRepsoitory.GetByOrderId(orderId);
                return new BaseResponce<List<OrderPosition>>()
                {
                    Data = positions,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetPositionsByOrderId] : " + ex);
                return new BaseResponce<List<OrderPosition>>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[GetPositionsByOrderId] : " + ex
                };
            }
        }

        public async Task<BaseResponce<OrderPosition>> GetPositionById(int id)
        {
            try
            {
                var position = await _orderPositionRepsoitory.GetById(id);
                if (position == null)
                {
                    return new BaseResponce<OrderPosition>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Position was not found"
                    };
                }
                return new BaseResponce<OrderPosition>()
                {
                    Data = position,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetPositionById] : " + ex);
                return new BaseResponce<OrderPosition>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[GetPositionById] : " + ex
                };
            }
        }
    }
}
