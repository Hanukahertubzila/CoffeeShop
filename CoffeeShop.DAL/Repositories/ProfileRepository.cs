using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DAL.Repositories
{
    public class ProfileRepository : IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public async Task<bool> Create(Profile entity)
        {
            await _context.Profiles.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;    
        }

        public async Task<bool> Delete(Profile entity)
        {
            _context.Profiles.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Profile>> GetAll()
        {
            var profiles = await _context.Profiles.ToListAsync();
            return profiles;
        }

        public async Task<Profile> Update(Profile entity)
        {
            _context.Profiles.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Profile> GetProfile(int id)
        {
            var profile = await _context.Profiles.FirstOrDefaultAsync(x => x.Id == id);
            return profile;
        }
    }
}
