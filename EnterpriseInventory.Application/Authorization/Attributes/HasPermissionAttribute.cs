using Microsoft.AspNetCore.Authorization;

namespace EnterpriseInventory.Application.Authorization.Attributes;

/// <summary>
/// Specifies that the decorated controller or action
/// requires the specified permission.
/// </summary>
public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(string permission)
    {
        Policy = permission;
    }
}