using EnterpriseInventory.API.Helpers;
using EnterpriseInventory.Application.Authorization;
using EnterpriseInventory.Application.Authorization.Attributes;
using EnterpriseInventory.Application.Features.RolePermissions.DTOs;
using EnterpriseInventory.Application.Features.RolePermissions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseInventory.API.Controllers;

/// <summary>
/// Manages permission assignments for roles.
/// </summary>
[Route("api/roles/{roleId:int}/permissions")]
[ApiController]
[Authorize]
public sealed class RolePermissionController : ControllerBase
{
    private readonly IRolePermissionService _rolePermissionService;

    public RolePermissionController(
        IRolePermissionService rolePermissionService)
    {
        _rolePermissionService = rolePermissionService;
    }

    /// <summary>
    /// Retrieves all permissions assigned to the specified role.
    /// </summary>
    [HttpGet]
    [HasPermission(PermissionConstants.Role.View)]
    public async Task<IActionResult> GetByRoleId(int roleId)
    {
        var response =
            await _rolePermissionService.GetByRoleIdAsync(roleId);

        return Ok(
            ApiResponseFactory.Success(
                response,
                "Role permissions retrieved successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier));
    }

    /// <summary>
    /// Replaces all permissions assigned to the specified role.
    /// </summary>
    [HttpPut]
    [HasPermission(PermissionConstants.Role.Update)]
    public async Task<IActionResult> AssignPermissions(
        int roleId,
        AssignPermissionsRequest request)
    {
        await _rolePermissionService.AssignPermissionsAsync(
            roleId,
            request);

        return Ok(
            ApiResponseFactory.Success(
                data: (object?)null,
                message: "Role permissions updated successfully.",
                statusCode: StatusCodes.Status200OK,
                traceId: HttpContext.TraceIdentifier));
    }
}