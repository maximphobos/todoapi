using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Services.Models.Responses;

public class GetToDoTaskByIdAsyncResponse : BaseResponse
{
    public ToDoTaskViewModel? ToDoTaskViewModel { get; set; }
}
