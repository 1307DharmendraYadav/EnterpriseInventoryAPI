namespace EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

public sealed class PermissionBreakdownResponse
{
    public int UserId { get; init; }

    public string Username { get; init; } = string.Empty;

    public int PermissionId { get; init; }

    public string PermissionName { get; init; } = string.Empty;

    public IReadOnlyList<PermissionRoleContribution> RoleContributions { get; init; }
        = [];

    public PermissionUserOverride? UserOverride { get; init; }

    public bool IsAllowed { get; init; }

    public string Source { get; init; } = string.Empty;
}