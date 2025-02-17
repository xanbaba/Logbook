using AutoMapper;
using Logbook.DataAccess;
using Logbook.Entities;
using Logbook.Features.UsersManagement.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Logbook.Features.UsersManagement.Services;

public class UsersContext(AppDbContext dbContext, IMapper mapper) : IUsersContext
{
    public Task<IEnumerable<User>> GetUsersAsync()
    {
        return Task.FromResult<IEnumerable<User>>(dbContext.Users.AsQueryable());
    }

    public Task<IEnumerable<User>> GetUsersByRoleAsync(UserRole role)
    {
        return Task.FromResult<IEnumerable<User>>(dbContext.Users.Where(u => u.Role == role).AsQueryable());
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByLoginAsync(string login)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> AddUserAsync(User user)
    {
        EnsureUserDataIsUnique(user);
        
        user.Id = Guid.CreateVersion7();
        var entity = dbContext.Users.Add(user).Entity;
        
        await dbContext.SaveChangesAsync();

        return entity;
    }

    public Task<User> DeleteUserAsync(User user)
    {
        return DeleteUserAsync(user.Id);
    }

    public async Task<User> DeleteUserAsync(Guid id)
    {
        var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
        if (user is null)
        {
            throw new UserNotFoundException("User was not found");
        }

        dbContext.Users.Remove(user);
        await dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        EnsureUserDataIsUnique(user);
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        if (existingUser is null)
        {
            throw new UserNotFoundException("User was not found");
        }
        
        mapper.Map(user, existingUser);

        await dbContext.SaveChangesAsync();

        return existingUser;
    }

    private void EnsureUserDataIsUnique(User user)
    {
        // Checks if db already has a user with the same Email or Login
        var existingUser =
            dbContext.Users.FirstOrDefault(u => u.Id != user.Id && ((u.Email != null && u.Email == user.Email) || u.Login == user.Login));

        if (existingUser == null) return;
        
        if (existingUser.Email is not null && existingUser.Email == user.Email)
        {
            throw new UniqueConstraintException("Email already exists");
        }

        if (existingUser.Login == user.Login)
        {
            throw new UniqueConstraintException("Login already exists");
        }
    }
}