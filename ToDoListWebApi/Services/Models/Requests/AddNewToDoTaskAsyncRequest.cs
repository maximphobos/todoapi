using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Services.Models.Requests
{
    public class AddNewToDoTaskAsyncRequest
    {
        public ToDoTaskViewModel? ToDoTaskViewModel { get; set; }
    }
}
