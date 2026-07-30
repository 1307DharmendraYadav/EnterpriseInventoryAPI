using EnterpriseInventory.API.Helpers;
using EnterpriseInventory.Application.Authorization;
using EnterpriseInventory.Application.Authorization.Attributes;
using EnterpriseInventory.Application.Features.Roles.DTOs;
using EnterpriseInventory.Application.Features.Roles.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseInventory.API.Controllers
{
    /// <summary>
    /// Manages role operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public sealed class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }


        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        [HttpGet]
        [HasPermission(PermissionConstants.Role.View)]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _roleService.GetAllAsync();
            return Ok(ApiResponseFactory.Success(
                 roles,
                 "Roles retrieved successfully.",
                 StatusCodes.Status200OK,
                 HttpContext.TraceIdentifier
                 ));
        }


        /// <summary>
        /// Retrieves a role by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        [HasPermission(PermissionConstants.Role.View)]
        public async Task<IActionResult> GetById(int id)
        {
            var role = await _roleService.GetByIdAsync(id);
            return Ok(ApiResponseFactory.Success(
                 role,
                 "Role retrieved successfully.",
                 StatusCodes.Status200OK,
                 HttpContext.TraceIdentifier
                 ));
        }


        /// <summary>
        /// Creates a new role.
        /// </summary>
        [HttpPost]
        [HasPermission(PermissionConstants.Role.Create)]
        public async Task<IActionResult> Create(CreateRoleRequest request)
        {
            // Returns HTTP 201 Created and generates the Location header
            // pointing to the newly created resource.
            //
            // Example:
            // role.Id = 5
            // nameof(GetById) -> GetById(int id)
            // new { id = role.Id } -> { id = 5 }
            // Generated URL -> GET /api/Role/5

            var role = await _roleService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = role.Id },
                ApiResponseFactory.Success(
                    role,
                    "Role created successfully.",
                    StatusCodes.Status201Created,
                    HttpContext.TraceIdentifier));
        }

        /// <summary>
        /// Updates an existing role.
        /// </summary>
        [HttpPut("{id:int}")]
        [HasPermission(PermissionConstants.Role.Update)]
        public async Task<IActionResult> Update(int id, UpdateRoleRequest request)
        {
            var role = await _roleService.UpdateAsync(id, request);

            return Ok(ApiResponseFactory.Success(
                role,
                "Role updated successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier
                ));
        }


        /// <summary>
        /// Deletes a role.
        /// </summary>
        [HttpDelete("{id:int}")]
        [HasPermission(PermissionConstants.Role.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            await _roleService.DeleteAsync(id);

            return Ok(
                ApiResponseFactory.Success(
                    data: (object?)null,
                    message: "Role deleted successfully.",
                    statusCode: StatusCodes.Status200OK,
                    traceId: HttpContext.TraceIdentifier));
        }
    }
}
