namespace ToDoListWebApi.ViewModels.AccountViewModels;

public class UserViewModel
{
    public UserViewModel()
    {
        UserRoles = new List<string>();
    }

    public string? Id { get; set; }

    public string? UserName { get; set; }

    public string? UserEmail { get; set; }

    public IList<string> UserRoles { get; set; }

    public string? UserPassword { get; set; }
}
