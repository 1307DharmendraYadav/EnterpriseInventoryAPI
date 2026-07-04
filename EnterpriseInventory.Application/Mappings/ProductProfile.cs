using AutoMapper;
using EnterpriseInventory.Application.DTOs;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            // Request DTOs → Domain Entity

            // Maps client input into the Product domain entity.
            // Business rules are executed before mapping.
            CreateMap<CreateProductRequest, Product>();

            CreateMap<UpdateProductRequest, Product>();

            // Domain Entity → Response DTO

            // Maps the persisted Product entity into the DTO
            // returned to the API client.
            CreateMap<Product, ProductResponse>();

            
        }
    }
}