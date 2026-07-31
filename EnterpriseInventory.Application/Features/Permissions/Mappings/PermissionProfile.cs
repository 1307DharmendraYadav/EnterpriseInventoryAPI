using AutoMapper;
using EnterpriseInventory.Application.Features.Permissions.DTOs;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.Permissions.Mappings;

/// <summary>
/// AutoMapper profile for Permission mappings.
/// </summary>
public sealed class PermissionProfile : Profile
{
    public PermissionProfile()
    {
        CreateMap<CreatePermissionRequest, Permission>();

        CreateMap<UpdatePermissionRequest, Permission>();

        CreateMap<Permission, PermissionResponse>();
    }
}