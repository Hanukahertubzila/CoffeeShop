using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.ViewModels;
using CoffeeShop.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace CoffeeShop.Services.Implementations
{
    public class ProfileService : IProfileService
    {
        public ProfileService(ILogger<IProfileService> logger, IProfileRepository repository)
        {
            _logger = logger;
            _profileRepository = repository;
        }

        private readonly ILogger<IProfileService> _logger;
        private readonly IProfileRepository _profileRepository;

        public async Task<BaseResponce<bool>> CreateProfile(ProfileViewModel model)
        {
            try
            {
                var profile = new Profile();
                profile.Id = model.Id;
                profile.Name = model.Name;
                profile.Address = model.Address;
                profile.BirthDate = model.BirthDate;
                profile.Bonuses = 0;
                await _profileRepository.Create(profile);
                return new BaseResponce<bool>()
                {
                    Data = true,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[CreateProfile] : " + ex);
                return new BaseResponce<bool>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[CreateProfile] : " + ex
                };
            }
        }

        public async Task<BaseResponce<Profile>> GetProfile(int id)
        {
            try
            {
                var profile = await _profileRepository.GetProfile(id);
                if (profile == null)
                {
                    return new BaseResponce<Profile>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Not found"
                    };
                }
                return new BaseResponce<Profile>()
                {
                    Data = profile,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[GetProfile] : " + ex);
                return new BaseResponce<Profile>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[GetProfile] : " + ex
                };
            }
        }

        public Task<BaseResponce<List<Profile>>> GetProfiles()
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponce<Profile>> UpdateProfile(ProfileViewModel model, int id)
        {
            try
            {
                var profile = await _profileRepository.GetProfile(id);
                if (profile == null)
                {
                    return new BaseResponce<Profile>()
                    {
                        StatusCode = Domain.Enums.StatusCode.NullRecieved,
                        Description = "Not found"
                    };
                }
                profile.Name = model.Name;
                profile.Address = model.Address;
                profile.BirthDate = model.BirthDate;
                profile.Bonuses = model.Bonuses;
                await _profileRepository.Update(profile);
                return new BaseResponce<Profile>()
                {
                    Data = profile,
                    StatusCode = Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[UpdateProfile] : " + ex);
                return new BaseResponce<Profile>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[UpdateProfile] : " + ex
                };
            }
        }

        public async Task<BaseResponce<Profile>> AddBonuses(int bonusCount, Profile profile)
        {
            try
            {
                profile.Bonuses += bonusCount;
                await _profileRepository.Update(profile);
                return new BaseResponce<Profile>()
                {
                    Data = profile,
                    StatusCode =Domain.Enums.StatusCode.Success
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("[AddBonuses] : " + ex);
                return new BaseResponce<Profile>()
                {
                    StatusCode = Domain.Enums.StatusCode.ServerInternalError,
                    Description = "[AddBonuses] : " + ex
                };
            }
        }
    }
}
