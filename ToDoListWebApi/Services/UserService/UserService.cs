using Microsoft.AspNetCore.Identity;
using ToDoListWebApi.Infrastructure.Identity;
using ToDoListWebApi.Persistence.Models.Identity;
using ToDoListWebApi.Services.UserService.Requests;
using ToDoListWebApi.ViewModels.AccountViewModels;

namespace ToDoListWebApi.Services.UserService;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<SignInResult> PasswordSignInAsync(PasswordSignInAsyncRequest request)
    {
        var result = await _signInManager.PasswordSignInAsync(request.UserName!, request.UserPassword!,
            request.RememberMe!, request.LockoutOnFailure!);

        return result;
    }

    public async Task<UserViewModel> GetUserByName(BaseUserRequest request)
    {
        var identityUser = await _userManager.FindByNameAsync(request.UserName!);
        var roles = await _userManager.GetRolesAsync(identityUser!);

        return new UserViewModel()
        {
            UserEmail = identityUser?.Email!,
            Id = identityUser?.Id,
            UserName = identityUser?.UserName,
            UserRoles = roles
        };
    }

    public async Task<IdentityResult> CreateUserAsync(CreateUserAsyncRequest request)
    {
        var user = new ApplicationUser { UserName = request.UserName, Email = request.UserEmail };
        var result = await _userManager.CreateAsync(user, request.UserPassword!);
        await _userManager.AddToRoleAsync(user, UserRoles.WebApi);

        return result;
    }
}

