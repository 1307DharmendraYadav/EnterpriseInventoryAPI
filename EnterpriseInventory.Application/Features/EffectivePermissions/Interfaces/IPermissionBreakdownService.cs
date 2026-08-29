using EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

namespace EnterpriseInventory.Application.Features.EffectivePermissions.Interfaces;

public interface IPermissionBreakdownService
{
    Task<PermissionBreakdownResponse> GetPermissionBreakdownAsync(
        int userId,
        int permissionId);
}