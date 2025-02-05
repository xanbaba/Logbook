namespace Logbook.Entities;

public class GroupTeacher
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; } = null!;
    
    public Guid TeacherId { get; set; }
    public Teacher Teacher { get; set; } = null!;
}