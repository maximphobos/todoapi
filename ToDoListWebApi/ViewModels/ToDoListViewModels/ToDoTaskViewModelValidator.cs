using FluentValidation;

namespace ToDoListWebApi.ViewModels.ToDoListViewModels
{
    public class ToDoTaskViewModelValidator : AbstractValidator<ToDoTaskViewModel>
    {
        public ToDoTaskViewModelValidator()
        {
            RuleFor(p => p.TaskBodyText).NotNull().NotEmpty();
        }
    }
}
