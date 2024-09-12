using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylShop.DataAccess.Entities;

namespace VinylShop.DataAccess.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> userBuilder)
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
        
        
        userBuilder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRoleEntity>(
                l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(r => r.RoleId),
                r => r.HasOne<UserEntity>().WithMany().HasForeignKey(u => u.UserId));
    }
}