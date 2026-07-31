namespace EnterpriseInventory.Application.Features.RolePermissions.DTOs;

/// <summary>
/// Represents a permission assigned to a role.
/// </summary>
public sealed class AssignedPermissionResponse
{
    /// <summary>
    /// Gets or sets the Permission Id.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the Permission Name.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}