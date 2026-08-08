using EnterpriseInventory.Application.Features.UserPermissions.DTOs;

namespace EnterpriseInventory.Application.Features.UserPermissions.Interfaces;

public interface IUserPermissionService
{
    Task<IEnumerable<UserPermissionResponse>> GetByUserIdAsync(int userId);

    Task<UserPermissionResponse?> GetAsync(int userId, int permissionId);

    Task<UserPermissionResponse> CreateAsync(int userId,CreateUserPermissionRequest request);

    Task<UserPermissionResponse> UpdateAsync(int userId,int permissionId,UpdateUserPermissionRequest request);

    Task DeleteAsync(int userId,int permissionId);
}