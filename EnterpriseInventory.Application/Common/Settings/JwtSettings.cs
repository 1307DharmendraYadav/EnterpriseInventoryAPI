namespace EnterpriseInventory.Application.Common.Settings;

/// <summary>
/// Represents the JWT (JSON Web Token) configuration settings loaded from
/// the <c>JwtSettings</c> section of <c>appsettings.json</c>.
///
/// These settings are used during:
/// • JWT token generation
/// • JWT token validation
/// • Authentication configuration
///
/// The values are bound using the ASP.NET Core Options Pattern
/// (<see cref="Microsoft.Extensions.Options.IOptions{TOptions}"/>).
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Secret key used to digitally sign and validate JWT tokens.
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Identifies the application that issues the JWT.
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Identifies the intended recipient(s) of the JWT.
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Specifies the JWT expiration time in minutes.
    /// </summary>
    public int ExpiryMinutes { get; set; }
}