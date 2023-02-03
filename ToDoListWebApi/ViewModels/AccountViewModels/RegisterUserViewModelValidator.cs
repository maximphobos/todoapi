using FluentValidation;

namespace ToDoListWebApi.ViewModels.AccountViewModels;

public class RegisterUserViewModelValidator : AbstractValidator<RegisterUserViewModel>
{
    public RegisterUserViewModelValidator()
    {
        RuleFor(x => x.UserName).NotNull().MaximumLength(50);
        RuleFor(x => x.UserEmail).EmailAddress().NotNull();
        RuleFor(x => x.UserPassword).NotNull();
        RuleFor(x => x.UserConfirmPassword).NotNull().Equal(x => x.UserPassword);
    }
}
