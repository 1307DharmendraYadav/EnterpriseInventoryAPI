using EnterpriseInventory.Application.Features.UserRole.DTOs;

namespace EnterpriseInventory.Application.Features.UserRole.Interfaces;

public interface IUserRoleService
{
    Task<IEnumerable<UserRoleResponse>> GetUserRolesAsync(int userId);

    Task ReplaceUserRolesAsync(
        int userId,
        ReplaceUserRolesRequest request);

    Task<IEnumerable<UserResponse>> GetUsersByRoleAsync(int roleId);

}