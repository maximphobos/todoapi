namespace ToDoListWebApi.Persistence.Models.Responses
{
    public class GetToDoTaskByIdAsyncResponse : BaseResponse
    {
        public ToDoTask? ToDoTask { get; set; }
    }
}
