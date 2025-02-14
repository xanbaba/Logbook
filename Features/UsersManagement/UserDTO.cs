using Logbook.Entities;

namespace Logbook.Features.UsersManagement;

public record UserDTO
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public DateOnly? UtcBornAt { get; set; }
    public string? Email { get; set; }
    public string? Role { get; set; }
}