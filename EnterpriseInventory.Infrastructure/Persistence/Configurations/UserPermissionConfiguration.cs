using EnterpriseInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnterpriseInventory.Infrastructure.Persistence.Configurations;

public sealed class UserPermissionConfiguration
    : IEntityTypeConfiguration<UserPermission>
{
    public void Configure(EntityTypeBuilder<UserPermission> builder)
    {
        builder.ToTable("UserPermissions");

        builder.HasKey(up => up.Id);

        builder.Property(up => up.IsAllowed)
               .IsRequired();

        builder.HasOne(up => up.User)
               .WithMany(u => u.UserPermissions)
               .HasForeignKey(up => up.UserId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(up => up.Permission)
               .WithMany(p => p.UserPermissions)
               .HasForeignKey(up => up.PermissionId)
               .OnDelete(DeleteBehavior.Restrict);

        // One override per User-Permission pair
        builder.HasIndex(up => new
        {
            up.UserId,
            up.PermissionId
        })
        .IsUnique();
    }
}