using EnterpriseInventory.API.Helpers;
using EnterpriseInventory.Application.Features.Authentication.DTOs;
using EnterpriseInventory.Application.Features.Authentication.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseInventory.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var user = await _authService.RegisterAsync(request);

            var response = ApiResponseFactory.Success(
                user,
                "User registered successfully.",
                StatusCodes.Status201Created,
                HttpContext.TraceIdentifier);

            return StatusCode(StatusCodes.Status201Created,response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var response = await _authService.LoginAsync(request);

            return Ok(
                ApiResponseFactory.Success(
                    data: response,
                    message: "Login successful.",
                    statusCode: StatusCodes.Status200OK,
                    traceId: HttpContext.TraceIdentifier));
        }
    }
}
