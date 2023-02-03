using ToDoListWebApi.Services.ToDoListService.Models.Requests;
using ToDoListWebApi.Services.ToDoListService.Models.Responses;

namespace ToDoListWebApi.Services.ToDoListService;

public interface IToDoListService
{
    Task<GetAllToDoTasksAsyncResponse> GetAllToDoTasksAsync();

    Task<GetToDoTaskByIdAsyncResponse> GetToDoTaskByIdAsync(GetToDoTaskByIdAsyncRequest request);

    Task<AddNewToDoTaskAsyncResponse> AddNewToDoTaskAsync(AddNewToDoTaskAsyncRequest request);

    Task<DeleteToDoTaskAsyncResponse> DeleteToDoTaskAsync(DeleteToDoTaskAsyncRequest request);
}
