namespace EnterpriseInventory.Application.Features.Permissions.DTOs;

/// <summary>
/// Represents the request to update an existing permission.
/// </summary>
public class UpdatePermissionRequest
{
    /// <summary>
    /// Gets or sets the updated name of the permission.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated description of the permission.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}