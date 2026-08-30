using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;
using EnterpriseInventory.Application.Features.EffectivePermissions.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;

namespace EnterpriseInventory.Application.Features.EffectivePermissions.Services;

public sealed class PermissionDiagnosticService : IPermissionDiagnosticService
{
    private readonly IPermissionBreakdownService _permissionBreakdownService;
    private readonly IPermissionRepository _permissionRepository;

    public PermissionDiagnosticService(
        IPermissionBreakdownService permissionBreakdownService,
        IPermissionRepository permissionRepository)
    {
        _permissionBreakdownService = permissionBreakdownService;
        _permissionRepository = permissionRepository;
    }

    public async Task<PermissionDiagnosticResponse>
        GetPermissionDiagnosticAsync(
            int userId,
            int permissionId)
    {
        // ============================================================
        // 1. Validate that the permission exists globally
        //
        // Important:
        // A permission can exist in the system even when the user
        // has no role-based assignment and no user-specific override.
        //
        // Therefore, permission existence must be checked against
        // the Permissions table, not against the user's assignments.
        // ============================================================

        var permission =
            await _permissionRepository
                .GetByIdAsync(permissionId);

        if (permission is null)
        {
            throw new NotFoundException(
                $"Permission with Id '{permissionId}' was not found.");
        }

        // ============================================================
        // 2. Get permission breakdown
        //
        // Breakdown determines:
        // - User information
        // - Role contributions
        // - User-specific override
        // - Effective permission
        // - Effective permission source
        // ============================================================

        var breakdown =
            await _permissionBreakdownService
                .GetPermissionBreakdownAsync(
                    userId,
                    permissionId);

        // ============================================================
        // 3. Determine final decision
        // ============================================================

        var decision = breakdown.IsAllowed
            ? "Allowed"
            : "Denied";

        // ============================================================
        // 4. Determine human-readable reason
        // ============================================================

        var reason = DetermineReason(breakdown);

        // ============================================================
        // 5. Build diagnostic response
        // ============================================================

        return new PermissionDiagnosticResponse
        {
            UserId = breakdown.UserId,
            Username = breakdown.Username,

            PermissionId = permission.Id,
            PermissionName = permission.Name,

            IsAllowed = breakdown.IsAllowed,
            Decision = decision,

            Source = breakdown.Source,
            Reason = reason
        };
    }

    // ================================================================
    // Determines why the permission was allowed or denied.
    // ================================================================

    private static string DetermineReason(
        PermissionBreakdownResponse breakdown)
    {
        // ------------------------------------------------------------
        // Case 1:
        // Explicit user-level override exists.
        //
        // User override always has higher precedence than role
        // permissions.
        // ------------------------------------------------------------

        if (breakdown.UserOverride is not null)
        {
            if (breakdown.UserOverride.IsAllowed)
            {
                return
                    "Permission is allowed by an explicit user-level Allow override.";
            }

            // If a role also grants the permission, the user Deny
            // overrides that role-based Allow.
            if (breakdown.RoleContributions.Count > 0)
            {
                return
                    "Permission is denied because an explicit user-level Deny override takes precedence over the role-based Allow.";
            }

            // User Deny exists but there is no role contribution.
            return
                "Permission is denied because an explicit user-level Deny override is configured for this permission.";
        }

        // ------------------------------------------------------------
        // Case 2:
        // No user override, but one or more roles grant permission.
        // ------------------------------------------------------------

        if (breakdown.RoleContributions.Count > 0)
        {
            return
                "Permission is allowed because it is granted through an assigned role and no user-specific override exists.";
        }

        // ------------------------------------------------------------
        // Case 3:
        // Permission exists globally, but the user has:
        //
        // - No role-based grant
        // - No user-specific override
        //
        // Therefore effective permission = Denied.
        // ------------------------------------------------------------

        return
            "Permission is denied because the user has no role-based permission and no user-specific override.";
    }
}