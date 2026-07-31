using EnterpriseInventory.Application.Features.Permissions.DTOs;

namespace EnterpriseInventory.Application.Features.Permissions.Interfaces;

/// <summary>
/// Provides business operations for managing permissions.
/// </summary>
public interface IPermissionService
{
    Task<IEnumerable<PermissionResponse>> GetAllAsync();

    Task<PermissionResponse?> GetByIdAsync(int id);

    Task<PermissionResponse> CreateAsync(
        CreatePermissionRequest request);

    Task<PermissionResponse> UpdateAsync(
        int id,
        UpdatePermissionRequest request);

    Task DeleteAsync(int id);
}