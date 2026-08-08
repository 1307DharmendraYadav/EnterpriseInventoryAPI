using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Repositories;

public interface IUserPermissionRepository
{
    Task<IEnumerable<UserPermission>> GetByUserIdAsync(int userId);

    Task<UserPermission?> GetAsync(int userId, int permissionId);

    Task AddAsync(UserPermission userPermission);

    Task UpdateAsync(UserPermission userPermission);

    Task DeleteAsync(UserPermission userPermission);
}