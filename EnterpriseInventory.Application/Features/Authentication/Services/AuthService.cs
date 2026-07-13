using AutoMapper;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.Authentication.DTOs;
using EnterpriseInventory.Application.Features.Authentication.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var username = request.Username.Trim();
        var email = request.Email.Trim();

        // (We'll discuss password normalization separately.)

        if (await _userRepository.ExistsByUsernameAsync(username))
        {
            throw new ConflictException(
                $"Username '{username}' already exists.");
        }

        if (await _userRepository.ExistsByEmailAsync(email))
        {
            throw new ConflictException(
                $"Email '{email}' is already registered.");
        }

        request.Username = username;

        request.Email = email;

        var user = _mapper.Map<User>(request);

        user.PasswordHash = _passwordHasher.Hash(request.Password);

        var createdUser = await _userRepository.AddAsync(user);

        return _mapper.Map<RegisterResponse>(createdUser);
    }
}
