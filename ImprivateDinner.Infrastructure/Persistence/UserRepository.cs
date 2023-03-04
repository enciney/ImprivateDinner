using ImprivateDinner.Application.Interfaces.Persistence;
using ImprivateDinner.Domain.Entities;

namespace ImprivateDinner.Infrastructure.Persistence;

public class UserRepository : IUserRepository
{
    private static readonly List<User> users = new();
    public void Add(User user)
    {
        users.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return users.SingleOrDefault(s => s.Email == email);
    }
}