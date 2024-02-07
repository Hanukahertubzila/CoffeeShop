using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DAL.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public async Task<bool> Create(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Delete(Order entity)
        {
            _context.Orders.Remove(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<Order>> GetAll()
        {
            var orders = await _context.Orders.ToListAsync();
            return orders;
        }

        public async Task<Order> Update(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Order>> GetOrdersByUserId(int userId)
        {
            var orders = await _context.Orders.Where(x => x.UserId == userId && x.Finished).ToListAsync();
            return orders;
        }

        public async Task<Order?> GetCurrentOrder(int userId)
        {
            var current = await _context.Orders.FirstOrDefaultAsync(x => x.UserId == userId && !x.Finished);
            return current;
        }
    }
}
