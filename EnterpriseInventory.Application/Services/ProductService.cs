using EnterpriseInventory.Application.DTOs;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _repository;

    public ProductService(IProductRepository repository)
    {
        _repository = repository;
    }
    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var productName = request.Name.Trim();

        // Business Rule
        if (await _repository.ExistsByNameAsync(productName))
            throw new ConflictException($"Product '{productName}' already exists.");

        // DTO -> Entity
        var product = new Product
        {
            Name = productName,
            Price = request.Price,
            Quantity = request.Quantity
        };

        // Save
        var createdProduct = await _repository.AddAsync(product);

        // Entity -> DTO
        return new ProductResponse
        {
            Id = createdProduct.Id,
            Name = createdProduct.Name,
            Price = createdProduct.Price,
            Quantity = createdProduct.Quantity
        };
    }


    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();

        return products.Select(product => new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        });
    }

    public async Task<ProductResponse?> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
        {
            throw new NotFoundException($"Product with Id {id} was not found.");
        }

        return new ProductResponse
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price,
            Quantity = product.Quantity
        };
    }

    public async Task UpdateAsync(int id, UpdateProductRequest request)
    {
        var productName = request.Name.Trim();

        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException($"Product with Id {id} was not found.");

        if (await _repository.ExistsByNameExcludingIdAsync(productName, id))
            throw new ConflictException($"Product '{productName}' already exists.");

        product.Name = productName;
        product.Price = request.Price;
        product.Quantity = request.Quantity;

        await _repository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        // Retrieve the existing product
        var product = await _repository.GetByIdAsync(id);

        // Business decision
        if (product is null)
            throw new NotFoundException($"Product with Id {id} was not found.");

        // Delete the product
        await _repository.DeleteAsync(product);
    }
}