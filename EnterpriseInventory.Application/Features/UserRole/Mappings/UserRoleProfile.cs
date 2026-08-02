using AutoMapper;
using EnterpriseInventory.Application.Features.UserRole.DTOs;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.UserRole.Mappings;

public sealed class UserRoleProfile : Profile
{
    public UserRoleProfile()
    {
        // Role Entity -> UserRoleResponse DTO
        CreateMap<Role, UserRoleResponse>()
            .ForMember(
                dest => dest.RoleId,
                opt => opt.MapFrom(src => src.Id))
            .ForMember(
                dest => dest.RoleName,
                opt => opt.MapFrom(src => src.Name));


        // User Entity -> UserResponse DTO
        CreateMap<User, UserResponse>();
    }
}