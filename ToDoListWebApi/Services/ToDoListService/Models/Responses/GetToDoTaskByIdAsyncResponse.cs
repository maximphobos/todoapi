using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Services.ToDoListService.Models.Responses;

public class GetToDoTaskByIdAsyncResponse : BaseResponse
{
    public ToDoTaskViewModel? ToDoTaskViewModel { get; set; }
}
