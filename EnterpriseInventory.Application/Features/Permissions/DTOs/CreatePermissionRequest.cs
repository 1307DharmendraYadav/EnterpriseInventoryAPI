namespace EnterpriseInventory.Application.Features.Permissions.DTOs;

/// <summary>
/// Represents the request to create a new permission.
/// </summary>
public class CreatePermissionRequest
{
    /// <summary>
    /// Gets or sets the unique name of the permission.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the permission.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}