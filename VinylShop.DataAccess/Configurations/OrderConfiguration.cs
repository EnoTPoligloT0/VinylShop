using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<OrderEntity>
{
    public void Configure(EntityTypeBuilder<OrderEntity> orderBuilder)
    {
        orderBuilder.ToTable("Orders").HasKey(o => o.Id);
        orderBuilder.Property(o => o.UserId)
            .IsRequired();
        orderBuilder.Property(o => o.OrderDate)
            .IsRequired();
        orderBuilder.Property(o => o.TotalAmount)
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        orderBuilder.Property(o => o.Status)
            .IsRequired()
            .HasConversion<int>();

        orderBuilder.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId);
                
        orderBuilder.HasMany(o => o.OrderItems)
            .WithOne(oi => oi.Order)
            .HasForeignKey(oi => oi.OrderId);

        orderBuilder.HasMany(o => o.Shipments)
            .WithOne(s => s.Order)
            .HasForeignKey(s => s.OrderId);
                
        orderBuilder.HasMany(o => o.Payments)
            .WithOne(p => p.Order)
            .HasForeignKey(p => p.OrderId);
    }
}