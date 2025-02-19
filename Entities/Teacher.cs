namespace Logbook.Entities;

public class Teacher : User
{
    public ICollection<GroupTeacher> GroupTeachers { get; set; } = null!;

    public Teacher()
    {
        Role = UserRole.Teacher;
    }
}