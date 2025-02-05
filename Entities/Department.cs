namespace Logbook.Entities;

public class Department
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    
    public ICollection<Group> Groups { get; set; } = null!;
}