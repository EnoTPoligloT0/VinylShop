using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Configurations;

public class VinylConfiguration : IEntityTypeConfiguration<VinylEntity>
{
    public void Configure(EntityTypeBuilder<VinylEntity> vinylBuilder)
    {
        vinylBuilder.ToTable("Vinyls").HasKey(v => v.Id);
        vinylBuilder.Property(v => v.Title).IsRequired().HasMaxLength(100);
        vinylBuilder.Property(v => v.Artist).IsRequired().HasMaxLength(100);
        vinylBuilder.Property(v => v.Genre).IsRequired().HasMaxLength(50);
        vinylBuilder.Property(v => v.ReleaseYear).IsRequired();
        vinylBuilder.Property(v => v.Price).IsRequired().HasColumnType("decimal(18,2)");
        vinylBuilder.Property(v => v.Stock).IsRequired();
        vinylBuilder.Property(v => v.Description).HasMaxLength(500);
        vinylBuilder.Property(v => v.IsAvailable).IsRequired();
        
    }
}