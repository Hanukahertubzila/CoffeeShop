using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ProfileController : Controller
    {
        public ProfileController(IProfileService profileService, 
            IAccountService accountService,
            IOrderService orderService)
        {
            _profileService = profileService;
            _accountService = accountService;
            _orderService = orderService;
        }

        private readonly IProfileService _profileService;
        private readonly IAccountService _accountService;
        private readonly IOrderService _orderService;

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var profile = await GetCurrentProfile();
            if (profile != null)
            {
                var ordersResponse = await _orderService.GetOrdersByUserId(profile.Id);
                if (ordersResponse.StatusCode == Domain.Enums.StatusCode.NullRecieved)
                {
                    ordersResponse.Data = new List<Order>();
                }
                var model = new ProfileWithOrdersViewModel()
                {
                    Profile = profile,
                    Orders = ordersResponse.Data
                };
                return View(model);
            }
            else
            {
                return RedirectToAction("CreateProfile", "Profile");
            }
        }

        [HttpGet]
        public async Task<IActionResult> CreateProfile()
        {
            var profile = await GetCurrentProfile();
            if (profile != null)
                return RedirectToAction("GetProfile", "Profile", new { Id = profile.Id });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProfile(ProfileViewModel model)
        {
            var responseUser = await _accountService.GetUserByEmail(User.Identity.Name);
            if (responseUser.StatusCode == Domain.Enums.StatusCode.Success)
            {
                model.Id = responseUser.Data.Id;
                if (ModelState.IsValid)
                {
                    var responceProfile = await _profileService.CreateProfile(model);
                    if (responceProfile.StatusCode == Domain.Enums.StatusCode.Success)
                    {
                        return RedirectToAction("GetProfile", "Profile", new { Id = responseUser.Data.Id });
                    }
                    ModelState.AddModelError("", responceProfile.Description);
                }
            }
            else
            {
                ModelState.AddModelError("", responseUser.Description);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateProfile()
        {
            var profile = await GetCurrentProfile();
            return View(profile);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProfile(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _profileService.UpdateProfile(model, model.Id);
                return RedirectToAction("GetProfile");
            }
            return View();
        }

        private async Task<Profile?> GetCurrentProfile()
        {
            var responseUser = await _accountService.GetUserByEmail(User.Identity.Name);
            if (responseUser.StatusCode == Domain.Enums.StatusCode.Success)
            {
                var responseProfile = await _profileService.GetProfile(responseUser.Data.Id);
                if (responseProfile.StatusCode == Domain.Enums.StatusCode.Success)
                {
                    return responseProfile.Data;
                }
            }
            return null;
        }
    }
}
