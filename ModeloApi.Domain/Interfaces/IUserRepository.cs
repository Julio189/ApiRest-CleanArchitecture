
using ModeloApi.Domain.Entities;

namespace ModeloApi.Domain.Interfaces;
public interface IUserRepository
{
    Task<ICollection<User>> GetAllUsersAsync();
    Task<User> GetUserByIdAsync(int id);
    Task<bool> IsNameAlreadyExists(string name);
    Task<User> GetUserByName(string name);
    Task<User> CreateUserAsync(User user);
    Task UpdateUserAsync(User user);
    Task DeleteUserAsync(User user);
}
