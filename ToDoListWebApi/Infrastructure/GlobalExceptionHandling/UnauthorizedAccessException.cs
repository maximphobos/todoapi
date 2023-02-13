namespace ToDoListWebApi.Infrastructure.GlobalExceptionHandling;

public class UnauthorizedAccessException : Exception
{
    public UnauthorizedAccessException(string message) : base(message) { }
}
