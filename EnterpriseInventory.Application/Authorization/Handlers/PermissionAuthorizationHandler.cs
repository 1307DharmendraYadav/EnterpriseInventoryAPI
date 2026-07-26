using EnterpriseInventory.Application.Authorization.Constants;
using EnterpriseInventory.Application.Authorization.Requirements;
using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EnterpriseInventory.Application.Authorization.Handlers;

/// <summary>
/// Evaluates whether the current authenticated user
/// possesses the permission required to access a protected resource.
///
/// This handler is automatically executed by the ASP.NET Core
/// Authorization Pipeline whenever a <see cref="PermissionRequirement"/>
/// needs to be evaluated.
///
/// Authorization Flow
/// ------------------------------------------------------------
/// HTTP Request
///        ↓
/// [HasPermission("Product.Create")]
///        ↓
/// PermissionPolicyProvider
///        ↓
/// PermissionRequirement("Product.Create")
///        ↓
/// PermissionAuthorizationHandler
///        ↓
/// ClaimsPrincipal (JWT Claims)
///        ↓
/// Access Granted / Access Denied
/// ------------------------------------------------------------
/// </summary>
public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    /// <summary>
    /// Evaluates whether the authenticated user satisfies
    /// the requested permission requirement.
    /// </summary>
    /// <param name="context">
    /// Contains the current authenticated user (ClaimsPrincipal),
    /// authorization state and protected resource.
    /// </param>
    /// <param name="requirement">
    /// The permission that must be present.
    ///
    /// Examples:
    /// Product.View
    /// Product.Create
    /// Product.Update
    /// User.Delete
    /// </param>
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        // ============================================================
        // STEP 1
        //
        // Verify that the request belongs to an authenticated user.
        //
        // If authentication has failed, there is no authenticated
        // identity and therefore no claims to evaluate.
        //
        // Authorization immediately stops.
        // ============================================================
        if (context.User.Identity?.IsAuthenticated != true)
        {
            return Task.CompletedTask;
        }

        // ============================================================
        // STEP 2
        //
        // Search the authenticated user's JWT claims for the
        // permission required by the current request.
        //
        //During Login(), JwtTokenGenerator creates the JWT and
        //adds one custom permission claim for every effective
        //permission assigned to the authenticated user.

        //
        // Example JWT payload:
        //
        // {
        //   "permission": "Product.View",
        //   "permission": "Product.Create",
        //   "permission": "Product.Update",
        //   "permission": "User.View"
        // }
        //
        // Here we simply verify whether the required permission
        // exists in the authenticated user's claims.
        //
        // NOTE
        // ----
        // We intentionally validate Permission claims instead of
        // Role claims.
        //
        // Roles answer:
        //
        //     "Who is the user?"
        //
        // Permissions answer:
        //
        //     "What is the user allowed to do?"
        //
        // Enterprise applications typically authorize requests
        // based on permissions rather than roles because permissions
        // provide much finer control over access.
        // ============================================================
        var hasPermission = context.User.Claims.Any(claim =>
            claim.Type == ClaimConstants.Permission &&
            claim.Value == requirement.Permission);

        // ============================================================
        // STEP 3
        //
        // If the required permission is found,
        // mark this authorization requirement as successful.
        //
        // ASP.NET Core will continue processing the request and
        // allow the controller/action to execute.
        // ============================================================
        if (hasPermission)
        {
            context.Succeed(requirement);
        }

        // ============================================================
        // If context.Succeed() is never called,
        // ASP.NET Core automatically treats this requirement as failed
        // and returns:
        //
        // HTTP 403 - Forbidden
        //
        // No explicit "Fail()" call is required.
        // ============================================================
        return Task.CompletedTask;
    }
}