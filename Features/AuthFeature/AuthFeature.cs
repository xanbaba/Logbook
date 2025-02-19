using Logbook.DataAccess;
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
        app.UseAuthentication();
        app.UseAuthorization();
        
        var group = app.MapGroup("/api/v1");
        group.MapEndpoints<AuthEndpointMapper>();

        using var serviceScope = app.Services.CreateScope();
        using var appDbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (!appDbContext.Admins.Any())
        {
            var defaultAdminLogin = app.Configuration["DefaultAdmin:Login"];
            var defaultAdminPassword = app.Configuration["DefaultAdmin:Password"];

            if (string.IsNullOrEmpty(defaultAdminLogin) || string.IsNullOrEmpty(defaultAdminPassword))
            {
                throw new ApplicationException("Default admin login or password are required. Try setting 'DefaultAdmin:Login' and 'DefaultAdmin:Password' in appsettings.json.");
            }
            
            appDbContext.Users.Add(new Admin
            {
                FirstName = "DefaultAdmin",
                LastName = "DefaultAdmin",
                Login = defaultAdminLogin,
                PasswordHash = PasswordHasher.HashPassword(defaultAdminPassword),
                Id = Guid.CreateVersion7()
            });
        }
    }
}