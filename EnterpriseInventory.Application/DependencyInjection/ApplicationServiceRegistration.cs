using EnterpriseInventory.Application.Authorization.Handlers;
using EnterpriseInventory.Application.Authorization.PolicyProviders;
using EnterpriseInventory.Application.Features.Authentication.Interfaces;
using EnterpriseInventory.Application.Features.Authentication.Services;
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
        // Register application services here.
        services.AddScoped<IProductService, ProductService>();

        services.AddScoped<IAuthService, AuthService>();

        services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();

        services.AddSingleton<IAuthorizationHandler,PermissionAuthorizationHandler>();

        services.AddScoped<IRoleService, RoleService>();

        return services;
    }
}
