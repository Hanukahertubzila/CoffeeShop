using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;

namespace CoffeeShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<BaseResponce<Product>> GetById(int id);

        Task<BaseResponce<Product>> GetByName(string name);

        Task<BaseResponce<List<List<Product>>>> GetAllByCategory();

        Task<BaseResponce<bool>> Create(ProductViewModel viewModel);

        Task<BaseResponce<bool>> Delete(int id);

        Task<BaseResponce<Product>> Edit(int id, ProductViewModel model);
    }
}
