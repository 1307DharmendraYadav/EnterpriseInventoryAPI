using AutoMapper;
using EnterpriseInventory.Application.Common.Settings;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.Authentication.DTOs;
using EnterpriseInventory.Application.Features.Authentication.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Domain.Entities;
using Microsoft.Extensions.Options;

namespace EnterpriseInventory.Application.Features.Authentication.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public AuthService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMapper mapper,
        IJwtTokenGenerator jwtTokenGenerator,
        IOptions<JwtSettings> jwtOption)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
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

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var login = request.Login.Trim();

        var user = await _userRepository.GetByLoginAsync(login);

        if (user is null)
        {
            throw new UnauthorizedException(
                "Invalid username or password.");
        }

        var passwordValid =_passwordHasher.Verify(request.Password,user.PasswordHash);

        if (!passwordValid)
        {
            throw new UnauthorizedException(
                "Invalid username or password.");
        }

        var response = _mapper.Map<LoginResponse>(user);

        var jwtResult = _jwtTokenGenerator.GenerateToken(user);

        response.Token = jwtResult.Token;
        response.ExpiresAtUtc = jwtResult.ExpiresAtUtc;

        return response;
    }
}
