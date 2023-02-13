namespace ToDoListWebApi.Infrastructure.GlobalExceptionHandling;

public class NotImplementedException : Exception
{
    public NotImplementedException(string message) : base(message) { }
}
