using System.Text;

namespace Logbook.Features.UsersManagement.Services;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        var bytes = Constants.DefaultEncoding.GetBytes(password);
        return Convert.ToBase64String(bytes);
    }
}