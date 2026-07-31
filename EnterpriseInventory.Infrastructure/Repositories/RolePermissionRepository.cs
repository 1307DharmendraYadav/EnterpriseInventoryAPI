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
}