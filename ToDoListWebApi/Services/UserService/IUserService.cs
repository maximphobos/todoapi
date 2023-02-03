using Microsoft.AspNetCore.Identity;
using ToDoListWebApi.Services.UserService.Requests;
using ToDoListWebApi.ViewModels.AccountViewModels;

namespace ToDoListWebApi.Services.UserService;

public interface IUserService
{
    Task<SignInResult> PasswordSignInAsync(PasswordSignInAsyncRequest request);

    Task<UserViewModel> GetUserByName(BaseUserRequest request);

    Task<IdentityResult> CreateUserAsync(CreateUserAsyncRequest request);
}
