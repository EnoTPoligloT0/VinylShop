using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess;

public class VinylShopDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(IConfiguration configuration)
    {
        _configuration;
    }
    public VinylShopDbContext(DbContextOptions<VinylShopDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(.Configuration.GetConnectionString("Database"));
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }
    public DbSet<VinylEntity> Vinyls { get; set; }
    public DbSet<ShipmentEntity> Shipment { get; set; }
    public DbSet<PaymentEntity> Payment { get; set; }
}