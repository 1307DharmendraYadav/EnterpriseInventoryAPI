using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Seed.Security;

/// <summary>
/// Seeds the default Role-Permission mappings.
///
/// The Administrator role is intentionally granted all
/// application permissions so that a freshly deployed
/// environment is immediately manageable without requiring
/// manual database updates.
///
/// Other default roles are seeded with their predefined
/// permission sets to provide a consistent RBAC baseline
/// across all environments.
/// </summary>
public static class RolePermissionSeeder
{
    public static async Task InitializeAsync(ApplicationDbContext context)
    {
        // ============================================================
        // PREVENT DUPLICATE ROLE-PERMISSION SEEDING
        // ============================================================

        if (await context.RolePermissions.AnyAsync())
        {
            return;
        }

        // ============================================================
        // LOAD ROLES
        // ============================================================

        var roles = await context.Roles
            .ToDictionaryAsync(
                role => role.Name,
                role => role);

        // ============================================================
        // LOAD PERMISSIONS
        // ============================================================

        var permissions = await context.Permissions
            .ToDictionaryAsync(
                permission => permission.Name,
                permission => permission);

        // ============================================================
        // DEFAULT ROLE-PERMISSION MAPPINGS
        //
        // These mappings bootstrap the application so that a
        // fresh installation has a fully functional Administrator
        // account and predefined business roles.
        // ============================================================

        IReadOnlyDictionary<string, IReadOnlyList<string>> rolePermissions =
            new Dictionary<string, IReadOnlyList<string>>
            {
                ["Admin"] =
                [
                    // ========================================================
                    // PRODUCT
                    // ========================================================
                    "Product.View",
                    "Product.Create",
                    "Product.Update",
                    "Product.Delete",

                    // ========================================================
                    // USER
                    // ========================================================
                    "User.View",
                    "User.Create",
                    "User.Update",
                    "User.Delete",

                    // ========================================================
                    // ROLE
                    // ========================================================
                    "Role.View",
                    "Role.Create",
                    "Role.Update",
                    "Role.Delete",

                    // ========================================================
                    // PERMISSION
                    // ========================================================
                    "Permission.View",
                    "Permission.Create",
                    "Permission.Update",
                    "Permission.Delete"
                ],

                ["Manager"] =
                [
                    "Product.View",
                    "Product.Create",
                    "Product.Update",
                    "User.View"
                ],

                ["Operator"] =
                [
                    "Product.View",
                    "Product.Update"
                ],

                ["Viewer"] =
                [
                    "Product.View",
                    "User.View"
                ]
            };

        // ============================================================
        // BUILD ROLE-PERMISSION MAPPINGS
        // ============================================================

        var mappings = new List<RolePermission>();

        foreach (var rolePermission in rolePermissions)
        {
            foreach (var permissionName in rolePermission.Value)
            {
                mappings.Add(
                    new RolePermission
                    {
                        RoleId = roles[rolePermission.Key].Id,
                        PermissionId = permissions[permissionName].Id
                    });
            }
        }

        // ============================================================
        // SAVE ROLE-PERMISSION MAPPINGS
        // ============================================================

        await context.RolePermissions.AddRangeAsync(mappings);

        await context.SaveChangesAsync();
    }
}