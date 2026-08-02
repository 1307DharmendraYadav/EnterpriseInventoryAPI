using AutoMapper;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.UserRole.DTOs;
using EnterpriseInventory.Application.Features.UserRole.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;

namespace EnterpriseInventory.Application.Features.UserRole.Services;

public sealed class UserRoleService : IUserRoleService
{
    private readonly IUserRoleRepository _repository;

    private readonly IUserRepository _userRepository;

    private readonly IRoleRepository _roleRepository;

    private readonly IMapper _mapper;

    public UserRoleService(
        IUserRoleRepository repository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IMapper mapper)
    {
        _repository = repository;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _mapper = mapper;
    }

    public async Task ReplaceUserRolesAsync(
    int userId,
    ReplaceUserRolesRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new NotFoundException(
                $"User with Id {userId} was not found.");
        }

        var roleIds = request.RoleIds
                .Distinct()
                .ToList();

        if (!roleIds.Any())
            throw new ValidationException("At least one role must be assigned.");

        var existingRoleIds = await _roleRepository.GetExistingRoleIdsAsync(roleIds);

        var missingRoleIds = roleIds
            .Except(existingRoleIds)
            .ToList();

        if (missingRoleIds.Any())
        {
            throw new NotFoundException(
                $"Roles not found: {string.Join(", ", missingRoleIds)}");
        }

        await _repository.ReplaceUserRolesAsync(userId, roleIds);
    }

    public async Task<IEnumerable<UserResponse>> GetUsersByRoleAsync(int roleId)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);

        if (role is null)
            throw new NotFoundException(
                $"Role with Id {roleId} was not found.");

        var users = await _repository.GetUsersByRoleIdAsync(roleId);

        return _mapper.Map<IEnumerable<UserResponse>>(users);
    }

    public async Task<IEnumerable<UserRoleResponse>> GetUserRolesAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
            throw new NotFoundException(
                $"User with Id {userId} was not found.");

        var userRoles = await _repository.GetRolesByUserIdAsync(userId);

        return _mapper.Map<IEnumerable<UserRoleResponse>>(userRoles);
    }

}