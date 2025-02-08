using Microsoft.AspNetCore.Mvc;

namespace Logbook.Features.UsersManagement;

public abstract class AdminsManagementEndpointMapper : IEndpointMapper
{
    public static void Map(IEndpointRouteBuilder source)
    {
        // GET /admins?offset=10&count=20
        source.MapGet("/admins", GetAdmins);

        // GET /admins/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapGet("/admins/{id:guid}", GetAdmin);

        // POST /admins
        source.MapPost("/admins", CreateAdmin);

        // PUT /admins/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapPut("/admins/{id:guid}", UpdateAdmin);

        // DELETE /admins/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapDelete("/admins/{id:guid}", DeleteAdmin);
    }
    
    private static Task<IResult> GetAdmin(Guid id)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> GetAdmins(int offset = 0, int limit = 100)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> CreateAdmin([FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> UpdateAdmin(Guid id, [FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> DeleteAdmin(Guid id)
    {
        throw new NotImplementedException();
    }
}