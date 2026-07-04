using EnterpriseInventory.Application.DTOs;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;
using AutoMapper;
namespace EnterpriseInventory.Application.Services;


public class ProductService : IProductService
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public ProductService(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<ProductResponse> CreateAsync(CreateProductRequest request)
    {
        var productName = request.Name.Trim();

        if (await _repository.ExistsByNameAsync(productName))
            throw new ConflictException($"Product '{productName}' already exists.");

        var product = _mapper.Map<Product>(request);

        var createdProduct = await _repository.AddAsync(product);

        return _mapper.Map<ProductResponse>(createdProduct);
    }


    public async Task<IEnumerable<ProductResponse>> GetAllAsync()
    {
        var products = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<ProductResponse> GetByIdAsync(int id)
    {
        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException($"Product with Id {id} was not found.");

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> UpdateAsync(int id,UpdateProductRequest request)
    {
        var productName = request.Name.Trim();

        var product = await _repository.GetByIdAsync(id);

        if (product is null)
            throw new NotFoundException(
                $"Product with Id {id} was not found.");

        if (await _repository.ExistsByNameExcludingIdAsync(productName, id))
            throw new ConflictException(
                $"Product '{productName}' already exists.");

        request.Name = productName;

        _mapper.Map(request, product);

        await _repository.UpdateAsync(product);

        return _mapper.Map<ProductResponse>(product);
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