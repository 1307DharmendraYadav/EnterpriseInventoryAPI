using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseInventory.Application.Features.UserPermissions.DTOs;

public class UserPermissionResponse
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PermissionId { get; set; }

    public string PermissionName { get; set; } = string.Empty;

    public bool IsAllowed { get; set; }
}
