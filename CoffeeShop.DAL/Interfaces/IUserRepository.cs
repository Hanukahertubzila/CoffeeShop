using CoffeeShop.Domain.Entity;

namespace CoffeeShop.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User> GetByEmail(string email);
    }
}
