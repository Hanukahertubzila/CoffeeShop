using CoffeeShop.DAL.Interfaces;
using CoffeeShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        private readonly ApplicationDbContext _context;

        public async Task<bool> Create(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Product entity)
        {
            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAll()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }

        public async Task<Product> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }

        public async Task<Product> GetByName(string name)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Name.Contains(name));
            return product;
        }

        public async Task<List<List<Product>>> GetAllByCategory()
        {
            var products = await _context.Products.GroupBy(x => x.Category).Select(z => z.ToList()).ToListAsync();
            return products;
        }

        public async Task<Product> Update(Product entity)
        {
            _context.Products.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
