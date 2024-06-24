
using Microsoft.EntityFrameworkCore;
using ModeloApi.Domain.Entities;
using ModeloApi.Domain.Interfaces;
using ModeloApi.Infra.Data.Context;

namespace ModeloApi.Infra.Data.Repositories;
public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _dbContext;

    public UserRepository(ApplicationDbContext applicationDbContext)
    {
        _dbContext = applicationDbContext;
    }

    public async Task<ICollection<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }
    public async Task<User> GetUserByName(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));    
    }

    public async Task<bool> IsNameAlreadyExists(string name)
    {
        return await _dbContext.Users.AnyAsync(x => x.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<User> CreateUserAsync(User user)
    {
        _dbContext.Add(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _dbContext.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}
