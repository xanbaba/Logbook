namespace Logbook;

public static class EndpointRouteBuilderExtensions
{
    public static void MapEndpoints<TEndpointMapper>(this IEndpointRouteBuilder endpointRouteBuilder)
        where TEndpointMapper : IEndpointMapper
    {
        TEndpointMapper.Map(endpointRouteBuilder);
    }
}