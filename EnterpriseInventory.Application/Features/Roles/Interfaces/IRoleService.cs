using EnterpriseInventory.Application.Features.Roles.DTOs;

namespace EnterpriseInventory.Application.Features.Roles.Interfaces;

/// <summary>
/// Provides business operations for managing roles.
/// </summary>
public interface IRoleService
{
    Task<IEnumerable<RoleResponse>> GetAllAsync();

    Task<RoleResponse?> GetByIdAsync(int id);

    Task<RoleResponse> CreateAsync(CreateRoleRequest request);

    Task<RoleResponse> UpdateAsync(int id, UpdateRoleRequest request);

    Task DeleteAsync(int id);
}