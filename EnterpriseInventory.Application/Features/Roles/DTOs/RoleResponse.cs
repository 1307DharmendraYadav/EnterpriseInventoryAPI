namespace EnterpriseInventory.Application.Features.Roles.DTOs;

/// <summary>
/// Represents a business role returned by the API.
/// </summary>
public class RoleResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the role.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}