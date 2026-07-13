using AutoMapper;
using EnterpriseInventory.Application.Features.Authentication.DTOs;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Mappings;

public class AuthenticationProfile : Profile
{
    public AuthenticationProfile()
    {
        CreateMap<RegisterRequest, User>();

        CreateMap<User, RegisterResponse>();
    }
}