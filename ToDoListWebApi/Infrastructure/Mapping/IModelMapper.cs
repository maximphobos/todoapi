using ToDoListWebApi.Persistence.Models;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Infrastructure.Mapping;

public interface IModelMapper
{
    T MapTo<T>(ToDoTaskViewModel model);

    T MapTo<T>(ToDoTask model);

    List<T> MapTo<T>(IList<ToDoTask> model);
}
