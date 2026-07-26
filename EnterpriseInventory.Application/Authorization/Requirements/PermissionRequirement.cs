using Microsoft.AspNetCore.Authorization;

namespace EnterpriseInventory.Application.Authorization.Requirements;

/// <summary>
/// Represents a permission that must be satisfied before
/// a protected resource (controller/action) can be accessed.
///
/// This requirement is created by the
/// <see cref="PolicyProviders.PermissionPolicyProvider"/>
/// and evaluated by the
/// <see cref="Handlers.PermissionAuthorizationHandler"/>
/// during the ASP.NET Core authorization pipeline.
///
/// Example:
///
/// [HasPermission(PermissionConstants.Product.Create)]
///
/// ↓
///
/// PermissionRequirement("Product.Create")
/// </summary>
public sealed class PermissionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// Initializes a new permission requirement.
    /// </summary>
    /// <param name="permission">
    /// The business permission required to access
    /// the protected resource.
    ///
    /// Example:
    /// Product.View
    /// Product.Create
    /// User.Delete
    /// </param>
    public PermissionRequirement(string permission)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(permission);

        Permission = permission;
    }

    /// <summary>
    /// Gets the business permission that must be satisfied
    /// for authorization to succeed.
    /// </summary>
    public string Permission { get; }
}