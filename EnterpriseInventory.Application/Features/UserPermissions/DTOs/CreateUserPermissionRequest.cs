namespace EnterpriseInventory.Application.Features.UserPermissions.DTOs;

public class CreateUserPermissionRequest
{
   // public int UserId { get; set; }

    public int PermissionId { get; set; }

    public bool IsAllowed { get; set; }
}
