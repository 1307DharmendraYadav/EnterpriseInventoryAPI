namespace EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

public sealed class PermissionUserOverride
{
    public bool IsAllowed { get; init; }

    public string OverrideType { get; init; } = string.Empty;
}