namespace Logbook;

public interface IEndpointMapper
{
    public static abstract void Map(IEndpointRouteBuilder source);
}