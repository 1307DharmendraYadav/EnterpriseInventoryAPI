using EnterpriseInventory.API.Helpers;
using EnterpriseInventory.Application.Authorization;
using EnterpriseInventory.Application.Authorization.Attributes;
using EnterpriseInventory.Application.Features.EffectivePermissions.DTOs;
using EnterpriseInventory.Application.Features.EffectivePermissions.Interfaces;
using EnterpriseInventory.Application.Features.EffectivePermissions.Services;
using EnterpriseInventory.Application.Features.UserPermissions.DTOs;
using EnterpriseInventory.Application.Features.UserPermissions.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseInventory.API.Controllers
{
    /// <summary>
    /// Provides APIs for managing user-specific permission overrides.
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("api/users/{userId:int}/permissions")]
    public sealed class UserPermissionsController : ControllerBase
    {
        private readonly IUserPermissionService _userPermissionService;
        private readonly IEffectivePermissionService _effectivePermissionService;
        private readonly IPermissionBreakdownService _permissionBreakdownService;

        public UserPermissionsController(
            IUserPermissionService userPermissionService,
            IEffectivePermissionService effectivePermissionService,
            IPermissionBreakdownService permissionBreakdownService)
        {
            _userPermissionService = userPermissionService;
            _effectivePermissionService = effectivePermissionService;
            _permissionBreakdownService = permissionBreakdownService;
        }

        /// <summary>
        /// Retrieves all permission overrides assigned directly to a user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>User permission overrides.</returns>
        [HttpGet]
        [HasPermission(PermissionConstants.UserPermission.View)]
        public async Task<ActionResult<IEnumerable<UserPermissionResponse>>> GetByUserId(int userId)
        {
            var userPermissions = await _userPermissionService.GetByUserIdAsync(userId);
            return Ok(ApiResponseFactory.Success(
                userPermissions,
                "User permissions retrieved successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier));
        }

        /// <summary>
        /// Retrieves a specific permission override assigned to a user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="permissionId">Permission identifier.</param>
        /// <returns>User permission override.</returns>
        [HttpGet("{permissionId:int}")]
        [HasPermission(PermissionConstants.UserPermission.View)]
        public async Task<ActionResult<UserPermissionResponse>> Get(int userId, int permissionId)
        {
            var userPermission = await _userPermissionService.GetAsync(
                userId,
                permissionId);

            return Ok(ApiResponseFactory.Success(
                userPermission,
                "User permission retrieved successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier));
        }


        /// <summary>
        /// Creates a new permission override for a user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="request">Permission override request.</param>
        /// <returns>Created permission override.</returns>
        [HttpPost]
        [HasPermission(PermissionConstants.UserPermission.Create)]
        public async Task<ActionResult<UserPermissionResponse>> Create(int userId, CreateUserPermissionRequest request)
        {
            var created = await _userPermissionService.CreateAsync(
                userId,
                request);

            return CreatedAtAction(
                nameof(Get),
                new
                {
                    userId = created.UserId,
                    permissionId = created.PermissionId
                },
                ApiResponseFactory.Success(
                    created,
                    "User permission created successfully.",
                    StatusCodes.Status201Created,
                    HttpContext.TraceIdentifier));
        }


        /// <summary>
        /// Updates an existing user-specific permission override.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="permissionId">Permission identifier.</param>
        /// <param name="request">Updated permission override.</param>
        [HttpPut("{permissionId:int}")]
        [HasPermission(PermissionConstants.UserPermission.Update)]
        public async Task<IActionResult> Update(int userId, int permissionId, UpdateUserPermissionRequest request)
        {
            var updated = await _userPermissionService.UpdateAsync(userId, permissionId, request);

            return Ok(ApiResponseFactory.Success(
                updated,
                "User permission updated successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier));
        }

        /// <summary>
        /// Removes a permission override from a user.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="permissionId">Permission identifier.</param>
        [HttpDelete("{permissionId:int}")]
        [HasPermission(PermissionConstants.UserPermission.Delete)]
        public async Task<IActionResult> Delete(int userId, int permissionId)
        {
            await _userPermissionService.DeleteAsync(userId, permissionId);

            return Ok(ApiResponseFactory.Success(
                data: (object?)null,
                "User permission deleted successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier));
        }


        /// <summary>
        /// Retrieves the effective permissions for a user after applying
        /// role-based permissions and user-specific permission overrides.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <returns>The user's effective permissions.</returns>
        [HttpGet("effective")]
        [HasPermission(PermissionConstants.UserPermission.View)]
        public async Task<ActionResult<IEnumerable<EffectivePermissionResponse>>>
            GetEffectivePermissions(int userId)
        {
            var permissions =
                await _effectivePermissionService
                    .GetEffectivePermissionsAsync(userId);

            return Ok(ApiResponseFactory.Success(
                permissions,
                "Effective permissions retrieved successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier));
        }

        /// <summary>
        /// Retrieves the permission breakdown for a user.
        /// Shows role contributions, user-specific override,
        /// and the final effective permission.
        /// </summary>
        /// <param name="userId">User identifier.</param>
        /// <param name="permissionId">Permission identifier.</param>
        /// <returns>Permission breakdown for the specified user and permission.</returns>
        [HttpGet("{permissionId:int}/breakdown")]
        [HasPermission(PermissionConstants.UserPermission.View)]
        public async Task<ActionResult<PermissionBreakdownResponse>>
            GetPermissionBreakdown(
                int userId,
                int permissionId)
        {
            var breakdown =
                await _permissionBreakdownService
                    .GetPermissionBreakdownAsync(
                        userId,
                        permissionId);

            return Ok(ApiResponseFactory.Success(
                breakdown,
                "Permission breakdown retrieved successfully.",
                StatusCodes.Status200OK,
                HttpContext.TraceIdentifier));
        }
    }
}
