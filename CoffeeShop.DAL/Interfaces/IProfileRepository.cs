using CoffeeShop.Domain.Entity;

namespace CoffeeShop.DAL.Interfaces
{
    public interface IProfileRepository : IBaseRepository<Profile>
    {
        Task<Profile> GetProfile(int id);
    }
}
