using EnterpriseInventory.API.Helpers;
using EnterpriseInventory.Application.Common;
using EnterpriseInventory.Application.DTOs;
using EnterpriseInventory.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllAsync();

            return Ok(
                ApiResponseFactory.Success(
                    products,
                    "Products retrieved successfully.",
                    StatusCodes.Status200OK,
                    HttpContext.TraceIdentifier));
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductRequest request)
        {
            var product = await _productService.CreateAsync(request);

            var response = ApiResponseFactory.Success(
                product,
                "Product created successfully.",
                StatusCodes.Status201Created,
                HttpContext.TraceIdentifier);

            return CreatedAtAction(
                nameof(GetById),
                new { id = product.Id },
                response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return Ok(
                ApiResponseFactory.Success(
                    product,
                    "Product retrieved successfully.",
                    StatusCodes.Status200OK,
                    HttpContext.TraceIdentifier));
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,CreateProductRequest request)
        {
            await _productService.UpdateAsync(id, request);

            return Ok(
                ApiResponseFactory.Success(
                    data: (object?)null,
                    message: "Product updated successfully.",
                    statusCode: StatusCodes.Status200OK,
                    traceId: HttpContext.TraceIdentifier));
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteAsync(id);
            return Ok(ApiResponseFactory.Success
                (
                    data: (object?)null,
                    message: "Product deleted successfully.",
                    statusCode: StatusCodes.Status200OK,
                    traceId: HttpContext.TraceIdentifier
                ));
        }

        [HttpGet("GetInMemory")]
        public async Task<IActionResult> GetInMemory()
        {
            var products = await _productService.GetAllInMemoryAsync();

            return Ok(products);
        }
    }
}
