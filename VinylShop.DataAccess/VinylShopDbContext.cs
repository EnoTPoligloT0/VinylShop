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
        
    }
}