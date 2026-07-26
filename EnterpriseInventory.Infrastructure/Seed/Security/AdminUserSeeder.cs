using EnterpriseInventory.Application.Common.Settings;
using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace EnterpriseInventory.Infrastructure.Seed.Security;

public static class AdminUserSeeder
{
    public static async Task InitializeAsync(
        ApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IOptions<DefaultAdminSettings> adminOptions)
    {
        // ============================================================
        // LOAD DEFAULT ADMIN CONFIGURATION
        // ============================================================

        DefaultAdminSettings admin = adminOptions.Value;

        // ============================================================
        // PREVENT DUPLICATE ADMIN USER CREATION
        // ============================================================

        bool adminExists = await context.Users.AnyAsync(u =>
            u.Email == admin.Email);

        if (adminExists)
        {
            return;
        }

        // ============================================================
        // CREATE DEFAULT ADMIN USER
        // ============================================================

        User user = new()
        {
            Username = admin.Username,
            Email = admin.Email,
            PasswordHash = passwordHasher.Hash(admin.Password),
            IsActive = admin.IsActive,
            CreatedAtUtc = DateTime.UtcNow
        };

        await context.Users.AddAsync(user);

        await context.SaveChangesAsync();
    }
}