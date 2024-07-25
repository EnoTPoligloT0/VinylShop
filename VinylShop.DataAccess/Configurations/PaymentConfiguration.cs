using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<PaymentEntity>
{
    public void Configure(EntityTypeBuilder<PaymentEntity> paymentBuilder)
    {
        paymentBuilder.ToTable("Payments").HasKey(p => p.PaymentId);
        paymentBuilder.Property(p => p.PaymentId).IsRequired();
        paymentBuilder.Property(p => p.OrderId).IsRequired();
        paymentBuilder.Property(p => p.PaymentDate).IsRequired();
        paymentBuilder.Property(p => p.Amount).IsRequired().HasColumnType("decimal(18,2)");
        paymentBuilder.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);
    
        paymentBuilder.HasOne(p => p.Order)
            .WithMany(o => o.Payments) 
            .HasForeignKey(p => p.OrderId);
    }
}