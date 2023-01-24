namespace ToDoListWebApi.Persistence.Models.Responses;

public class GetAllToDoTasksAsyncResponse : BaseResponse
{
    public List<ToDoTask>? ToDoTasks { get; set; }
}
