namespace Logbook.Entities;

public class Group
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; } = null!;

    public ICollection<GroupTeacher> GroupTeachers { get; set; } = null!;
}