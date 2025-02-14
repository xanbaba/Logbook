namespace Logbook.Features.UsersManagement.Exceptions;

public class UniqueConstraintException(string? message = null) : Exception(message);
