namespace Logbook.Features.AuthFeature;

public abstract class AuthFeature : IFeature
{
    public static void Build(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<JwtTokenGenerator>();
        builder.Services.AddScoped<IRefreshTokensManager, RefreshTokensManager>();
    }

    public static void Configure(WebApplication app)
    {
        app.MapGroup("/api/v1").MapEndpoints<AuthEndpointMapper>();
    }
}