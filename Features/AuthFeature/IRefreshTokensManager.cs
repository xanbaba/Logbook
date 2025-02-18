using Logbook.Entities;

namespace Logbook.Features.AuthFeature;

public interface IRefreshTokensManager
{
    public Task<string> RefreshTokenAsync(string refreshToken);
    public Task<string> AssignRefreshTokenAsync(User user);
}