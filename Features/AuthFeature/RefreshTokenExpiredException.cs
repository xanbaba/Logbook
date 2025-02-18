namespace Logbook.Features.AuthFeature;

public class RefreshTokenExpiredException(string? message = null) : Exception(message);