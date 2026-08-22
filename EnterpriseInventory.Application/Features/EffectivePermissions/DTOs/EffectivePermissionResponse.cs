namespace EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

public sealed class EffectivePermissionResponse
{
    public int PermissionId { get; init; }

    public string PermissionName { get; init; } = string.Empty;

    public bool IsAllowed { get; init; }

    public string Source { get; init; } = string.Empty;

    public string? RoleName { get; init; }

    public string? OverrideType { get; init; }
}