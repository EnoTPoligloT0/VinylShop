using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemEntity>
{
    public void Configure(EntityTypeBuilder<OrderItemEntity> orderItemBuilder)
    {
        orderItemBuilder.ToTable("OrderItems").HasKey(oi => oi.Id);
        orderItemBuilder.Property(oi => oi.Quantity).IsRequired();
        orderItemBuilder.Property(oi => oi.UnitPrice).IsRequired().HasColumnType("decimal(18,2)");

        orderItemBuilder.HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId);

        orderItemBuilder.HasOne(oi => oi.Vinyl)
            .WithMany(v => v.OrderItems)
            .HasForeignKey(oi => oi.VinylId);
    }
}