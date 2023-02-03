namespace ToDoListWebApi.Services.UserService.Requests;

public class CreateUserAsyncRequest : BaseUserRequest
{
    public string? UserEmail { get; set; }

    public string? UserPassword { get; set; }
}
