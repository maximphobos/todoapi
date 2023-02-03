using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Services.ToDoListService.Models.Responses;

public class AddNewToDoTaskAsyncResponse : BaseResponse
{
    public ToDoTaskViewModel? ToDoTaskViewModel { get; set; }
}
