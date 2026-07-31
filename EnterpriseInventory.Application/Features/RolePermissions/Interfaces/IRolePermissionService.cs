using EnterpriseInventory.Application.Features.RolePermissions.DTOs;

namespace EnterpriseInventory.Application.Features.RolePermissions.Interfaces;

/// <summary>
/// Provides business operations for assigning permissions to roles.
/// </summary>
public interface IRolePermissionService
{
    /// <summary>
    /// Gets all permissions assigned to the specified role.
    /// </summary>
    Task<RolePermissionResponse> GetByRoleIdAsync(int roleId);

    /// <summary>
    /// Replaces all permissions assigned to the specified role.
    /// Existing assignments are removed before the new assignments are saved.
    /// </summary>
    Task AssignPermissionsAsync(
        int roleId,
        AssignPermissionsRequest request);
}