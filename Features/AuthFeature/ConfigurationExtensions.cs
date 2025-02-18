namespace Logbook.Features.AuthFeature;

public static class ConfigurationExtensions
{
    private static T GetJwtOption<T>(IConfiguration configuration, string optionName)
    {
        var jwtOption = configuration.GetSection($"Jwt:{optionName}").Get<T>();
        if (jwtOption is null)
        {
            throw new ApplicationException(
                "Missing JWT options. Try specify JWT options in appsettings.json.\n" +
                "Path must be Jwt:<option>.\n" +
                "Options are [Secret, Issuer, Audience, LifetimeMinutes]");
        }

        return jwtOption;
    }

    public static string GetJwtSecret(this IConfiguration configuration)
    {
        return GetJwtOption<string>(configuration, "Secret");
    }
    
    public static string GetJwtAudience(this IConfiguration configuration)
    {
        return GetJwtOption<string>(configuration, "Audience");
    }
    
    public static string GetJwtIssuer(this IConfiguration configuration)
    {
        return GetJwtOption<string>(configuration, "Secret");
    }

    public static int GetLifetimeMinutes(this IConfiguration configuration)
    {
        return GetJwtOption<int>(configuration, "LifetimeMinutes");
    }
}