using AutoMapper;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.Permissions.DTOs;
using EnterpriseInventory.Application.Features.Permissions.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.Permissions.Services;

/// <summary>
/// Provides business operations for managing permissions.
/// </summary>
public sealed class PermissionService : IPermissionService
{
    private readonly IPermissionRepository _permissionRepository;
    private readonly IMapper _mapper;

    public PermissionService(
        IPermissionRepository permissionRepository,
        IMapper mapper)
    {
        _permissionRepository = permissionRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PermissionResponse>> GetAllAsync()
    {
        var permissions =
           await _permissionRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<PermissionResponse>>(permissions);
    }

    public async Task<PermissionResponse?> GetByIdAsync(int id)
    {
        var permission = await _permissionRepository.GetByIdAsync(id);

        if (permission is null)
        {
            throw new NotFoundException(
                $"Permission with Id {id} was not found.");
        }

        return _mapper.Map<PermissionResponse>(permission);
    }

    public async Task<PermissionResponse> CreateAsync(CreatePermissionRequest request)
    {
        var permissionName = request.Name.Trim();
        var permissionDescription = request.Description.Trim();

        if (await _permissionRepository.ExistsByNameAsync(permissionName))
        {
            throw new ConflictException(
                $"Permission '{permissionName}' already exists.");
        }

        request.Name = permissionName;
        request.Description = permissionDescription;

        var permission = _mapper.Map<Permission>(request);

        permission = await _permissionRepository.AddAsync(permission);
        return _mapper.Map<PermissionResponse>(permission);
    }

    public async Task<PermissionResponse> UpdateAsync(int id, UpdatePermissionRequest request)
    {
        var permissionName = request.Name.Trim();
        var permissionDescription = request.Description.Trim();

        var permission = await _permissionRepository.GetByIdAsync(id);
        if (permission is null)
        {
            throw new NotFoundException(
                $"Permission with Id {id} was not found.");
        }

        if (await _permissionRepository.ExistsByNameExcludingIdAsync(permissionName, id))
        {
            throw new ConflictException(
                $"Permission '{permissionName}' already exists.");
        }

        request.Name = permissionName;
        request.Description = permissionDescription;

        _mapper.Map(request, permission);

        await _permissionRepository.UpdateAsync(permission);

        return _mapper.Map<PermissionResponse>(permission);
    }

    public async Task DeleteAsync(int id)
    {
        var permission =
             await _permissionRepository.GetByIdAsync(id);

        if (permission is null)
        {
            throw new NotFoundException(
                $"Permission with Id {id} was not found.");
        }

        await _permissionRepository.DeleteAsync(permission);
    }
}