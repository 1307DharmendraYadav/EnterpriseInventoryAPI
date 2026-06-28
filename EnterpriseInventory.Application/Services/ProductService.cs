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
        // Business Validation
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Product name is required.");

        if (request.Price <= 0)
            throw new ValidationException("Price must be greater than zero.");

        if (request.Quantity < 0)
            throw new ValidationException("Quantity cannot be negative.");

        // DTO -> Entity
        var product = new Product
        {
            Name = request.Name.Trim(),
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

    public async Task UpdateAsync(int id, CreateProductRequest request)
    {
        // Ensure the incoming request satisfies business rules.
        if (string.IsNullOrWhiteSpace(request.Name))
            throw new ValidationException("Product name is required.");

        if (request.Price <= 0)
            throw new ValidationException("Price must be greater than zero.");

        if (request.Quantity < 0)
            throw new ValidationException("Quantity cannot be negative.");

        // Load existing entity
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException($"Product with Id {id} was not found.");

        // Update only allowed fields
        product.Name = request.Name.Trim();
        product.Price = request.Price;
        product.Quantity = request.Quantity;

        // Save
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
    //public async Task<IEnumerable<ProductDto>> GetAllAsync()
    //{
    //    var products = await _repository.GetAllAsync();

    //    return products.Select(product => new ProductDto
    //    {
    //        Id = product.Id,
    //        Name = product.Name,
    //        Price = product.Price,
    //        Quantity = product.Quantity
    //    });
    //}

    public Task<IEnumerable<ProductDto>> GetAllInMemoryAsync()
    {
        var products = new List<ProductDto>
        {
            new()
            {
                Id = 1,
                Name = "Laptop",
                Price = 65000,
                Quantity = 10
            },
            new()
            {
                Id = 2,
                Name = "Keyboard",
                Price = 1500,
                Quantity = 50
            },
            new()
            {
                Id = 3,
                Name = "Mouse",
                Price = 700,
                Quantity = 80
            }
        };

        return Task.FromResult(products.AsEnumerable());
    }

}