using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Repositories;

/// <summary>
/// Provides data access operations for Role entities.
/// </summary>
public interface IRoleRepository
{
    Task<IEnumerable<Role>> GetAllAsync();

    Task<Role?> GetByIdAsync(int id);

    Task<Role> AddAsync(Role role);

    Task UpdateAsync(Role role);

    Task DeleteAsync(Role role);

    Task<bool> ExistsByNameAsync(string name);

    Task<bool> ExistsByNameExcludingIdAsync(string name, int id);
}