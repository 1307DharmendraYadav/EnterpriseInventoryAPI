namespace EnterpriseInventory.Application.Features.UserRole.DTOs;

public sealed class UserRoleResponse
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;
}