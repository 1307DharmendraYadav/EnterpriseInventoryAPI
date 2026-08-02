using EnterpriseInventory.Application.Common.Settings;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Infrastructure.Persistence.Context;
using EnterpriseInventory.Infrastructure.Repositories;
using EnterpriseInventory.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EnterpriseInventory.Infrastructure.DependencyInjection;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // ============================================================
        // DATABASE
        // ============================================================

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("EnterpriseInventoryDb"));
        });

        // ============================================================
        // CONFIGURATION
        // ============================================================


        services.Configure<DefaultAdminSettings>(
            configuration.GetSection(DefaultAdminSettings.SectionName));

        // ============================================================
        // REPOSITORIES
        // ============================================================

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRolePermissionRepository, RolePermissionRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        // ============================================================
        // SECURITY
        // ============================================================

        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}