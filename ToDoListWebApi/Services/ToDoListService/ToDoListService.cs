using ToDoListWebApi.Infrastructure.Mapping;
using ToDoListWebApi.Persistence.Models;
using ToDoListWebApi.Persistence.Repositories;
using ToDoListWebApi.Services.ToDoListService.Models.Requests;
using ToDoListWebApi.Services.ToDoListService.Models.Responses;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Services.ToDoListService;

public class ToDoListService : IToDoListService
{
    private readonly IToDoListRepository _toDoListRepository;
    private readonly IModelMapper _modelMapper;
    private readonly ILogger _logger;

    public ToDoListService(IToDoListRepository toDoListRepository,
        IModelMapper modelMapper,
        ILogger logger)
    {
        _toDoListRepository = toDoListRepository;
        _modelMapper = modelMapper;
        _logger = logger;
    }

    public async Task<GetAllToDoTasksAsyncResponse> GetAllToDoTasksAsync()
    {
        var getAllToDoTasksAsyncResponse = new GetAllToDoTasksAsyncResponse();

        try
        {
            var result = await _toDoListRepository.GetAllToDoTasksAsync();

            if (!result.Error && result.ToDoTasks != null)
            {
                getAllToDoTasksAsyncResponse.ToDoTasks = _modelMapper.MapTo<ToDoTaskViewModel>(result.ToDoTasks);
                getAllToDoTasksAsyncResponse.SuccessMessage = result.SuccessMessage;

                _logger.LogInformation($"GetAllToDoTasksAsync service method was successfully executed at {DateTime.Now}. " +
                    $"Message: {result.SuccessMessage}");
            }
            else
            {
                getAllToDoTasksAsyncResponse.Error = result.Error;
                getAllToDoTasksAsyncResponse.ErrorMessage = result.ErrorMessage;

                _logger.LogError(result.ErrorMessage);
            }
        }
        catch (Exception exc)
        {
            getAllToDoTasksAsyncResponse.Error = true;
            string errorMessage = $"There was an error during GetAllToDoTasksAsync service method execution at {DateTime.Now}. " +
                $"Error message: {exc.Message}";
            getAllToDoTasksAsyncResponse.ErrorMessage = errorMessage;

            _logger.LogError(errorMessage);
        }

        return getAllToDoTasksAsyncResponse;
    }

    public async Task<GetToDoTaskByIdAsyncResponse> GetToDoTaskByIdAsync(GetToDoTaskByIdAsyncRequest request)
    {
        var getToDoTaskByIdAsyncResponse = new GetToDoTaskByIdAsyncResponse();

        try
        {
            var result = await _toDoListRepository.GetToDoTaskByIdAsync(request.TaskId);

            if (!result.Error && result.ToDoTask != null)
            {
                getToDoTaskByIdAsyncResponse.ToDoTaskViewModel = _modelMapper.MapTo<ToDoTaskViewModel>(result.ToDoTask);
                getToDoTaskByIdAsyncResponse.SuccessMessage = result.SuccessMessage;

                _logger.LogInformation($"GetToDoTaskByIdAsync service method was successfully executed at {DateTime.Now}. " +
                    $"Message: {result.SuccessMessage}");
            }
            else
            {
                getToDoTaskByIdAsyncResponse.Error = true;
                getToDoTaskByIdAsyncResponse.ErrorMessage = result.ErrorMessage;

                _logger.LogError(result.ErrorMessage);
            }
        }
        catch (Exception exc)
        {
            getToDoTaskByIdAsyncResponse.Error = true;
            string errorMessage = $"There was an error during GetToDoTaskByIdAsync service method execution at {DateTime.Now}. " +
                $"Error message: {exc.Message}";
            getToDoTaskByIdAsyncResponse.ErrorMessage = errorMessage;

            _logger.LogError(errorMessage);
        }

        return getToDoTaskByIdAsyncResponse;
    }

    public async Task<AddNewToDoTaskAsyncResponse> AddNewToDoTaskAsync(AddNewToDoTaskAsyncRequest request)
    {
        var addNewToDoTaskAsyncResponse = new AddNewToDoTaskAsyncResponse();

        try
        {
            if (request.ToDoTaskViewModel != null)
            {
                var itemToBeAdded = _modelMapper.MapTo<ToDoTask>(request.ToDoTaskViewModel);
                var result = await _toDoListRepository.AddNewToDoTaskAsync(itemToBeAdded);

                if (!result.Error && result.ToDoTask != null)
                {
                    addNewToDoTaskAsyncResponse.ToDoTaskViewModel = _modelMapper.MapTo<ToDoTaskViewModel>(result.ToDoTask);
                    addNewToDoTaskAsyncResponse.SuccessMessage = result.SuccessMessage;

                    _logger.LogInformation($"AddNewToDoTaskAsync service method was successfully executed at {DateTime.Now}. " +
                        $"Message: {result.SuccessMessage}");
                }
                else
                {
                    addNewToDoTaskAsyncResponse.Error = true;
                    addNewToDoTaskAsyncResponse.ErrorMessage = result.ErrorMessage;

                    _logger.LogError(result.ErrorMessage);
                }
            }
            else
            {
                addNewToDoTaskAsyncResponse.Error = true;
                string errorMessage = $"Reqest at {DateTime.Now} for AddNewToDoTaskAsync service method was null.";
                addNewToDoTaskAsyncResponse.ErrorMessage = errorMessage;

                _logger.LogError(errorMessage);
            }
        }
        catch (Exception exc)
        {
            addNewToDoTaskAsyncResponse.Error = true;
            string errorMessage = $"There was an error during AddNewToDoTaskAsync service method execution at {DateTime.Now}. " +
                $"Error message = {exc.Message}";
            addNewToDoTaskAsyncResponse.ErrorMessage = errorMessage;

            _logger.LogError(errorMessage);
        }

        return addNewToDoTaskAsyncResponse;
    }

    public async Task<DeleteToDoTaskAsyncResponse> DeleteToDoTaskAsync(DeleteToDoTaskAsyncRequest request)
    {
        var deleteToDoTaskAsyncResponse = new DeleteToDoTaskAsyncResponse();

        try
        {

            var result = await _toDoListRepository.DeleteToDoTaskAsync(request.TaskId);

            if (!result.Error)
            {
                deleteToDoTaskAsyncResponse.SuccessMessage = result.SuccessMessage;

                _logger.LogInformation($"DeleteToDoTaskAsync service method was successfully executed at {DateTime.Now}. " +
                       $"Message: {result.SuccessMessage}");
            }
            else
            {
                deleteToDoTaskAsyncResponse.Error = true;
                deleteToDoTaskAsyncResponse.ErrorMessage = result.ErrorMessage;

                _logger.LogError(result.ErrorMessage);
            }
        }
        catch (Exception exc)
        {
            deleteToDoTaskAsyncResponse.Error = true;
            string errorMessage = $"There was an error during DeleteToDoTaskAsync service method execution at {DateTime.Now}. " +
                $"Error message = {exc.Message}";
            deleteToDoTaskAsyncResponse.ErrorMessage = errorMessage;

            _logger.LogError(errorMessage);
        }

        return deleteToDoTaskAsyncResponse;
    }
}
