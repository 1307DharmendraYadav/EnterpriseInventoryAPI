namespace EnterpriseInventory.Application.Features.Roles.DTOs;

/// <summary>
/// Represents the request to update an existing business role.
/// </summary>
public class UpdateRoleRequest
{
    /// <summary>
    /// Gets or sets the updated name of the role.
    /// </summary>
    public string Name { get; set; } = string.Empty;
}