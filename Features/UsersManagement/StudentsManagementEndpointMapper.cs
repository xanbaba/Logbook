using Microsoft.AspNetCore.Mvc;

namespace Logbook.Features.UsersManagement;

public abstract class StudentsManagementEndpointMapper : IEndpointMapper
{
    public static void Map(IEndpointRouteBuilder source)
    {
        // GET /students?offset=10&count=20
        source.MapGet("/students", GetStudents);

        // GET /students/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapGet("/students/{id:guid}", GetStudent);

        // POST /students
        source.MapPost("/students", CreateStudent);

        // PUT /students/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapPut("/students/{id:guid}", UpdateStudent);

        // DELETE /students/0194e5af-ae13-714b-a974-1acee1f19cd3
        source.MapDelete("/students/{id:guid}", DeleteStudent);
    }
    
    private static Task<IResult> GetStudent(Guid id)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> GetStudents(int offset = 0, int count = 100)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> CreateStudent([FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> UpdateStudent(Guid id, [FromBody] UserDTO? dto)
    {
        throw new NotImplementedException();
    }

    private static Task<IResult> DeleteStudent(Guid id)
    {
        throw new NotImplementedException();
    }
}