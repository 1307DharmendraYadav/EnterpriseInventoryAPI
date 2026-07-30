namespace EnterpriseInventory.Application.Features.Roles.DTOs;

/// <summary>
/// Represents the request to create a new business role.
/// </summary>
public class CreateRoleRequest
{
    /// <summary>
    /// Gets or sets the unique name of the role.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}