using EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;

namespace EnterpriseInventory.Application.Features.EffectivePermissions.Interfaces;

public interface IPermissionDiagnosticService
{
    Task<PermissionDiagnosticResponse> GetPermissionDiagnosticAsync(
        int userId,
        int permissionId);
}
