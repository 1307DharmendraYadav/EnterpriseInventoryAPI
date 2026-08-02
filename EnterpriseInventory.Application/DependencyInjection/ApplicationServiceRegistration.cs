using EnterpriseInventory.Application.Authorization.Handlers;
using EnterpriseInventory.Application.Authorization.PolicyProviders;
using EnterpriseInventory.Application.Features.Authentication.Interfaces;
using EnterpriseInventory.Application.Features.Authentication.Services;
using EnterpriseInventory.Application.Features.Permissions.Interfaces;
using EnterpriseInventory.Application.Features.Permissions.Services;
using EnterpriseInventory.Application.Features.RolePermissions.Interfaces;
using EnterpriseInventory.Application.Features.RolePermissions.Services;
using EnterpriseInventory.Application.Features.Roles.Interfaces;
using EnterpriseInventory.Application.Features.Roles.Services;
using EnterpriseInventory.Application.Features.UserRole.Interfaces;
using EnterpriseInventory.Application.Features.UserRole.Services;
using EnterpriseInventory.Application.Interfaces;
using EnterpriseInventory.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseInventory.Application.DependencyInjection;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services)
    {
        // ============================================================
        // APPLICATION SERVICES
        // ============================================================

        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IAuthService, AuthService>();

        // ============================================================
        // AUTHORIZATION
        // ============================================================

        services.AddSingleton<
            IAuthorizationPolicyProvider,
            PermissionPolicyProvider>();

        services.AddSingleton<
            IAuthorizationHandler,
            PermissionAuthorizationHandler>();

        // ============================================================
        // ROLE
        // ============================================================

        services.AddScoped<IRoleService, RoleService>();

        // ============================================================
        // PERMISSION
        // ============================================================

        services.AddScoped<IPermissionService, PermissionService>();

        // ============================================================
        // ROLE PERMISSION
        // ============================================================

        services.AddScoped<IRolePermissionService, RolePermissionService>();

        // ============================================================
        // USER-ROLE MANAGEMENT (RBAC USER ASSIGNMENT)
        // ============================================================
        // Handles assigning roles to users and retrieving user-role mappings.
        // Example:
        // User -> UserRole -> Role
        //
        // Used for:
        // - Assigning roles during user creation/update
        // - Loading user permissions during authentication
        // - Supporting Role-Based Access Control (RBAC)
        // ============================================================

        services.AddScoped<IUserRoleService, UserRoleService>();

        return services;
    }
}