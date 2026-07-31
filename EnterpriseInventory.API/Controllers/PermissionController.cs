using EnterpriseInventory.API.Helpers;
using EnterpriseInventory.Application.Authorization;
using EnterpriseInventory.Application.Authorization.Attributes;
using EnterpriseInventory.Application.Features.Permissions.DTOs;
using EnterpriseInventory.Application.Features.Permissions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseInventory.API.Controllers
{
    /// <summary>
    /// Manages permission operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public sealed class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Retrieves all permissions.
        /// </summary>
        [HttpGet]
        [HasPermission(PermissionConstants.Permission.View)]
        public async Task<IActionResult> GetAll()
        {
            var permissions = await _permissionService.GetAllAsync();

            return Ok(
                ApiResponseFactory.Success(
                    permissions,
                    "Permissions retrieved successfully.",
                    StatusCodes.Status200OK,
                    HttpContext.TraceIdentifier
                ));
        }

        /// <summary>
        /// Retrieves a permission by Id.
        /// </summary>
        [HttpGet("{id:int}")]
        [HasPermission(PermissionConstants.Permission.View)]
        public async Task<IActionResult> GetById(int id)
        {
            var permission = await _permissionService.GetByIdAsync(id);

            return Ok(
                ApiResponseFactory.Success(
                    permission,
                    "Permission retrieved successfully.",
                    StatusCodes.Status200OK,
                    HttpContext.TraceIdentifier
                ));
        }

        /// <summary>
        /// Creates a new permission.
        /// </summary>
        [HttpPost]
        [HasPermission(PermissionConstants.Permission.Create)]
        public async Task<IActionResult> Create(CreatePermissionRequest request)
        {
            var permission = await _permissionService.CreateAsync(request);

            return CreatedAtAction(
                nameof(GetById),
                new { id = permission.Id },
                ApiResponseFactory.Success(
                    permission,
                    "Permission created successfully.",
                    StatusCodes.Status201Created,
                    HttpContext.TraceIdentifier
                ));
        }

        /// <summary>
        /// Updates an existing permission.
        /// </summary>
        [HttpPut("{id:int}")]
        [HasPermission(PermissionConstants.Permission.Update)]
        public async Task<IActionResult> Update(int id,UpdatePermissionRequest request)
        {
            var permission = await _permissionService.UpdateAsync(id, request);

            return Ok(
                ApiResponseFactory.Success(
                    permission,
                    "Permission updated successfully.",
                    StatusCodes.Status200OK,
                    HttpContext.TraceIdentifier
                ));
        }

        /// <summary>
        /// Deletes a permission.
        /// </summary>
        [HttpDelete("{id:int}")]
        [HasPermission(PermissionConstants.Permission.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            await _permissionService.DeleteAsync(id);

            return Ok(
                ApiResponseFactory.Success(
                    data: (object?)null,
                    message: "Permission deleted successfully.",
                    statusCode: StatusCodes.Status200OK,
                    traceId: HttpContext.TraceIdentifier));
        }
    }
}