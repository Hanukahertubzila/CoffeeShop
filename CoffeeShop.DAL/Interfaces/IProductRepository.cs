using CoffeeShop.Domain.Entity;

namespace CoffeeShop.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<Product> GetById(int id);

        Task<Product> GetByName(string name);

        Task<List<List<Product>>> GetAllByCategory();
    }
}
