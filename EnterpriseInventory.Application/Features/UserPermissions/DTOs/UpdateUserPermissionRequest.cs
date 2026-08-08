using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseInventory.Application.Features.UserPermissions.DTOs;

public class UpdateUserPermissionRequest
{
    public bool IsAllowed { get; set; }
}