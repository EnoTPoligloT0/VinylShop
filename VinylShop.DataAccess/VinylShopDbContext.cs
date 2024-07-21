using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess;

public class VinylShopDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public VinylShopDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(_configuration.GetConnectionString("Database"))
            .UseLoggerFactory(CreateLoggerFactory())
            .EnableSensitiveDataLogging();
    }

    public ILoggerFactory CreateLoggerFactory() =>
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VinylEntity>(
            vinylBuilder =>
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
                vinylBuilder.HasOne(v => v.OrderItem)
                    .WithMany(o => o.Vinyls)
                    .HasForeignKey(v => v.OrderItemId);
            });
        
        modelBuilder.Entity<UserEntity>(
            userBuilder =>
            {
                userBuilder.ToTable("Users").HasKey(u => u.UserId);
                userBuilder.Property(u => u.FirstName).IsRequired().HasMaxLength(100);
                userBuilder.Property(u => u.LastName).IsRequired().HasMaxLength(100);
                userBuilder.Property(u => u.PasswordHash).IsRequired();
                userBuilder.Property(u => u.Email).IsRequired().HasMaxLength(100);
                userBuilder.Property(u => u.PhoneNumber).HasMaxLength(15);
                userBuilder.Property(u => u.AddressLine1).HasMaxLength(200);
                userBuilder.Property(u => u.AddressLine2).HasMaxLength(200);
                userBuilder.Property(u => u.City).HasMaxLength(100);
                userBuilder.Property(u => u.State).HasMaxLength(100);
                userBuilder.Property(u => u.ZipCode).HasMaxLength(20);

                userBuilder.HasMany(u => u.Orders)
                    .WithOne(o => o.User)
                    .HasForeignKey(o => o.UserId);
            });
    }
}