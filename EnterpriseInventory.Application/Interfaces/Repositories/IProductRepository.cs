using EnterpriseInventory.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnterpriseInventory.Application.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();

        Task<Product?> GetByIdAsync(int id);

        Task<Product> AddAsync(Product product);

        Task UpdateAsync(Product product);

        Task DeleteAsync(Product product);

        Task<bool> ExistsByNameAsync(string name);
        Task<bool> ExistsByNameExcludingIdAsync(string name, int id);
    }
}
