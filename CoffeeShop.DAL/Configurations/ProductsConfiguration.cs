using CoffeeShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.DAL.Configurations
{
    public class ProductsConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Description).HasMaxLength(255).IsRequired();
            builder.Property(x => x.ImageURL).HasMaxLength(255).IsRequired();
            builder.Property(x => x.Category).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Price).IsRequired();
            builder.Property(x => x.Discount).IsRequired();
            builder.Property(x => x.Weight).IsRequired();
            builder.Property(x => x.Units).HasMaxLength(15).IsRequired();
            builder.Property(x => x.InStock).IsRequired();
            builder.Property(x => x.Proteins).IsRequired();
            builder.Property(x => x.Fats).IsRequired();
            builder.Property(x => x.Carbohydrates).IsRequired();
            builder.Property(x => x.Lactose).IsRequired();
            builder.Property(x => x.Caffeine).IsRequired();
            builder.Property(x => x.Gluten).IsRequired();
            builder.Property(x => x.Sugar).IsRequired();
        }
    }
}
