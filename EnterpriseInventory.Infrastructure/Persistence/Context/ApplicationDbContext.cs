using EnterpriseInventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseInventory.Infrastructure.Persistence.Context;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();
}