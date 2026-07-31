using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Repositories;

/// <summary>
/// Provides data access operations for Permission entities.
/// </summary>
public interface IPermissionRepository
{
    Task<IEnumerable<Permission>> GetAllAsync();

    Task<Permission?> GetByIdAsync(int id);

    Task<Permission> AddAsync(Permission permission);

    Task UpdateAsync(Permission permission);

    Task DeleteAsync(Permission permission);

    Task<bool> ExistsByNameAsync(string name);

    Task<bool> ExistsByNameExcludingIdAsync(string name, int id);

    /// <summary>
    /// Returns all permissions matching the supplied Permission Ids.
    /// </summary>
    Task<IEnumerable<Permission>> GetPermissionsByIdsAsync(IEnumerable<int> permissionIds);
}