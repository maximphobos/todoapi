namespace ToDoListWebApi.Persistence.Models.Responses;

public class AddNewToDoTaskAsyncResponse : BaseResponse
{
    public ToDoTask? ToDoTask { get; set; }
}
