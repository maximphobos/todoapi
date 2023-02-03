using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Services.ToDoListService.Models.Responses;

public class GetAllToDoTasksAsyncResponse : BaseResponse
{
    public List<ToDoTaskViewModel>? ToDoTasks { get; set; }
}
