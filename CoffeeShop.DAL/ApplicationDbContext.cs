using CoffeeShop.DAL.Configurations;
using CoffeeShop.Domain.Entity;
using CoffeeShop.Domain.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CoffeeShop.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) 
        {
            Database.EnsureCreated();
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderPosition> OrderPositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductsConfiguration());
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new ProfilesConfiguration());
            modelBuilder.ApplyConfiguration(new OrdersConfiguration()); 
            modelBuilder.ApplyConfiguration(new OrderPositionsConfiguration());
        }
    }
}
