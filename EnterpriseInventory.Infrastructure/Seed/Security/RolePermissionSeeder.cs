using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Seed.Security;

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
        // DEFINE ROLE-PERMISSION MAPPINGS
        // ============================================================

        IReadOnlyDictionary<string, IReadOnlyList<string>> rolePermissions =
            new Dictionary<string, IReadOnlyList<string>>
            {
                ["Admin"] =
                [
                    "Product.View",
                    "Product.Create",
                    "Product.Update",
                    "Product.Delete",
                    "User.View",
                    "User.Create",
                    "User.Update",
                    "User.Delete"
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
        // BUILD ROLE-PERMISSION ENTITIES
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