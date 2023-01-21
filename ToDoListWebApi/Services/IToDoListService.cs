using ToDoListWebApi.Services.Models.Requests;
using ToDoListWebApi.Services.Models.Responses;

namespace ToDoListWebApi.Services
{
    public interface IToDoListService
    {
        Task<GetAllToDoTasksAsyncResponse> GetAllToDoTasksAsync();

        Task<GetToDoTaskByIdAsyncResponse> GetToDoTaskByIdAsync(GetToDoTaskByIdAsyncRequest request);

        Task<AddNewToDoTaskAsyncResponse> AddNewToDoTaskAsync(AddNewToDoTaskAsyncRequest request);

        Task<DeleteToDoTaskAsyncResponse> DeleteToDoTaskAsync(DeleteToDoTaskAsyncRequest request);
    }
}
