using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Repositories;

public sealed class UserPermissionRepository : IUserPermissionRepository
{
    private readonly ApplicationDbContext _context;

    public UserPermissionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UserPermission>> GetByUserIdAsync(int userId)
    {
        return await _context.UserPermissions
            .Where(up => up.UserId == userId)
            .Include(up => up.Permission)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<UserPermission?> GetAsync(int userId, int permissionId)
    {
        return await _context.UserPermissions
            .Include(up => up.Permission)
            .AsNoTracking()
            .FirstOrDefaultAsync(up =>
                up.UserId == userId &&
                up.PermissionId == permissionId);
    }

    public async Task AddAsync(UserPermission userPermission)
    {
        await _context.UserPermissions.AddAsync(userPermission);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(UserPermission userPermission)
    {
        _context.UserPermissions.Remove(userPermission);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(UserPermission userPermission)
    {
        _context.UserPermissions.Update(userPermission);
        await _context.SaveChangesAsync();
    }
}
