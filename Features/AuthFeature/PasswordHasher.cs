using System.Security.Cryptography;

namespace Logbook.Features.AuthFeature;

public static class PasswordHasher
{
    public static string HashPassword(string password)
    {
        var passwordBytes = Constants.DefaultEncoding.GetBytes(password);
        var hashedPasswordBytes = SHA256.HashData(passwordBytes);
        var hashedPassword = Convert.ToBase64String(hashedPasswordBytes);
        return hashedPassword;
    }
}