using EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

namespace EnterpriseInventory.Application.Features.EffectivePermissions.Interfaces;

public interface IEffectivePermissionService
{
    Task<IReadOnlyList<EffectivePermissionResponse>>GetEffectivePermissionsAsync(int userId);
}