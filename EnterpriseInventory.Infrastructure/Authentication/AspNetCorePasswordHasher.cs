using EnterpriseInventory.Application.Interfaces.Security;
using EnterpriseInventory.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace EnterpriseInventory.Infrastructure.Authentication;

public class AspNetCorePasswordHasher : IPasswordHasher
{
    private readonly Microsoft.AspNetCore.Identity.PasswordHasher<User>
     _passwordHasher = new();

    public string Hash(string password)
    {
        return _passwordHasher.HashPassword(null!, password);
    }

    public bool Verify(string password, string passwordHash)
    {
        var result = _passwordHasher.VerifyHashedPassword(
            null!,
            passwordHash,
            password);

        return result != PasswordVerificationResult.Failed;
    }
}