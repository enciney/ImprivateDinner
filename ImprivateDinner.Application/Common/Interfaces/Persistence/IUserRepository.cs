using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
    
}