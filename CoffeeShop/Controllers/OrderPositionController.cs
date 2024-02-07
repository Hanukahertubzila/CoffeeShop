using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Services.Implementations;
using CoffeeShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class OrderPositionController : Controller
    {
        public OrderPositionController(IOrderService orderService, 
            IOrderPositionService orderPositionService,
            IAccountService accountService)
        {
            _orderService = orderService;
            _orderPositionService = orderPositionService;
            _accountService = accountService;
        }

        private readonly IAccountService _accountService;
        private readonly IOrderService _orderService;
        private readonly IOrderPositionService _orderPositionService;

        [HttpPost]
        public async Task<IActionResult> AddPosition(OrderPositionViewModel model)
        {
            var user = await GetCurrentUser();
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }
            var currentOrder = await _orderService.GetCurrentOrder(user.Id);
            if (currentOrder.StatusCode == Domain.Enums.StatusCode.NullRecieved)
            {
                await _orderService.Create(user.Id);
                currentOrder = await _orderService.GetCurrentOrder(user.Id);
            }
            if (currentOrder.StatusCode == Domain.Enums.StatusCode.Success)
            {
                model.OrderId = currentOrder.Data.Id;
                var created = await _orderPositionService.Create(model);
                if (created.StatusCode == Domain.Enums.StatusCode.Success)
                {
                    return Json(created.Description);
                }
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        public async Task<IActionResult> RemovePosition(int id)
        {
            var response = await _orderPositionService.Delete(id);
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
            {
                return Json(response.Description);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
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
