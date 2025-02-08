using Microsoft.AspNetCore.Mvc;

namespace Logbook.Features.UsersManagement;

public abstract class TeachersManagementEndpointMapper : IEndpointMapper
{
    public static void Map(IEndpointRouteBuilder source)
    {
        // GET /teachers?offset=10&count=20
        source.MapGet("/teachers", GetTeachers);

        // GET /teachers/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapGet("/teachers/{id:guid}", GetTeacher);

        // POST /teachers
        source.MapPost("/teachers", CreateTeacher);

        // PUT /teachers/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapPut("/teachers/{id:guid}", UpdateTeacher);

        // DELETE /teachers/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapDelete("/teachers/{id:guid}", DeleteTeacher);
    }
    
    private static Task<IResult> GetTeacher(Guid id)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> GetTeachers(int offset = 0, int limit = 100)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> CreateTeacher([FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> UpdateTeacher(Guid id, [FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> DeleteTeacher(Guid id)
    {
        throw new NotImplementedException();
    }
}