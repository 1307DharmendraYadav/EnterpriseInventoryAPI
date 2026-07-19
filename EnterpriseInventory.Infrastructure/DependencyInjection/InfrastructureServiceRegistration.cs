using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Infrastructure.Authentication;
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
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlServer(
                configuration.GetConnectionString("EnterpriseInventoryDb"));
        });

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IPasswordHasher, AspNetCorePasswordHasher>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

        return services;
    }
}