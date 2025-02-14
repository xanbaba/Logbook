namespace Logbook.Features;

public interface IFeature
{
    public static abstract void Build(WebApplicationBuilder builder);
    public static abstract void Configure(WebApplication app);
}