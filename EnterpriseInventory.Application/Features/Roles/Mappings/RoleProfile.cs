using AutoMapper;
using EnterpriseInventory.Application.Features.Roles.DTOs;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.Roles.Mappings;

/// <summary>
/// AutoMapper profile for Role mappings.
/// </summary>
public sealed class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<CreateRoleRequest, Role>();

        CreateMap<UpdateRoleRequest, Role>();

        CreateMap<Role, RoleResponse>();
    }
}