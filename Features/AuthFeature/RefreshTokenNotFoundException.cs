namespace Logbook.Features.AuthFeature;

public class RefreshTokenNotFoundException(string? message = null) : Exception(message);