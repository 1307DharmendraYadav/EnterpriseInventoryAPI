using EnterpriseInventory.Infrastructure.Persistence.Context;
using EnterpriseInventory.Infrastructure.Seed.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using EnterpriseInventory.Application.Common.Settings;
using EnterpriseInventory.Application.Interfaces.Security;
using Microsoft.Extensions.Options;

namespace EnterpriseInventory.Infrastructure.Seed;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        // Create a temporary DI scope for startup database operations.
        using var scope = serviceProvider.CreateScope();

        // Resolve the scoped DbContext.
        var context = scope.ServiceProvider
            .GetRequiredService<ApplicationDbContext>();

        // Resolve required services.
        var passwordHasher = scope.ServiceProvider
            .GetRequiredService<IPasswordHasher>();

        var adminSettings = scope.ServiceProvider
            .GetRequiredService<IOptions<DefaultAdminSettings>>();


        // Apply any pending EF Core migrations.
        await context.Database.MigrateAsync();

        // ============================================================
        // SEED SECURITY DATA
        // ============================================================

        await RoleSeeder.InitializeAsync(context);

        await PermissionSeeder.InitializeAsync(context);

        await RolePermissionSeeder.InitializeAsync(context);

        await AdminUserSeeder.InitializeAsync(context, passwordHasher, adminSettings);

        await UserRoleSeeder.InitializeAsync(context,adminSettings);

        // ============================================================
        //  REMAINING SECURITY SEEDERS
        // ============================================================


        // await UserRoleSeeder.InitializeAsync(context);

        // ============================================================
        // FUTURE MASTER DATA SEEDERS
        // ============================================================

        // await CountrySeeder.InitializeAsync(context);
        // await CurrencySeeder.InitializeAsync(context);
        // await DepartmentSeeder.InitializeAsync(context);
    }
}