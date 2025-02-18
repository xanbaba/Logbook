using Logbook.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Logbook.Features.AuthFeature;

public abstract class AuthFeature : IFeature
{
    public static void Build(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<JwtTokenGenerator>();
        builder.Services.AddScoped<IRefreshTokensManager, RefreshTokensManager>();

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = builder.Configuration.GetJwtIssuer(),

                ValidateAudience = true,
                ValidAudience = builder.Configuration.GetJwtAudience(),

                ValidateIssuerSigningKey = true,
                IssuerSigningKey =
                    new SymmetricSecurityKey(Constants.DefaultEncoding.GetBytes(builder.Configuration.GetJwtSecret())),

                ValidateLifetime = true
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy(UserRole.Admin.ToString(), policyBuilder =>
            {
                policyBuilder.RequireRole(UserRole.Admin.ToString());
            });
        });
    }

    public static void Configure(WebApplication app)
    {
        app.MapGroup("/api/v1").MapEndpoints<AuthEndpointMapper>();
        
        app.UseAuthentication();
        app.UseAuthorization();
    }
}