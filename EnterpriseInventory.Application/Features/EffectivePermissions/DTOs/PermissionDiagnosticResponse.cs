namespace EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

public sealed class PermissionDiagnosticResponse
{
    public int UserId { get; init; }

    public string Username { get; init; } = string.Empty;

    public int PermissionId { get; init; }

    public string PermissionName { get; init; } = string.Empty;

    public bool IsAllowed { get; init; }

    public string Decision { get; init; } = string.Empty;

    public string Source { get; init; } = string.Empty;

    public string Reason { get; init; } = string.Empty;
}
