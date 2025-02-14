namespace Logbook.Features.UsersManagement.Exceptions;

public class UserNotFoundException(string? message = null) : Exception(message);