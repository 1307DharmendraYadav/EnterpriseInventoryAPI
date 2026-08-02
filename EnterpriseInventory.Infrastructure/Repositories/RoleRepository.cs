using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Repositories;

/// <summary>
/// Provides data access operations for Role entities.
/// </summary>
public sealed class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await _context.Roles
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task<Role> AddAsync(Role role)
    {
        await _context.Roles.AddAsync(role);

        await _context.SaveChangesAsync();

        return role;
    }

    public async Task UpdateAsync(Role role)
    {
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Role role)
    {
        _context.Roles.Remove(role);

        await _context.SaveChangesAsync();
    }

    public async Task<bool> ExistsByNameAsync(string name)
    {
        name = name.Trim();

        return await _context.Roles
            .AnyAsync(r => r.Name == name);
    }

    public async Task<bool> ExistsByNameExcludingIdAsync(string name, int id)
    {
        name = name.Trim();

        return await _context.Roles
            .AnyAsync(r => r.Name == name && r.Id != id);
    }

    public async Task<IReadOnlyCollection<int>> GetExistingRoleIdsAsync(IEnumerable<int> ids)
    {
        return await _context.Roles
            .Where(r => ids.Contains(r.Id))
            .Select(r => r.Id)
            .ToListAsync();
    }
}