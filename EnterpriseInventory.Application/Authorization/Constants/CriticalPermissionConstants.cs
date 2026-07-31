namespace EnterpriseInventory.Application.Authorization.Constants;

/// <summary>
/// Contains permissions that are considered critical for system administration.
/// </summary>
public static class CriticalPermissionConstants
{
    /// <summary>
    /// Minimum permissions required by the bootstrap Administrator role.
    /// </summary>
    public static readonly string[] AdministratorPermissions =
    [
        PermissionConstants.Role.View,
        PermissionConstants.Role.Update
    ];
}