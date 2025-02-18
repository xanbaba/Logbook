using System.Diagnostics.CodeAnalysis;
using Logbook.Features.UsersManagement.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Logbook.Features.AuthFeature;

public abstract class AuthEndpointMapper : IEndpointMapper
{
    public static void Map(IEndpointRouteBuilder source)
    {
        source.MapPost("/login", Login);

        source.MapPost("/refresh", Refresh);
    }

    [SuppressMessage("ReSharper", "NotAccessedPositionalProperty.Local")]
    private record TokensResponse(string AccessToken, string RefreshToken);


    private record LoginRequest(string Login, string Password);

    private static async Task<Results<UnauthorizedHttpResult, Ok<TokensResponse>>> Login
    (
        [FromBody] LoginRequest request,
        [FromServices] IUsersContext usersContext,
        [FromServices] IConfiguration configuration,
        [FromServices] JwtTokenGenerator accessTokenGenerator,
        [FromServices] IRefreshTokensManager refreshTokensManager
    )
    {
        var user = await usersContext.GetUserByLoginAsync(request.Login);
        if (user is null)
        {
            return TypedResults.Unauthorized();
        }

        var hashedPassword = PasswordHasher.HashPassword(request.Password);

        if (user.PasswordHash != hashedPassword)
        {
            return TypedResults.Unauthorized();
        }


        var accessToken = accessTokenGenerator.GenerateJwtToken(user);
        var refreshToken = await refreshTokensManager.AssignRefreshTokenAsync(user);
        return TypedResults.Ok(new TokensResponse(accessToken, refreshToken));
    }


    private record RefreshRequest(string RefreshToken);
    private static async Task<Results<Ok<TokensResponse>, UnauthorizedHttpResult>> Refresh
    (
        [FromBody] RefreshRequest request,
        [FromServices] IRefreshTokensManager refreshTokensManager,
        [FromServices] JwtTokenGenerator accessTokenGenerator
    )
    {
        try
        {
            var newRefreshToken = await refreshTokensManager.RefreshTokenAsync(request.RefreshToken);
            var newAccessToken = accessTokenGenerator.GenerateJwtToken(newRefreshToken);

            return TypedResults.Ok(new TokensResponse(newAccessToken, newRefreshToken));
        }
        catch (Exception e) when(e is RefreshTokenNotFoundException or RefreshTokenExpiredException)
        {
            return TypedResults.Unauthorized();
        }
    }
}