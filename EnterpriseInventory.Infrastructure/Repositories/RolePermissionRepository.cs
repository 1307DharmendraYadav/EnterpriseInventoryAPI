using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Repositories;

/// <summary>
/// Provides data access operations for Role-Permission mappings.
/// </summary>
public sealed class RolePermissionRepository : IRolePermissionRepository
{
    private readonly ApplicationDbContext _context;

    public RolePermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Returns only the Permission Ids assigned to a role.
    /// </summary>
    public async Task<List<int>> GetPermissionIdsByRoleIdAsync(int roleId)
    {
        return await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.PermissionId)
            .ToListAsync();
    }

    /// <summary>
    /// Adds multiple role-permission mappings.
    /// </summary>
    public async Task AddRangeAsync(IEnumerable<RolePermission> rolePermissions)
    {
        await _context.RolePermissions
             .AddRangeAsync(rolePermissions);

        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Removes all permissions assigned to the specified role.
    /// </summary>
    public async Task RemoveByRoleIdAsync(int roleId)
    {
        var rolePermissions = _context.RolePermissions
            .Where(rp => rp.RoleId == roleId);
        _context.RolePermissions.RemoveRange(rolePermissions);
        await _context.SaveChangesAsync();
    }


    /// <summary>
    /// Returns Role-Permission mappings for all supplied roles,
    /// including the associated Role and Permission entities.
    ///
    /// Used by Sprint 12F to determine:
    /// - Which role granted a permission
    /// - Which permission was granted
    /// - The source of an effective permission
    /// </summary>
    public async Task<List<RolePermission>> GetByRoleIdsAsync(IEnumerable<int> roleIds)
    {
        var ids = roleIds
            .Distinct()
            .ToList();

        if (ids.Count == 0)
        {
            return [];
        }

        return await _context.RolePermissions
            .Where(rp => ids.Contains(rp.RoleId))
            .Include(rp => rp.Role)
            .Include(rp => rp.Permission)
            .AsNoTracking()
            .ToListAsync();
    }
}