using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Seed.Security;

public static class RoleSeeder
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        // ============================================================
        // PREVENT DUPLICATE ROLE SEEDING
        // ============================================================

        if (await context.Roles.AnyAsync())
        {
            return;
        }

        // ============================================================
        // DEFAULT SYSTEM ROLES
        // ============================================================

        IReadOnlyList<Role> roles =
        [
            new()
            {
                Name = "Admin",
                Description = "System Administrator"
            },
            new()
            {
                Name = "Manager",
                Description = "Business Manager"
            },
            new()
            {
                Name = "Operator",
                Description = "Operations User"
            },
            new()
            {
                Name = "Viewer",
                Description = "Read Only User"
            }
        ];

        await context.Roles.AddRangeAsync(roles);

        await context.SaveChangesAsync();
    }
}