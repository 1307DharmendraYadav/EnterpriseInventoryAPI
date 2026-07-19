namespace EnterpriseInventory.Application.Features.Authentication.DTOs;

/// <summary>
/// Represents the login request submitted by the client.
/// </summary>
public class LoginRequest
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}