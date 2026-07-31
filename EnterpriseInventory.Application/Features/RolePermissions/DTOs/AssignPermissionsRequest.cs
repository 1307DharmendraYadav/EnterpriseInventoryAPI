namespace EnterpriseInventory.Application.Features.RolePermissions.DTOs;

/// <summary>
/// Represents the request to assign permissions to a role.
/// </summary>
public class AssignPermissionsRequest
{
    /// <summary>
    /// Gets or sets the Permission Ids to assign.
    /// </summary>
    public List<int> PermissionIds { get; set; } = [];
}