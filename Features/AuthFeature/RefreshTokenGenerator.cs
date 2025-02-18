using System.Security.Cryptography;

namespace Logbook.Features.AuthFeature;

public static class RefreshTokenGenerator
{
    public static string GenerateRefreshToken()
    {
        var refreshToken = Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
        return refreshToken;
    }
}