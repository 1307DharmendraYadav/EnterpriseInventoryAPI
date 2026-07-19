namespace EnterpriseInventory.Application.Features.Authentication.DTOs;

/// <summary>
/// Represents the response returned after a successful login.
/// </summary>
public class LoginResponse
{
    public int UserId { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAtUtc { get; set; }
}