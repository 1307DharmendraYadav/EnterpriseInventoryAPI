namespace EnterpriseInventory.Application.Features.RolePermissions.DTOs;

/// <summary>
/// Represents all permissions assigned to a role.
/// </summary>
public class RolePermissionResponse
{
    /// <summary>
    /// Gets or sets the Role Id.
    /// </summary>
    public int RoleId { get; set; }

    /// <summary>
    /// Gets or sets the Role Name.
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the assigned permissions.
    /// </summary>
    public IEnumerable<AssignedPermissionResponse> Permissions { get; set; }= [];
}