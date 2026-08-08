namespace EnterpriseInventory.Application.Features.UserPermissions.DTOs;

public class CreateUserPermissionRequest
{
    public int PermissionId { get; set; }

    public bool IsAllowed { get; set; }
}
