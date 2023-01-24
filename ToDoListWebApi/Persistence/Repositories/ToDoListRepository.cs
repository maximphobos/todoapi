using Microsoft.EntityFrameworkCore;
using ToDoListWebApi.Persistence.Contexts;
using ToDoListWebApi.Persistence.Models;
using ToDoListWebApi.Persistence.Models.Responses;

namespace ToDoListWebApi.Persistence.Repositories;

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

            getAllToDoTasksAsyncResponse.SuccessMessage = $"Received {getAllToDoTasksAsyncResponse.ToDoTasks.Count} records.";
        }
        catch (Exception exc)
        {
            getAllToDoTasksAsyncResponse.Error = true;
            getAllToDoTasksAsyncResponse.ErrorMessage = $"There was an error during GetAllToDoTasksAsync repository " +
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
                    getToDoTaskByIdAsyncResponse.SuccessMessage = $"TODO task with Id={result.Id} was successfully found.";
                }
                else
                {
                    getToDoTaskByIdAsyncResponse.Error = true;
                    getToDoTaskByIdAsyncResponse.ErrorMessage = $"TODO task with Id={taskId} was not found.";
                }
            }
            else
            {
                getToDoTaskByIdAsyncResponse.Error = true;
                getToDoTaskByIdAsyncResponse.ErrorMessage = $"DbSet was null.";
            }
        }
        catch (Exception exc)
        {
            getToDoTaskByIdAsyncResponse.Error = true;
            getToDoTaskByIdAsyncResponse.ErrorMessage = $"There was an error during GetToDoTaskByIdAsync repository " +
                $"method execution. Error message: {exc.Message}";
        }

        return getToDoTaskByIdAsyncResponse;
    }

    public async Task<AddNewToDoTaskAsyncResponse> AddNewToDoTaskAsync(ToDoTask toDoTask)
    {
        var addNewToDoTaskAsyncResponse = new AddNewToDoTaskAsyncResponse();

        try
        {
            await _toDoListContext.ToDoTasks.AddAsync(toDoTask);
            await _toDoListContext.SaveChangesAsync();

            var addedItem = await _toDoListContext.ToDoTasks.FirstAsync(t => t.CreatedOn == toDoTask.CreatedOn
                && t.TaskBodyText == toDoTask.TaskBodyText);

            if (addedItem != null)
            {
                addNewToDoTaskAsyncResponse.ToDoTask = addedItem;
                addNewToDoTaskAsyncResponse.SuccessMessage = $"New TODO task was successfully added with Id={addedItem.Id}.";
            }
            else
            {
                addNewToDoTaskAsyncResponse.Error = true;
                addNewToDoTaskAsyncResponse.ErrorMessage = $"New TODO task {toDoTask.TaskBodyText} was not " +
                    $"saved into the database because of some uncaught reason.";
            }
        }
        catch (Exception exc)
        {
            addNewToDoTaskAsyncResponse.Error = true;
            addNewToDoTaskAsyncResponse.ErrorMessage = $"There wan an error during AddNewToDoTaskAsync repository " +
                $"method execution. Error message: {exc.Message}";
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
                await _toDoListContext.SaveChangesAsync();

                //Check if item successfully removed
                var deletedItem = await GetToDoTaskByIdAsync(itemToBeDeleted.ToDoTask.Id);

                if (!deletedItem.Error && deletedItem.ToDoTask != null)
                {
                    deleteToDoTaskAsyncResponse.Error = true;
                    deleteToDoTaskAsyncResponse.ErrorMessage = $"TODO task with Id={deletedItem.ToDoTask?.Id} was not deleted " +
                        $"for the some uncaught reason.";
                }
                else
                {
                    deleteToDoTaskAsyncResponse.SuccessMessage = $"TODO task with Id={taskId} was deleted successfully.";
                }
            }
            else
            {
                deleteToDoTaskAsyncResponse.Error = true;
                deleteToDoTaskAsyncResponse.ErrorMessage = $"TODO task with Id={taskId} was not found.";
            }
        }
        catch (Exception exc)
        {
            deleteToDoTaskAsyncResponse.Error = true;
            deleteToDoTaskAsyncResponse.ErrorMessage = $"There was an error during DeleteToDoTaskAsync repository method execution. " +
                $"Error message: {exc.Message}";
        }

        return deleteToDoTaskAsyncResponse;
    }
}
