using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Services.ToDoListService.Models.Requests;

public class AddNewToDoTaskAsyncRequest
{
    public ToDoTaskViewModel? ToDoTaskViewModel { get; set; }
}
