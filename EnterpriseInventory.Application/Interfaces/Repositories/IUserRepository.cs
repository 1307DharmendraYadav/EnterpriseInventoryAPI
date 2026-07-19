using EnterpriseInventory.Domain.Entities;

namespace EnterpriseInventory.Application.Interfaces.Repositories;

public interface IUserRepository
{
    Task<bool> ExistsByUsernameAsync(string username);

    Task<bool> ExistsByEmailAsync(string email);
    
    Task<User?> GetByLoginAsync(string login);

    Task<User> AddAsync(User user);
}