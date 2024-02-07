using CoffeeShop.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeShop.DAL.Configurations
{
    public class ProfilesConfiguration : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Address).HasMaxLength(50).IsRequired();
            builder.Property(x => x.BirthDate).IsRequired();
            builder.Property(x => x.Bonuses).IsRequired();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public int Bonuses { get; set; }
    }
}
