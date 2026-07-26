using EnterpriseInventory.Application.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace EnterpriseInventory.Application.Authorization.PolicyProviders;

/// <summary>
/// Dynamically creates authorization policies for permission names.
///
/// Example:
/// [HasPermission("Product.Create")]
///
/// ASP.NET Core requests a policy named "Product.Create".
/// This provider automatically creates the policy instead of
/// requiring every permission to be registered manually.
/// </summary>
public sealed class PermissionPolicyProvider
    : DefaultAuthorizationPolicyProvider
{
    public PermissionPolicyProvider(
        IOptions<AuthorizationOptions> options)
        : base(options)
    {
    }

    public override async Task<AuthorizationPolicy?> GetPolicyAsync(
        string policyName)
    {
        // Check if a policy has already been registered.
        var policy = await base.GetPolicyAsync(policyName);

        if (policy != null)
        {
            return policy;
        }

        // Dynamically create a permission policy.
        return new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionRequirement(policyName))
            .Build();
    }
}