using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsByUsernameAsync(string username);

    Task<bool> ExistsByEmailAsync(string email);
    Task<User?> GetByIdAsync(int id);


    Task<User?> GetByLoginAsync(string login);

    Task<User> AddAsync(User user);

    /// <summary>
    /// Retrieves all business roles assigned to the specified user.
    /// </summary>
    /// <param name="userId">
    /// The unique identifier of the user.
    /// </param>
    /// <returns>
    /// A read-only collection containing the names of all assigned roles.
    /// </returns>
    Task<IReadOnlyCollection<string>> GetRolesAsync(int userId);

    /// <summary>
    /// Retrieves all effective permissions assigned to the specified user.
    ///
    /// Permissions are resolved through the user's assigned roles
    /// (User → UserRoles → RolePermissions → Permissions).
    /// </summary>
    /// <param name="userId">
    /// The unique identifier of the user.
    /// </param>
    /// <returns>
    /// A read-only collection containing the names of all permissions
    /// granted to the user.
    /// </returns>
    Task<IReadOnlyCollection<string>> GetPermissionsAsync(int userId);
}