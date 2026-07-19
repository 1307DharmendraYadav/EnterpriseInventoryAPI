namespace EnterpriseInventory.Application.Interfaces.Security;

public sealed class JwtTokenResult
{
    public string Token { get; init; } = string.Empty;

    public DateTime ExpiresAtUtc { get; init; }
}