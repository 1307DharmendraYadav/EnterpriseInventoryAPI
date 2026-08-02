using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Repositories;

public interface IUserRoleRepository
{
    Task<IEnumerable<Role>> GetRolesByUserIdAsync(int userId);

    Task ReplaceUserRolesAsync(
        int userId,
        IEnumerable<int> roleIds);

    Task<IEnumerable<User>> GetUsersByRoleIdAsync(int roleId);
}