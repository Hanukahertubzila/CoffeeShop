using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class OrderController : Controller
    {
        public OrderController(IAccountService accountService,
            IProductService productService,
            IOrderService orderService, 
            IOrderPositionService orderPositionService,
            IProfileService profileService)
        {
            _accountService = accountService;
            _productService = productService;
            _orderService = orderService;
            _orderPositionService = orderPositionService;
            _profileService = profileService;
        }

        private readonly IAccountService _accountService;
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IOrderPositionService _orderPositionService;
        private readonly IProfileService _profileService;

        [HttpGet]
        public async Task<IActionResult> GetCurrentOrder()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var orderResponse = await _orderService.GetCurrentOrder(user.Id);
            if (orderResponse.StatusCode == Domain.Enums.StatusCode.Success)
            {
                var positionsResponce = await _orderPositionService.GetPositionsByOrderId(orderResponse.Data.Id);
                if (positionsResponce.StatusCode == Domain.Enums.StatusCode.Success)
                {
                    var model = new List<BasketOrderPositionViewModel>();
                    foreach (var e in positionsResponce.Data)
                    {
                        var productResponce = await _productService.GetById(e.ProductId);
                        if (productResponce.StatusCode == Domain.Enums.StatusCode.Success)
                        {
                            decimal price = (1 - ((decimal)productResponce.Data.Discount / 100)) * productResponce.Data.Price;
                            model.Add(new BasketOrderPositionViewModel
                            {
                                Id = e.Id,
                                Quantity = e.Quantity,
                                Price = price * e.Quantity,
                                Name = productResponce.Data.Name
                            });
                        }
                    }
                    return View(model);
                }
            }
            return View();
        }

        public async Task<IActionResult> FinishCurrentOrder()
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var orderResponse = await _orderService.GetCurrentOrder(user.Id);
            if (orderResponse.StatusCode == Domain.Enums.StatusCode.Success)
            {
                var order = orderResponse.Data;
                var orderPositionsResponse = await _orderPositionService.GetPositionsByOrderId(order.Id);
                if (orderPositionsResponse.StatusCode == Domain.Enums.StatusCode.Success)
                {
                    if (orderPositionsResponse.Data.Count > 0)
                    {
                        foreach (var p in orderPositionsResponse.Data)
                        {
                            var productResponce = await _productService.GetById(p.ProductId);
                            if (productResponce.StatusCode == Domain.Enums.StatusCode.Success)
                            {
                                order.TotalPrice += (1 - ((decimal)productResponce.Data.Discount) / 100) * productResponce.Data.Price * p.Quantity;
                            }
                        }
                        var profileResponse = await _profileService.GetProfile(user.Id);
                        if (profileResponse.StatusCode == Domain.Enums.StatusCode.Success)
                        {
                            var bonusCount = (int)(order.TotalPrice / 10);
                            await _profileService.AddBonuses(bonusCount, profileResponse.Data);
                        }
                        order.Finished = true;
                        var updateRespose = await _orderService.Update(order);
                        if (updateRespose.StatusCode == Domain.Enums.StatusCode.Success)
                        {
                            return RedirectToAction("GetProfile", "Profile");
                        }
                    }
                }
            }
            return RedirectToAction("GetCurrentOrder", "Order");
        }

        private async Task<User?> GetCurrentUser()
        {
            var responseUser = await _accountService.GetUserByEmail(User.Identity.Name);
            if (responseUser.StatusCode == Domain.Enums.StatusCode.Success)
            {
                return responseUser.Data;
            }
            return null;
        }
    }
}
