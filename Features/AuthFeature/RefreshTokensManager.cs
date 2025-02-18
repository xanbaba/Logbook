using Logbook.DataAccess;
using Logbook.Entities;

namespace Logbook.Features.AuthFeature;

public class RefreshTokensManager(AppDbContext dbContext) : IRefreshTokensManager
{
    private const int RefreshTokenLifetimeDays = 30;
    
    public async Task<string> RefreshTokenAsync(string refreshToken)
    {
        var existingRefreshToken = dbContext.RefreshTokens.FirstOrDefault(t => t.Token == refreshToken);
        if (existingRefreshToken == null)
        {
            throw new RefreshTokenNotFoundException();
        }

        if (existingRefreshToken.UtcExpiresAt < DateTime.UtcNow)
        {
            throw new RefreshTokenExpiredException();
        }

        var newRefreshToken = RefreshTokenGenerator.GenerateRefreshToken();
        
        existingRefreshToken.Token = newRefreshToken;
        existingRefreshToken.UtcExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenLifetimeDays);
        await dbContext.SaveChangesAsync();
        return newRefreshToken;
    }

    public async Task<string> AssignRefreshTokenAsync(User user)
    {
        var token = RefreshTokenGenerator.GenerateRefreshToken();
        dbContext.RefreshTokens.Add(new RefreshToken
        {
            UserId = user.Id,
            UtcExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenLifetimeDays),
            Token = token
        });
        
        await dbContext.SaveChangesAsync();

        return token;
    }
}