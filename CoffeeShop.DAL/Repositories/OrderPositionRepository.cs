using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DAL.Repositories
{
    public class OrderPositionRepository : IOrderPositionRepository
    {
        public OrderPositionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public async Task<bool> Create(OrderPosition entity)
        {
            await _context.OrderPositions.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(OrderPosition entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<OrderPosition>> GetAll()
        {
            var positions = await _context.OrderPositions.ToListAsync();
            return positions;
        }

        public async Task<OrderPosition> Update(OrderPosition entity)
        {
            _context.OrderPositions.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<OrderPosition>> GetByOrderId(int id)
        {
            var positions = await _context.OrderPositions.Where(x => x.OrderId == id).ToListAsync();
            return positions;
        }

        public async Task<OrderPosition> GetById(int id)
        {
            var position = await _context.OrderPositions.FirstOrDefaultAsync(x => x.Id == id);
            return position;
        }
    }
}
