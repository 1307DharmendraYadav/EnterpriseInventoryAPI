using EnterpriseInventory.Application.Common.Settings;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EnterpriseInventory.Infrastructure.Seed.Security;

public static class UserRoleSeeder
{
    public static async Task InitializeAsync(
        ApplicationDbContext context,
        IOptions<DefaultAdminSettings> adminOptions)
    {
        // ============================================================
        // LOAD DEFAULT ADMIN CONFIGURATION
        // ============================================================

        DefaultAdminSettings admin = adminOptions.Value;

        // ============================================================
        // FIND ADMIN USER
        // ============================================================

        User? adminUser = await context.Users
            .SingleOrDefaultAsync(u => u.Email == admin.Email);

        if (adminUser is null)
        {
            return;
        }

        // ============================================================
        // FIND ADMIN ROLE
        // ============================================================

        Role? adminRole = await context.Roles
            .SingleOrDefaultAsync(r => r.Name == "Admin");

        if (adminRole is null)
        {
            return;
        }

        // ============================================================
        // PREVENT DUPLICATE ROLE ASSIGNMENT
        // ============================================================

        bool mappingExists = await context.UserRoles.AnyAsync(ur =>
            ur.UserId == adminUser.Id &&
            ur.RoleId == adminRole.Id);

        if (mappingExists)
        {
            return;
        }

        // ============================================================
        // ASSIGN ADMIN ROLE TO DEFAULT ADMIN USER
        // ============================================================

        UserRole userRole = new()
        {
            UserId = adminUser.Id,
            RoleId = adminRole.Id
        };

        await context.UserRoles.AddAsync(userRole);

        await context.SaveChangesAsync();
    }
}