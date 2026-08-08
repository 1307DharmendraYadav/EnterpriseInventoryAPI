using AutoMapper;
using EnterpriseInventory.Application.Features.UserPermissions.DTOs;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.UserPermissions.Mappings;

public sealed class UserPermissionProfile : Profile
{
    public UserPermissionProfile()
    {
        // Entity -> Response DTO
        CreateMap<UserPermission, UserPermissionResponse>();

        // Create Request -> Entity
        CreateMap<CreateUserPermissionRequest, UserPermission>();

        // Update Request -> Existing Entity
        CreateMap<UpdateUserPermissionRequest, UserPermission>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.UserId, o => o.Ignore())
            .ForMember(d => d.PermissionId, o => o.Ignore());
    }
}