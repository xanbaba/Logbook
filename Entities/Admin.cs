namespace Logbook.Entities;

public class Admin : User
{
    public Admin()
    {
        Role = UserRole.Admin;
    }
}