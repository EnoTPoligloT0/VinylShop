using Microsoft.EntityFrameworkCore;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess;

public class VinylShopDbContext : DbContext
{
    public VinylShopDbContext(DbContextOptions<VinylShopDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }
    public DbSet<VinylEntity> Vinyls { get; set; }
    public DbSet<ShipmentEntity> Shipment { get; set; }
    public DbSet<PaymentEntity> Payment { get; set; }
}