using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using FluentValidation;
using Logbook.Entities;
using Logbook.Features.AuthFeature;
using Logbook.Features.UsersManagement.Exceptions;
using Logbook.Features.UsersManagement.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Logbook.Features.UsersManagement;

public abstract class UsersManagementEndpointMapper : IEndpointMapper
{
    public static void Map(IEndpointRouteBuilder source)
    {
        // GET /users?offset=10&count=20
        source.MapGet("/users", GetUsers);

        // GET /users/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapGet("/users/{id:guid}", GetUser);

        // GET /users/by-email/ooo0o@gmail.com
        source.MapGet("/users/email/{email}", GetUserByEmail);

        // GET /users/by-login/xanbaba
        source.MapGet("/users/login/{login}", GetUserByLogin);

        // POST /users
        source.MapPost("/users", CreateUser);

        // PUT /users/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapPut("/users/{id:guid}", UpdateUser);

        // DELETE /users/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapDelete("/users/{id:guid}", DeleteUser);

        // GET /users/students?offset=10&count=20
        source.MapGet("/users/students", GetStudents);
        
        // GET /users/teachers?offset=10&count=20
        source.MapGet("/users/teachers", GetTeachers);
        
        // GET /users/admins?offset=10&count=20
        source.MapGet("/users/admins", GetAdmins);
    }

    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Local")]
    private record GetUserResponse(UserDTO User, Guid Id);

    private static async Task<Results<Ok<GetUserResponse>, NotFound>> GetUser
    (
        [FromRoute] Guid id,
        [FromServices] IMapper mapper,
        [FromServices] IUsersContext usersContext
    )
    {
        var user = await usersContext.GetUserByIdAsync(id);
        if (user == null)
        {
            return TypedResults.NotFound();
        }

        return TypedResults.Ok(new GetUserResponse(mapper.Map<User, UserDTO>(user), id));
    }
    
    private static async Task<Results<Ok<UserDTO>, NotFound<string>>> GetUserByEmail(string email, IUsersContext usersContext, IMapper mapper)
    {
        var user = await usersContext.GetUserByEmailAsync(email);

        if (user is null)
        {
            return TypedResults.NotFound($"User with email {email} not found");
        }

        return TypedResults.Ok(mapper.Map<User, UserDTO>(user));
    }
    
    private static async Task<Results<Ok<UserDTO>, NotFound<string>>> GetUserByLogin(string login, IUsersContext usersContext, IMapper mapper)
    {
        var user = await usersContext.GetUserByLoginAsync(login);

        if (user is null)
        {
            return TypedResults.NotFound($"User with login {login} not found");
        }

        return TypedResults.Ok(mapper.Map<User, UserDTO>(user));
    }

    private static async Task<Ok<GetUserResponse[]>> GetUsers
    (
        [FromServices] IUsersContext usersContext,
        [FromServices] IMapper mapper,
        int offset = 0,
        int count = 30
    )
    {
        if (count > 100) count = 100;

        var usersRaw = await usersContext.GetUsersAsync();
        var users = usersRaw.Skip(offset).Take(count).Select(u => new GetUserResponse(mapper.Map<User, UserDTO>(u), u.Id))
            .ToArray();

        return TypedResults.Ok(users);
    }
    
    private static async Task<Ok<GetUserResponse[]>> GetStudents
    (
        [FromServices] IUsersContext usersContext,
        [FromServices] IMapper mapper,
        int offset = 0,
        int count = 30
    )
    {
        if (count > 100) count = 100;

        var usersRaw = await usersContext.GetUsersByRoleAsync(UserRole.Student);
        var users = usersRaw.Skip(offset).Take(count).Select(u => new GetUserResponse(mapper.Map<User, UserDTO>(u), u.Id))
            .ToArray();

        return TypedResults.Ok(users);
    }
    
    private static async Task<Ok<GetUserResponse[]>> GetTeachers
    (
        [FromServices] IUsersContext usersContext,
        [FromServices] IMapper mapper,
        int offset = 0,
        int count = 30
    )
    {
        if (count > 100) count = 100;

        var usersRaw = await usersContext.GetUsersByRoleAsync(UserRole.Teacher);
        var users = usersRaw.Skip(offset).Take(count).Select(u => new GetUserResponse(mapper.Map<User, UserDTO>(u), u.Id))
            .ToArray();

        return TypedResults.Ok(users);
    }
    
    private static async Task<Ok<GetUserResponse[]>> GetAdmins
    (
        [FromServices] IUsersContext usersContext,
        [FromServices] IMapper mapper,
        int offset = 0,
        int count = 30
    )
    {
        if (count > 100) count = 100;

        var usersRaw = await usersContext.GetUsersByRoleAsync(UserRole.Admin);
        var users = usersRaw.Skip(offset).Take(count).Select(u => new GetUserResponse(mapper.Map<User, UserDTO>(u), u.Id))
            .ToArray();

        return TypedResults.Ok(users);
    }

    private static async Task<Results<Created, BadRequest<string>, Conflict<string>>> CreateUser
    (
        [FromServices] IUsersContext usersContext,
        [FromServices] IMapper mapper,
        [FromServices] IValidator<UserDTO> validator,
        [FromBody] UserDTO? dto
    )
    {
        if (dto is null)
        {
            return TypedResults.BadRequest("Provide user data");
        }

        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.ToString("\n"));
        }

        var mappedUser = mapper.Map<UserDTO, User>(dto);

        var hashedPassword = PasswordHasher.HashPassword(dto.Password!);
        mappedUser.PasswordHash = hashedPassword;

        try
        {
            var user = await usersContext.AddUserAsync(mappedUser);

            return TypedResults.Created($"{user.Id}");
        }
        catch (UniqueConstraintException e)
        {
            return TypedResults.Conflict(e.Message);
        }
    }

    private static async Task<Results<BadRequest<string>, NotFound<string>, Ok, Conflict<string>>> UpdateUser
    (
        [FromRoute] Guid id,
        [FromBody] UserDTO? dto,
        [FromServices] IUsersContext usersContext,
        [FromServices] IMapper mapper,
        [FromServices] IValidator<UserDTO> validator
    )
    {
        if (dto is null)
        {
            return TypedResults.BadRequest("Provide user data");
        }

        var validationResult = await validator.ValidateAsync(dto);
        if (!validationResult.IsValid)
        {
            return TypedResults.BadRequest(validationResult.ToString("\n"));
        }

        var mappedUser = mapper.Map<UserDTO, User>(dto);
        mappedUser.Id = id;
        mappedUser.PasswordHash = PasswordHasher.HashPassword(dto.Password!);

        try
        {
            await usersContext.UpdateUserAsync(mappedUser);
            return TypedResults.Ok();
        }
        catch (UniqueConstraintException e)
        {
            return TypedResults.Conflict(e.Message);
        }
        catch (UserNotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }

    private static async Task<Results<NotFound<string>, Ok>> DeleteUser
    (
        [FromRoute] Guid id,
        [FromServices] IUsersContext usersContext,
        [FromServices] IMapper mapper
    )
    {
        try
        {
            await usersContext.DeleteUserAsync(id);
            return TypedResults.Ok();
        }
        catch (UserNotFoundException e)
        {
            return TypedResults.NotFound(e.Message);
        }
    }
}