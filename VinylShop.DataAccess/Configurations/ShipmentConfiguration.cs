using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Configurations;

public class ShipmentConfiguration : IEntityTypeConfiguration<ShipmentEntity>
{
    public void Configure(EntityTypeBuilder<ShipmentEntity> shipmentBuilder)
    {
        shipmentBuilder.ToTable("Shipments").HasKey(s => s.ShipmentId);
        shipmentBuilder.Property(s => s.ShipmentDate).IsRequired();
        shipmentBuilder.Property(s => s.TrackingNumber).IsRequired().HasMaxLength(100);
        shipmentBuilder.Property(s => s.ShipmentStatus).IsRequired().HasMaxLength(50);

        shipmentBuilder.HasOne(s => s.Order)
            .WithMany(o => o.Shipments)
            .HasForeignKey(s => s.OrderId);
    }
    
}