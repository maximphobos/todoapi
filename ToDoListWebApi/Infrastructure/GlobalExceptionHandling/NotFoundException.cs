namespace ToDoListWebApi.Infrastructure.GlobalExceptionHandling;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}
