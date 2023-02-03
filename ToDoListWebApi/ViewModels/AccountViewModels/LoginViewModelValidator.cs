using FluentValidation;

namespace ToDoListWebApi.ViewModels.AccountViewModels;

public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
{
    public LoginViewModelValidator()
    {
        RuleFor(x => x.UserName).NotNull().NotEmpty();
        RuleFor(x => x.UserPassword).NotNull().NotEmpty();
    }
}
