using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;
using EnterpriseInventory.Application.Features.EffectivePermissions.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;

namespace EnterpriseInventory.Application.Features.EffectivePermissions.Services;

public sealed class EffectivePermissionService : IEffectivePermissionService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    private readonly IRolePermissionRepository _rolePermissionRepository;
    private readonly IUserPermissionRepository _userPermissionRepository;

    public EffectivePermissionService(
        IUserRepository userRepository,
        IUserRoleRepository userRoleRepository,
        IRolePermissionRepository rolePermissionRepository,
        IUserPermissionRepository userPermissionRepository)
    {
        _userRepository = userRepository;
        _userRoleRepository = userRoleRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _userPermissionRepository = userPermissionRepository;
    }

    public async Task<IReadOnlyList<EffectivePermissionResponse>>
        GetEffectivePermissionsAsync(int userId)
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
        // 4. Get user-specific permission overrides
        // ============================================================

        var userOverrides = await _userPermissionRepository
            .GetByUserIdAsync(userId);

        // ============================================================
        // 5. Build effective permissions
        // ============================================================

        var effectivePermissions =
            new Dictionary<int, EffectivePermissionResponse>();

        // ============================================================
        // 6. Apply role-based permissions
        // ============================================================

        foreach (var rolePermission in rolePermissions)
        {
            var permission = rolePermission.Permission;

            if (effectivePermissions.ContainsKey(permission.Id))
            {
                continue;
            }

            effectivePermissions[permission.Id] =
                new EffectivePermissionResponse
                {
                    PermissionId = permission.Id,
                    PermissionName = permission.Name,
                    IsAllowed = true,
                    Source = "Role",
                    RoleName = rolePermission.Role.Name,
                    OverrideType = null
                };
        }

        // ============================================================
        // 7. Apply user-specific overrides
        //
        // User overrides have higher precedence than role permissions.
        //
        // User DENY  → DENY
        // User ALLOW → ALLOW
        // ============================================================

        foreach (var userOverride in userOverrides)
        {
            var permission = userOverride.Permission;

            effectivePermissions[permission.Id] =
                new EffectivePermissionResponse
                {
                    PermissionId = permission.Id,
                    PermissionName = permission.Name,
                    IsAllowed = userOverride.IsAllowed,
                    Source = "UserOverride",
                    RoleName = GetRoleName(
                        rolePermissions,
                        permission.Id),
                    OverrideType = userOverride.IsAllowed
                        ? "Allow"
                        : "Deny"
                };
        }

        // ============================================================
        // 8. Return deterministic result
        // ============================================================

        return effectivePermissions.Values
            .OrderBy(permission => permission.PermissionName)
            .ToList();
    }

    private static string? GetRoleName(
        IEnumerable<Domain.Entities.RolePermission> rolePermissions,
        int permissionId)
    {
        return rolePermissions
            .Where(rp => rp.PermissionId == permissionId)
            .Select(rp => rp.Role.Name)
            .FirstOrDefault();
    }
}