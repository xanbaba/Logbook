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

        // POST /users
        source.MapPost("/users", CreateUser);

        // PUT /users/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapPut("/users/{id:guid}", UpdateUser);

        // DELETE /users/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapDelete("/users/{id:guid}", DeleteUser);
    }

    private static Task<IResult> GetUser(Guid id)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> GetUsers(int offset = 0, int limit = 100)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> CreateUser([FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> UpdateUser(Guid id, [FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> DeleteUser(Guid id)
    {
        throw new NotImplementedException();
    }
}