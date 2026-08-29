namespace EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

public sealed class PermissionRoleContribution
{
    public int RoleId { get; init; }

    public string RoleName { get; init; } = string.Empty;

    public bool IsAllowed { get; init; }
}