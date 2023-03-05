using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
    
}