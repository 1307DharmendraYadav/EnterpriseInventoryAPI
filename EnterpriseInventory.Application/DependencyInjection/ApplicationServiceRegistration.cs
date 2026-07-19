using EnterpriseInventory.Application.Features.Authentication.Interfaces;
using EnterpriseInventory.Application.Features.Authentication.Services;
using EnterpriseInventory.Application.Interfaces;
using EnterpriseInventory.Application.Services;
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

        return services;
    }
}
