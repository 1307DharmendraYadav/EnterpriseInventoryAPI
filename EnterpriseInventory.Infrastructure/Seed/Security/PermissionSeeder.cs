using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Seed.Security;

public static class PermissionSeeder
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        // ============================================================
        // PREVENT DUPLICATE PERMISSION SEEDING
        // ============================================================

        if (await context.Permissions.AnyAsync())
        {
            return;
        }

        // ============================================================
        // DEFAULT SYSTEM PERMISSIONS
        // ============================================================

        IReadOnlyList<Permission> permissions =
        [
            new() { Name = "Product.View", Description = "View products" },
            new() { Name = "Product.Create", Description = "Create products" },
            new() { Name = "Product.Update", Description = "Update products" },
            new() { Name = "Product.Delete", Description = "Delete products" },

            new() { Name = "User.View", Description = "View users" },
            new() { Name = "User.Create", Description = "Create users" },
            new() { Name = "User.Update", Description = "Update users" },
            new() { Name = "User.Delete", Description = "Delete users" }
        ];

        await context.Permissions.AddRangeAsync(permissions);

        await context.SaveChangesAsync();
    }
}