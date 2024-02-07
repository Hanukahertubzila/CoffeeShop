using CoffeeShop.DAL.Interfaces;
using CoffeeShop.DAL.Repositories;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeShop.Services.Implementations
{
    public class OrderService : IOrderService
    {
        public OrderService(ILogger<IOrderService> logger, IOrderRepository repository)
        {
            _logger = logger;
            _orderRepository = repository;
        }

        private readonly ILogger<IOrderService> _logger;
        private readonly IOrderRepository _orderRepository;

        public async Task<BaseResponce<bool>> Create(int userId)
        {
            try
            {
                var order = new Order()
                {
                    TotalPrice = 0,
                    UserId = userId,
                    DateCreated = DateTime.Now,
                    Finished = false
                };
                var data = await _orderRepository.Create(order);
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

        public async Task<BaseResponce<bool>> Delete(int userId)
        {
            try
            {
                var order = await _orderRepository.GetCurrentOrder(userId);
                if (order == null)
                {
                    return new BaseResponce<bool>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Order was not found"
                    };
                }
                var data = await _orderRepository.Delete(order);
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

        public async Task<BaseResponce<Order>> GetCurrentOrder(int userId)
        {
            try
            {
                var order = await _orderRepository.GetCurrentOrder(userId);
                if (order == null)
                {
                    return new BaseResponce<Order>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Order was not found"
                    };
                }
                return new BaseResponce<Order>()
                {
                    Data = order,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetCurrentOrder] : " + ex);
                return new BaseResponce<Order>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[GetCurrentOrder] : " + ex
                };
            }
        }

        public async Task<BaseResponce<List<Order>>> GetOrdersByUserId(int userId)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersByUserId(userId);
                if (orders == null)
                {
                    return new BaseResponce<List<Order>>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Orders were not found"
                    };
                }
                return new BaseResponce<List<Order>>()
                {
                    StatusCode = Domain.Enums.StatusCode.Success,
                    Data = orders
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetOrdersByUserId] : " + ex);
                return new BaseResponce<List<Order>>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[GetOrdersByUserId] : " + ex
                };
            }
        }

        public async Task<BaseResponce<Order>> Update(Order order)
        {
            try
            {
                await _orderRepository.Update(order);
                return new BaseResponce<Order>()
                {
                    Data = order,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[Update] : " + ex);
                return new BaseResponce<Order>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[Update] : " + ex
                };
            }
        }
    }
}
