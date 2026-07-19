using EnterpriseInventory.Application.Common.Settings;
using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EnterpriseInventory.Infrastructure.Security;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtTokenGenerator"/> class.
    /// </summary>
    /// <param name="jwtOptions">
    /// Strongly typed JWT configuration loaded using the ASP.NET Core Options Pattern.
    /// </param>
    public JwtTokenGenerator(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    /// <summary>
    /// Generates a signed JSON Web Token (JWT) for the authenticated user.
    /// </summary>
    /// <param name="user">
    /// The authenticated user for whom the JWT will be generated.
    /// </param>
    /// <returns>
    /// A signed JWT string containing the user's identity claims.
    /// </returns>
    public JwtTokenResult GenerateToken(User user)
    {
        /*
         ---------------------------------------------------------------------
            Claims represent the authenticated user's identity.
            Every claim added here becomes part of the JWT payload and is
            available throughout the lifetime of the authenticated session
            (until the JWT expires).
         ---------------------------------------------------------------------
         */

        var claims = new List<Claim>
        {
            /* =============================================================
             JWT Registered Claims (RFC 7519)
            
             Recommended for:
             • REST APIs
             • React / Angular / Vue
             • Flutter / .NET MAUI
             • Mobile Applications
             • Microservices
             • OAuth 2.0
             • OpenID Connect
             • Microsoft Entra ID (Azure AD)
             • Single Sign-On (SSO)
            
             These are industry-standard claims that make JWT tokens
             portable across different platforms and identity providers.
             =============================================================
            */

            // Unique identifier of the authenticated user.
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),

            // Username of the authenticated user.
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),

            // Email address of the authenticated user.
            new Claim(JwtRegisteredClaimNames.Email, user.Email),


            /* =============================================================
             Microsoft Claims
            
             Recommended for:
             • ASP.NET Core Identity
             • ASP.NET MVC
             • Razor Pages
             • Blazor
             • ClaimsPrincipal
             • Windows Authentication
            
             These claims improve compatibility with Microsoft's
             authentication and authorization pipeline.
            
             NOTE:
             We keep both JWT Registered Claims and Microsoft Claims
             so that the token works seamlessly across modern clients
             (React, Angular, Mobile Apps, Microservices) while also
             supporting ASP.NET Core Identity and future MVC/Blazor
             applications without requiring any code changes.
             =============================================================
            */

            // Unique identifier of the authenticated user.
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),

            // Display name of the authenticated user.
            new Claim(ClaimTypes.Name, user.Username),

            // Email address of the authenticated user.
            new Claim(ClaimTypes.Email, user.Email)
        };

        /* 
         ---------------------------------------------------------------------
          Create the symmetric security key used to digitally sign the JWT.
          The secret key from configuration is converted into a byte array
          because cryptographic algorithms operate on binary data.
         ---------------------------------------------------------------------
        */

        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_jwtSettings.Key));

        /*  ---------------------------------------------------------------------
           Create signing credentials using the symmetric security key.
          
           SecurityAlgorithms.HmacSha256 represents the HS256 algorithm,
           which digitally signs the JWT to ensure its integrity and authenticity.
           ---------------------------------------------------------------------
        */

        var signingCredentials = new SigningCredentials(securityKey,
                                  SecurityAlgorithms.HmacSha256);

        /*  ---------------------------------------------------------------------
             Create the JWT token.
            
             The token contains:
             • Issuer    - Who created the token
             • Audience  - Who the token is intended for
             • Claims    - User identity information
             • Expiration- Token validity period
             • Signing Credentials - Used to digitally sign the token
             ---------------------------------------------------------------------
        */

        // Calculate expiry ONCE
        var expiryUtc = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: expiryUtc,
            signingCredentials: signingCredentials);


        /*  ---------------------------------------------------------------------
          Serialize the JwtSecurityToken into the standard compact JWT format.
         
          JwtSecurityTokenHandler converts the in-memory JwtSecurityToken object
          into a Base64Url encoded JWT string in the format:
         
          Header.Payload.Signature
          ---------------------------------------------------------------------
        */

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtToken = tokenHandler.WriteToken(token);

        return new JwtTokenResult
        {
            Token = jwtToken,
            ExpiresAtUtc = expiryUtc
        };
    }
}