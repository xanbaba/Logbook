namespace Logbook.Entities;

public class User
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string? FatherName { get; set; }
    public string Login { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public DateOnly? UtcBornAt { get; set; }
    public DateOnly? UtcLastSeenAt { get; set; }
    public string? Email { get; set; }
    public UserRole? Role { get; set; }
}

public enum UserRole
{
    Student,
    Admin,
    Teacher
}