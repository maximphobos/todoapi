using FluentValidation;

namespace ToDoListWebApi.ViewModels.ToDoListViewModels
{
    public class ToDoTaskAddViewModelValidator : AbstractValidator<ToDoTaskAddViewModel>
    {
        public ToDoTaskAddViewModelValidator()
        {
            RuleFor(p => p.TaskBodyText).NotNull().NotEmpty();
        }
    }
}
