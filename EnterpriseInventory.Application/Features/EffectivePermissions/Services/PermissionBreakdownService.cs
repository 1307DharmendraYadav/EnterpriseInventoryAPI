using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;
using EnterpriseInventory.Application.Features.EffectivePermissions.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;

namespace EnterpriseInventory.Application.Features.EffectivePermissions.Services;

public sealed class PermissionBreakdownService : IPermissionBreakdownService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IUserPermissionRepository _userPermissionRepository;
    private readonly IPermissionRepository _permissionRepository;

    public PermissionBreakdownService(
    IUserRepository userRepository,
    IUserRoleRepository userRoleRepository,
    IRolePermissionRepository rolePermissionRepository,
    IUserPermissionRepository userPermissionRepository,
    IPermissionRepository permissionRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _userPermissionRepository = userPermissionRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task<PermissionBreakdownResponse>
        GetPermissionBreakdownAsync(
            int userId,
            int permissionId)
    {
        // ============================================================
        // 1. Validate user
        // ============================================================

        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new NotFoundException(
                $"User with Id '{userId}' was not found.");
        }

        // ============================================================
        // 2. Get roles assigned to the user
        // ============================================================

        var roles = await _userRoleRepository
            .GetRolesByUserIdAsync(userId);

        var roleList = roles.ToList();

        // ============================================================
        // 3. Get role-permission mappings
        // ============================================================

        var roleIds = roleList
            .Select(role => role.Id)
            .Distinct()
            .ToList();

        var rolePermissions = roleIds.Count == 0
            ? []
            : await _rolePermissionRepository
                .GetByRoleIdsAsync(roleIds);

        // ============================================================
        // 4. Get role contributions for requested permission
        // ============================================================

        var roleContributions = rolePermissions
            .Where(rolePermission =>
                rolePermission.PermissionId == permissionId)
            .Select(rolePermission =>
                new PermissionRoleContribution
                {
                    RoleId = rolePermission.RoleId,
                    RoleName = rolePermission.Role.Name,
                    IsAllowed = true
                })
            .OrderBy(contribution => contribution.RoleName)
            .ToList();

        // ============================================================
        // 5. Get user-specific override
        // ============================================================

        var userOverrides = await _userPermissionRepository
            .GetByUserIdAsync(userId);

        var userOverride = userOverrides
            .FirstOrDefault(
                permission => permission.PermissionId == permissionId);

        // ============================================================
        // 6. Validate permission exists
        //
        // Permission existence is checked against the Permissions table,
        // not against the user's role assignments or overrides.
        //
        // A permission can exist globally even when the user has neither
        // a role-based grant nor a user-specific override.
        // ============================================================

        var permission =
            await _permissionRepository.GetByIdAsync(permissionId);

        if (permission is null)
        {
            throw new NotFoundException(
                $"Permission with Id '{permissionId}' was not found.");
        }

        // ============================================================
        // 7. Resolve effective permission
        //
        // Precedence:
        //
        // User Override
        //       ↓
        // Role Permission
        //
        // Explicit user override always wins.
        // ============================================================

        bool isAllowed;
        string source;

        if (userOverride is not null)
        {
            isAllowed = userOverride.IsAllowed;
            source = "UserOverride";
        }
        else if (roleContributions.Count > 0)
        {
            isAllowed = true;
            source = "Role";
        }
        else
        {
            isAllowed = false;
            source = "None";
        }

        // ============================================================
        // 8. Build user override response
        // ============================================================

        PermissionUserOverride? overrideResponse = null;

        if (userOverride is not null)
        {
            overrideResponse = new PermissionUserOverride
            {
                IsAllowed = userOverride.IsAllowed,
                OverrideType = userOverride.IsAllowed
                    ? "Allow"
                    : "Deny"
            };
        }

        // ============================================================
        // 9. Build breakdown response
        // ============================================================

        return new PermissionBreakdownResponse
        {
            UserId = user.Id,
            Username = user.Username,
            PermissionId = permission.Id,
            PermissionName = permission.Name,

            RoleContributions = roleContributions,

            UserOverride = overrideResponse,

            IsAllowed = isAllowed,

            Source = source
        };
    }
}