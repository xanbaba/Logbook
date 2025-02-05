namespace Logbook.EndpointMappers;

public interface IEndpointMapper
{
    public static abstract void Map(IEndpointRouteBuilder source);
}

public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints<TEndpointMapper>(this IEndpointRouteBuilder endpointRouteBuilder)
        where TEndpointMapper : IEndpointMapper
    {
        TEndpointMapper.Map(endpointRouteBuilder);
    }
}