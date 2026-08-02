using EnterpriseInventory.Application.Authorization.Constants;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.RolePermissions.DTOs;
using EnterpriseInventory.Application.Features.RolePermissions.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.RolePermissions.Services;

public sealed class RolePermissionService : IRolePermissionService
{
    private readonly IRoleRepository _roleRepository;

    private readonly IPermissionRepository _permissionRepository;

    private readonly IRolePermissionRepository _rolePermissionRepository;

    public RolePermissionService(
        IRoleRepository roleRepository,
        IPermissionRepository permissionRepository,
        IRolePermissionRepository rolePermissionRepository)
    {
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
        _rolePermissionRepository = rolePermissionRepository;
    }

    public async Task<RolePermissionResponse> GetByRoleIdAsync(int roleId)
    {
        // ============================================================
        // VALIDATE ROLE
        // ============================================================

        var role = await _roleRepository.GetByIdAsync(roleId);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {roleId} was not found.");
        }

        // ============================================================
        // GET ASSIGNED PERMISSION IDS
        // ============================================================

        var permissionIds = await _rolePermissionRepository
            .GetPermissionIdsByRoleIdAsync(roleId);

        // ============================================================
        // LOAD PERMISSION DETAILS
        // ============================================================

        var permissions = await _permissionRepository
            .GetPermissionsByIdsAsync(permissionIds);

        // ============================================================
        // BUILD RESPONSE
        // ============================================================

        return new RolePermissionResponse
        {
            RoleId = role.Id,
            RoleName = role.Name,

            Permissions = permissions.Select(permission => new AssignedPermissionResponse
            {
                Id = permission.Id,
                Name = permission.Name
            })
        };
    }

    public async Task AssignPermissionsAsync(
    int roleId,
    AssignPermissionsRequest request)
    {
        // ============================================================
        // VALIDATE ROLE
        // ============================================================

        var role = await _roleRepository.GetByIdAsync(roleId);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {roleId} was not found.");
        }

        // ============================================================
        // VALIDATE PERMISSIONS
        // ============================================================

        var permissions =
            await _permissionRepository
                .GetPermissionsByIdsAsync(request.PermissionIds);

        if (permissions.Count() != request.PermissionIds.Count)
        {
            throw new ValidationException(
                "One or more selected permissions do not exist.");
        }

        // ============================================================
        // PROTECT BOOTSTRAP ADMINISTRATOR ROLE
        // ============================================================

        if (role.Name == RoleConstants.Administrator)
        {
            var selectedPermissionNames = permissions
                .Select(permission => permission.Name)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            foreach (var permission in CriticalPermissionConstants.AdministratorPermissions)
            {
                if (!selectedPermissionNames.Contains(permission))
                {
                    throw new ValidationException(
    $"Administrator role must always contain the following permissions: {string.Join(", ", CriticalPermissionConstants.AdministratorPermissions)}.");
                }
            }
        }

        // ============================================================
        // REMOVE EXISTING ROLE-PERMISSION MAPPINGS
        // ============================================================

        await _rolePermissionRepository
            .RemoveByRoleIdAsync(roleId);

        // ============================================================
        // CREATE NEW ROLE-PERMISSION MAPPINGS
        // ============================================================

        var rolePermissions =
            request.PermissionIds
                .Select(permissionId => new RolePermission
                {
                    RoleId = roleId,
                    PermissionId = permissionId
                });

        // ============================================================
        // SAVE NEW MAPPINGS
        // ============================================================

        await _rolePermissionRepository
            .AddRangeAsync(rolePermissions);
    }
}