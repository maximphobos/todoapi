using ToDoListWebApi.Persistence.Models;
using ToDoListWebApi.Persistence.Models.Responses;

namespace ToDoListWebApi.Persistence.Repositories;

public interface IToDoListRepository
{
    Task<GetAllToDoTasksAsyncResponse> GetAllToDoTasksAsync();

    Task<GetToDoTaskByIdAsyncResponse> GetToDoTaskByIdAsync(int taskId);

    Task<AddNewToDoTaskAsyncResponse> AddNewToDoTaskAsync(ToDoTask toDoTask);

    Task<DeleteToDoTaskAsyncResponse> DeleteToDoTaskAsync(int taskId);
}
