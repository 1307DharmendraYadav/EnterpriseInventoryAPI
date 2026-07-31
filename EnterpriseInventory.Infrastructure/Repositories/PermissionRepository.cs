using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Repositories;

/// <summary>
/// Provides data access operations for Permission entities.
/// </summary>
public sealed class PermissionRepository : IPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public PermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Permission>> GetAllAsync()
    {
        return await _context.Permissions
            .AsNoTracking()
            .ToListAsync();
    }


    public async Task<Permission?> GetByIdAsync(int id)
    {
        return await _context.Permissions.FindAsync(id);
    }

    public async Task<Permission> AddAsync(Permission permission)
    {
        await _context.Permissions.AddAsync(permission);
        await _context.SaveChangesAsync();
        return permission;
    }

    public async Task UpdateAsync(Permission permission)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Permission permission)
    {
        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
       return await _context.Permissions.AnyAsync(p => p.Name == name);    
    }

    public async Task<bool> ExistsByNameExcludingIdAsync(string name, int id)
    {
       return await _context.Permissions.AnyAsync(p=>p.Name == name && p.Id != id);
    }

    public async Task<IEnumerable<Permission>> GetPermissionsByIdsAsync(IEnumerable<int> permissionIds)
    {
        return await _context.Permissions
            .Where(permission => permissionIds.Contains(permission.Id))
            .AsNoTracking()
            .ToListAsync();
    }
}