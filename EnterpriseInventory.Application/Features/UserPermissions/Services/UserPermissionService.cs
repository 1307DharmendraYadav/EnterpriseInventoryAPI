using AutoMapper;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.UserPermissions.DTOs;
using EnterpriseInventory.Application.Features.UserPermissions.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.UserPermissions.Services;

/// <summary>
/// Provides business logic for managing user-specific permission overrides.
/// </summary>
public sealed class UserPermissionService : IUserPermissionService
{
    private readonly IUserPermissionRepository _userPermissionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public UserPermissionService(
        IUserPermissionRepository userPermissionRepository,
        IUserRepository userRepository,
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _userPermissionRepository = userPermissionRepository;
        _userRepository = userRepository;
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all permission overrides assigned directly to a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <returns>A collection of user-specific permission overrides.</returns>
    public async Task<IEnumerable<UserPermissionResponse>> GetByUserIdAsync(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new NotFoundException($"User with Id {userId} was not found.");
        }

        var userPermissions = await _userPermissionRepository.GetByUserIdAsync(userId);

        return _mapper.Map<IEnumerable<UserPermissionResponse>>(userPermissions);
    }

    /// <summary>
    /// Retrieves a specific permission override assigned to a user.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="permissionId">The permission identifier.</param>
    /// <returns>The matching permission override.</returns>
    public async Task<UserPermissionResponse?> GetAsync(
        int userId,
        int permissionId)
    {
        var userPermission = await _userPermissionRepository.GetAsync(
            userId,
            permissionId);

        if (userPermission is null)
        {
            throw new NotFoundException(
                $"User permission override was not found for UserId '{userId}' and PermissionId '{permissionId}'.");
        }

        return _mapper.Map<UserPermissionResponse>(userPermission);
    }

    /// <summary>
    /// Creates a new user-specific permission override.
    /// </summary>
    /// <param name="request">The permission override request.</param>
    /// <returns>The created permission override.</returns>
    public async Task<UserPermissionResponse> CreateAsync(int userId,CreateUserPermissionRequest request)
    {
        var user = await _userRepository.GetByIdAsync(userId);

        if (user is null)
        {
            throw new NotFoundException(
                $"User with Id {userId} was not found.");
        }

        var permission = await _permissionRepository.GetByIdAsync(request.PermissionId);

        if (permission is null)
        {
            throw new NotFoundException(
                $"Permission with Id {request.PermissionId} was not found.");
        }

        var existing = await _userPermissionRepository.GetAsync(userId,request.PermissionId);

        if (existing is not null)
        {
            throw new ValidationException(
                "A permission override already exists for this user.");
        }

        //var entity = _mapper.Map<UserPermission>(request);
        var entity = new UserPermission
        {
            UserId = userId,
            PermissionId = request.PermissionId,
            IsAllowed = request.IsAllowed
        };

        await _userPermissionRepository.AddAsync(entity);

        return _mapper.Map<UserPermissionResponse>(entity);
    }

    /// <summary>
    /// Updates an existing user-specific permission override.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="permissionId">The permission identifier.</param>
    /// <param name="request">Updated permission override values.</param>
    public async Task<UserPermissionResponse> UpdateAsync(
        int userId,
        int permissionId,
        UpdateUserPermissionRequest request)
    {
        var existing = await _userPermissionRepository.GetAsync(
            userId,
            permissionId);

        if (existing is null)
        {
            throw new NotFoundException(
                $"User permission override was not found for UserId '{userId}' and PermissionId '{permissionId}'.");
        }

        _mapper.Map(request, existing);

        await _userPermissionRepository.UpdateAsync(existing);
        return _mapper.Map<UserPermissionResponse>(existing);
    }

    /// <summary>
    /// Removes a user-specific permission override.
    /// </summary>
    /// <param name="userId">The user identifier.</param>
    /// <param name="permissionId">The permission identifier.</param>
    public async Task DeleteAsync(
        int userId,
        int permissionId)
    {
        var existing = await _userPermissionRepository.GetAsync(
            userId,
            permissionId);

        if (existing is null)
        {
            throw new NotFoundException(
                $"User permission override was not found for UserId '{userId}' and PermissionId '{permissionId}'.");
        }

        await _userPermissionRepository.DeleteAsync(existing);
    }
}