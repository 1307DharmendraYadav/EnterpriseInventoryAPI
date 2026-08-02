using EnterpriseInventory.API.Helpers;
using EnterpriseInventory.Application.Authorization;
using EnterpriseInventory.Application.Authorization.Attributes;
using EnterpriseInventory.Application.Features.UserRole.DTOs;
using EnterpriseInventory.Application.Features.UserRole.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseInventory.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize]
public sealed class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _service;

    public UserRoleController(IUserRoleService service)
    {
        _service = service;
    }
    /// <summary>
    /// Gets all roles assigned to a user.
    /// </summary>
    [HttpGet("{userId:int}/roles")]
    [HasPermission(PermissionConstants.User.View)]
    public async Task<IActionResult> GetUserRoles(int userId)
    {
        var roles = await _service.GetUserRolesAsync(userId);
        return Ok(
            ApiResponseFactory.Success(
                roles,
                "User roles retrieved successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier)
            );
    }

    /// <summary>
    /// Replaces all roles assigned to a user.
    /// </summary>
    [HttpPut("{userId:int}/roles")]
    [HasPermission(PermissionConstants.User.Update)]
    public async Task<IActionResult> ReplaceUserRoles(
    int userId, ReplaceUserRolesRequest request)
    {
        await _service.ReplaceUserRolesAsync(userId, request);

        return Ok(
            ApiResponseFactory.Success(
                data: (object?)null,
                message: "User role assignments updated successfully.",
                statusCode: StatusCodes.Status200OK,
                traceId: HttpContext.TraceIdentifier));
    }

    /// <summary>
    /// Gets all users assigned to a role.
    /// </summary>
    [HttpGet("~/api/roles/{roleId:int}/users")]
    [HasPermission(PermissionConstants.User.View)]
    public async Task<IActionResult> GetUsersByRole(int roleId)
    {
        var users = await _service.GetUsersByRoleAsync(roleId);

        return Ok(
            ApiResponseFactory.Success(
                users,
                "Users retrieved successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier)
            );
    }
}