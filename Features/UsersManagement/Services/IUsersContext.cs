using Logbook.Entities;

namespace Logbook.Features.UsersManagement.Services;

public interface IUsersContext
{
    // Get
    public Task<IQueryable<User>> GetUsersAsync();
    public Task<IQueryable<User>> GetUsersByRoleAsync(UserRole role);
    public Task<User?> GetUserByIdAsync(Guid id);
    public Task<User?> GetUserByLoginAsync(string login);
    public Task<User?> GetUserByEmailAsync(string email);
    
    // Add
    public Task<User> AddUserAsync(User user);
    
    // Delete
    public Task<User> DeleteUserAsync(User user);
    public Task<User> DeleteUserAsync(Guid id);
    
    // Update
    public Task<User> UpdateUserAsync(User user);
}