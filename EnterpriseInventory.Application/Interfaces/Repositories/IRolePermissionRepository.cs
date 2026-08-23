using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Repositories;

/// <summary>
/// Provides data access operations for Role-Permission mappings.
/// </summary>
public interface IRolePermissionRepository
{
    /// <summary>
    /// Returns only the Permission Ids assigned to a role.
    ///
    /// We intentionally return List<int> instead of RolePermission
    /// because the Application layer only needs the Permission identifiers
    /// for assignment and comparison.
    ///
    /// Returning the join entity would expose unnecessary implementation
    /// details (RoleId, navigation properties, future audit fields, etc.)
    /// that the service does not require.
    ///
    /// Example:
    /// Role: Manager
    /// Returns: [1, 3, 5]
    /// </summary>
    Task<List<int>> GetPermissionIdsByRoleIdAsync(int roleId);


    /// <summary>
    /// Removes all permission assignments for the specified role.
    /// Used before saving the newly selected permission list.
    /// </summary>
    Task RemoveByRoleIdAsync(int roleId);

    /// <summary>
    /// Creates Role-Permission mappings in bulk.
    /// Each RolePermission represents one relationship between
    /// a Role and a Permission.
    /// </summary>
    Task AddRangeAsync(IEnumerable<RolePermission> rolePermissions);


    /// <summary>
    /// Returns Role-Permission mappings for multiple roles,
    /// including the associated Role and Permission entities.
    ///
    /// Used by the effective-permission calculation to determine
    /// which role granted each permission.
    /// </summary>
    Task<List<RolePermission>> GetByRoleIdsAsync(IEnumerable<int> roleIds);
}