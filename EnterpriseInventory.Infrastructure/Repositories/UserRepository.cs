using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsByUsernameAsync(string username)
    {
        return await _context.Users.AnyAsync(u => u.Username == username);
    }
    public Task<bool> ExistsByEmailAsync(string email)
    {
        return _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetByLoginAsync(string login)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u =>
                u.Username == login ||
                u.Email == login);
    }

    public async Task<User> AddAsync(User user)
    {
        await _context.Users.AddAsync(user);

        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<IReadOnlyCollection<string>> GetRolesAsync(int userId)
    {
        return await _context.UserRoles
            .AsNoTracking()
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.Role.Name)
            .ToListAsync();
    }

    /// <summary>
    /// Retrieves all effective permissions assigned to the specified user.
    ///
    /// Permissions are resolved through:
    /// User → UserRoles → RolePermissions → Permissions
    /// </summary>
    /// <param name="userId">
    /// The unique identifier of the user whose permissions are to be retrieved.
    /// </param>
    /// <returns>
    /// A read-only collection containing all effective permission names
    /// assigned to the user.
    /// </returns>
    public async Task<IReadOnlyCollection<string>> GetPermissionsAsync(int userId)
    {
        return await _context.UserRoles
            .AsNoTracking()

            // Get roles assigned to the user.
            .Where(ur => ur.UserId == userId)

            // Flatten all permissions from the user's assigned roles.
            .SelectMany(ur => ur.Role.RolePermissions)

            // Return only permission names.
            .Select(rp => rp.Permission.Name)

            // Remove duplicate permissions that may come from multiple roles.
            .Distinct()

            .ToListAsync();
    }
}
