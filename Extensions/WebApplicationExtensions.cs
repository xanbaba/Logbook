using Logbook.Features;

namespace Logbook.Extensions;

public static class WebApplicationExtensions
{
    public static void AddFeature<TFeature>(this WebApplicationBuilder builder) where TFeature : IFeature
    {
        TFeature.Build(builder);
    }

    public static void UseFeature<TFeature>(this WebApplication app) where TFeature : IFeature
    {
        TFeature.Configure(app);
    }
}