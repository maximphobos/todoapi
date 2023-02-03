namespace ToDoListWebApi.Services.UserService.Requests;

public class PasswordSignInAsyncRequest : BaseUserRequest
{
    public string? UserPassword { get; set; }

    public bool RememberMe { get; set; }

    public bool LockoutOnFailure { get; set; }
}
