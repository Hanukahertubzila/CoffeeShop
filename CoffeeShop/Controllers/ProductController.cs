using CoffeeShop.Domain.Enums;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Models;
using CoffeeShop.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Controllers
{
    public class ProductController : Controller
    {
        public ProductController(IProductService service)
        {
            _productService = service;
        }

        private readonly IProductService _productService;

        [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            var response = await _productService.GetById(id);
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return View(response.Data);
            return View("NotFound");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var response = await _productService.GetByName(name);
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return View(response.Data);
            return View("NotFound");
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory()
        {
            var response = await _productService.GetAllByCategory();
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
                return View(response.Data);
            return View();
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
                return View();
            var response = await _productService.GetById(id);
            if (response.StatusCode ==  Domain.Enums.StatusCode.Success)
            {
                return View(response.Data);
            }
            return View("Error");
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Id == 0)
                {
                    await _productService.Create(model);
                    return RedirectToAction("GetProductsByCategory", "Product");
                }
                else
                {
                    await _productService.Edit(model.Id, model);
                    return RedirectToAction("GetProductById", "Product", new { Id = model.Id });
                }
            }
            return View();
        }

        [Authorize(Roles = "Moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _productService.Delete(id);
            if (response.StatusCode == Domain.Enums.StatusCode.Success)
            {
                return RedirectToAction("GetProductsByCategory");
            }
            return View("Error");
        }
    }
}