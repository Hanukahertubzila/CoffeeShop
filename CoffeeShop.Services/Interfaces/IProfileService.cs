using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;

namespace CoffeeShop.Services.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponce<Profile>> GetProfile(int id);

        Task<BaseResponce<List<Profile>>> GetProfiles();

        Task<BaseResponce<Profile>> AddBonuses(int bonusCount, Profile profile);

        Task<BaseResponce<bool>> CreateProfile(ProfileViewModel model);

        Task<BaseResponce<Profile>> UpdateProfile(ProfileViewModel model, int id);
    }
}
