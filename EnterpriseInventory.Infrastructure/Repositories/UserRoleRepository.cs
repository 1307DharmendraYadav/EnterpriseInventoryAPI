using Microsoft.EntityFrameworkCore;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Infrastructure.Persistence.Context;

namespace EnterpriseInventory.Infrastructure.Repositories;

public sealed class UserRoleRepository : IUserRoleRepository
{
    private readonly ApplicationDbContext _context;

    public UserRoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId)
    {
        return await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId)
    {
        return await _context.UserRoles
            .Where(ur => ur.RoleId == roleId)
            .Select(ur => ur.User)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task ReplaceUserRolesAsync(int userId, IEnumerable<int> roleIds)
    {
        var existingRoles = await _context.UserRoles
         .Where(ur => ur.UserId == userId)
         .ToListAsync();

        _context.UserRoles.RemoveRange(existingRoles);

        var newUserRoles = roleIds.Select(roleId => new UserRole
        {
            UserId = userId,
            RoleId = roleId
        });

        await _context.UserRoles.AddRangeAsync(newUserRoles);

        await _context.SaveChangesAsync();
    }
}
