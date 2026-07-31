using EnterpriseInventory.Application.Authorization.Handlers;
using EnterpriseInventory.Application.Authorization.PolicyProviders;
using EnterpriseInventory.Application.Features.Authentication.Interfaces;
using EnterpriseInventory.Application.Features.Authentication.Services;
using EnterpriseInventory.Application.Features.Permissions.Interfaces;
using EnterpriseInventory.Application.Features.Permissions.Services;
using EnterpriseInventory.Application.Features.RolePermissions.Interfaces;
using EnterpriseInventory.Application.Features.Roles.Interfaces;
using EnterpriseInventory.Application.Features.Roles.Services;
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

        return services;
    }
}