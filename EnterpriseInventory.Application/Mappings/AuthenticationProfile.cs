using AutoMapper;
using EnterpriseInventory.Application.Features.Authentication.DTOs;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Mappings;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        // Register
        CreateMap<RegisterRequest, User>();

        CreateMap<User, RegisterResponse>();

        // Login
        CreateMap<User, LoginResponse>()
            .ForMember(
                dest => dest.UserId,
               opt => opt.MapFrom(src => src.Id));
    }
}