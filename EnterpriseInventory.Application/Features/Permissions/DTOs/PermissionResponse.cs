namespace EnterpriseInventory.Application.Features.Permissions.DTOs;

/// <summary>
/// Represents a permission returned by the API.
/// </summary>
public class PermissionResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the permission.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the unique name of the permission.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the permission.
    /// </summary>
    public string Description { get; set; } = string.Empty;
}