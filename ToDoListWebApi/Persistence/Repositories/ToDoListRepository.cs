using Microsoft.EntityFrameworkCore;
using ToDoListWebApi.Persistence.Contexts;
using ToDoListWebApi.Persistence.Models;
using ToDoListWebApi.Persistence.Models.Responses;

namespace ToDoListWebApi.Persistence.Repositories
{
    public class ToDoListRepository : IToDoListRepository
    {
        private readonly ToDoListContext _toDoListContext;

        public ToDoListRepository(ToDoListContext toDoListContext)
        {
            _toDoListContext = toDoListContext;
        }

        public async Task<GetAllToDoTasksAsyncResponse> GetAllToDoTasksAsync()
        {
            var getAllToDoTasksAsyncResponse = new GetAllToDoTasksAsyncResponse();

            try
            {
                getAllToDoTasksAsyncResponse.ToDoTasks = await _toDoListContext.ToDoTasks.ToListAsync();
            }
            catch (Exception exc)
            {
                getAllToDoTasksAsyncResponse.Error = true;
                getAllToDoTasksAsyncResponse.ErrorMessage = $"There was an error during GetAllToDoTasksAsync " +
                    $"method execution. Error message: {exc.Message}";
            }

            return getAllToDoTasksAsyncResponse;
        }

        public async Task<GetToDoTaskByIdAsyncResponse> GetToDoTaskByIdAsync(int taskId)
        {
            var getToDoTaskByIdAsyncResponse = new GetToDoTaskByIdAsyncResponse();

            try
            {
                var dbSet = _toDoListContext.ToDoTasks;

                if (dbSet != null)
                {
                    var result = await dbSet.FirstOrDefaultAsync(t => t.Id == taskId);

                    if (result != null)
                    {
                        getToDoTaskByIdAsyncResponse.ToDoTask = result;
                    }
                    else
                    {
                        getToDoTaskByIdAsyncResponse.Error = true;
                        getToDoTaskByIdAsyncResponse.ErrorMessage = $"TODO task with Id={taskId} not found " +
                            $"at {DateTime.Now}.";
                    }
                }
                else
                {
                    getToDoTaskByIdAsyncResponse.Error = true;
                    getToDoTaskByIdAsyncResponse.ErrorMessage = $"DbSet was null at {DateTime.Now}.";
                }
            }
            catch (Exception exc)
            {
                getToDoTaskByIdAsyncResponse.Error = true;
                getToDoTaskByIdAsyncResponse.ErrorMessage = $"There was an error during GetToDoTaskByIdAsync " +
                    $"method execution at {DateTime.Now}. Error message: {exc.Message}";
            }

            return getToDoTaskByIdAsyncResponse;
        }

        public async Task<AddNewToDoTaskAsyncResponse> AddNewToDoTaskAsync(ToDoTask toDoTask)
        {
            var addNewToDoTaskAsyncResponse = new AddNewToDoTaskAsyncResponse();

            try
            {
                await _toDoListContext.ToDoTasks.AddAsync(toDoTask);
                var addedTaskId = await _toDoListContext.SaveChangesAsync();

                var addedItem = await GetToDoTaskByIdAsync(addedTaskId);

                if (!addedItem.Error && addedItem.ToDoTask != null)
                {
                    addNewToDoTaskAsyncResponse.ToDoTask = addedItem.ToDoTask;
                }
                else
                {
                    addNewToDoTaskAsyncResponse.Error = true;
                    addNewToDoTaskAsyncResponse.ErrorMessage = $"New TODO task {toDoTask.TaskBodyText} was not " +
                        $"saved into the database because of some uncaught reason at {DateTime.Now}.";
                }
            }
            catch (Exception exc)
            {
                addNewToDoTaskAsyncResponse.Error = true;
                addNewToDoTaskAsyncResponse.ErrorMessage = $"There wan an error during AddNewToDoTaskAsync " +
                    $"method execution at {DateTime.Now}. Error message: {exc.Message}";
            }
            
            return addNewToDoTaskAsyncResponse;
        }

        public async Task<DeleteToDoTaskAsyncResponse> DeleteToDoTaskAsync(int taskId)
        {
            var deleteToDoTaskAsyncResponse = new DeleteToDoTaskAsyncResponse();

            try
            {
                var itemToBeDeleted = await GetToDoTaskByIdAsync(taskId);

                if (!itemToBeDeleted.Error && itemToBeDeleted.ToDoTask != null)
                {
                    _toDoListContext.ToDoTasks.Remove(itemToBeDeleted.ToDoTask);
                }
                else
                {
                    deleteToDoTaskAsyncResponse.Error = true;
                    deleteToDoTaskAsyncResponse.ErrorMessage = $"TODO task with Id={taskId} not found at {DateTime.Now}.";
                }
            }
            catch (Exception exc)
            {
                deleteToDoTaskAsyncResponse.Error = true;
                deleteToDoTaskAsyncResponse.ErrorMessage = $"There was an error during DeleteToDoTaskAsync method" +
                    $" at {DateTime.Now}. Error message: {exc.Message}";
            }

            return deleteToDoTaskAsyncResponse;
        }
    }
}
