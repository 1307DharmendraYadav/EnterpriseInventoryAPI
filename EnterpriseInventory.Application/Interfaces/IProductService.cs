using EnterpriseInventory.Application.DTOs;

namespace EnterpriseInventory.Application.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductResponse>> GetAllAsync();

    Task<ProductResponse?> GetByIdAsync(int id);

    Task<ProductResponse> CreateAsync(CreateProductRequest request);

    Task UpdateAsync(int id, CreateProductRequest request);

    Task DeleteAsync(int id);
    Task<IEnumerable<ProductDto>> GetAllInMemoryAsync();
}