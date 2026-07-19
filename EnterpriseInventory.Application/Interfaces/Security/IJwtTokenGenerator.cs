using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Security;

public interface IJwtTokenGenerator
{
    JwtTokenResult GenerateToken(User user);
}
