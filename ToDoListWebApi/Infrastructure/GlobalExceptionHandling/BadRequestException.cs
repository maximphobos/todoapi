namespace ToDoListWebApi.Infrastructure.GlobalExceptionHandling;

public class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}
