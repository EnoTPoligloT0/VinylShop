using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VinylShop.DataAccess.Configurations;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess;

public class VinylShopDbContext : DbContext
{
    public VinylShopDbContext(DbContextOptions<VinylShopDbContext> options)
        : base(options)
    {
    }

    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }
    public DbSet<PaymentEntity> Payment { get; set; }
    public DbSet<ShipmentEntity> Shipment { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<VinylEntity> Vinyls { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new OrderConfiguration());
        modelBuilder.ApplyConfiguration(new OrderItemConfiguration());
        modelBuilder.ApplyConfiguration(new VinylConfiguration());
        modelBuilder.ApplyConfiguration(new ShipmentConfiguration());
        modelBuilder.ApplyConfiguration(new PaymentConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
