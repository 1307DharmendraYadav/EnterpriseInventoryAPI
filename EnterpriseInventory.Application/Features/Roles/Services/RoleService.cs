using AutoMapper;
using EnterpriseInventory.Application.Exceptions;
using EnterpriseInventory.Application.Features.Roles.DTOs;
using EnterpriseInventory.Application.Features.Roles.Interfaces;
using EnterpriseInventory.Application.Interfaces.Repositories;
using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Features.Roles.Services;

/// <summary>
/// Provides business operations for managing roles.
/// </summary>
public sealed class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    private readonly IMapper _mapper;

    public RoleService(IRoleRepository roleRepository,IMapper mapper)
    {
        _roleRepository = roleRepository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<RoleResponse>> GetAllAsync()
    {
        var roles = await _roleRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<RoleResponse>>(roles);
    }

    public async Task<RoleResponse?> GetByIdAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {id} was not found.");
        }

        return _mapper.Map<RoleResponse>(role);
    }

    public async Task<RoleResponse> CreateAsync(CreateRoleRequest request)
    {
        var roleName = request.Name.Trim();

        if (await _roleRepository.ExistsByNameAsync(roleName))
        {
            throw new ConflictException(
                $"Role '{roleName}' already exists.");
        }

        request.Name = roleName;

        var role = _mapper.Map<Role>(request);

        await _roleRepository.AddAsync(role);

        return _mapper.Map<RoleResponse>(role);
    }

    public async Task<RoleResponse> UpdateAsync(int id, UpdateRoleRequest request)
    {
        var roleName = request.Name.Trim();

        var role = await _roleRepository.GetByIdAsync(id);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {id} was not found.");
        }

        if (await _roleRepository.ExistsByNameExcludingIdAsync(roleName, id))
        {
            throw new ConflictException(
                $"Role '{roleName}' already exists.");
        }

        request.Name = roleName;

        _mapper.Map(request, role);

        await _roleRepository.UpdateAsync(role);

        return _mapper.Map<RoleResponse>(role);
    }

    public async Task DeleteAsync(int id)
    {
        var role = await _roleRepository.GetByIdAsync(id);

        if (role is null)
        {
            throw new NotFoundException(
                $"Role with Id {id} was not found.");
        }

        await _roleRepository.DeleteAsync(role);
    }
}
