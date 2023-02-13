using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using ToDoListWebApi.Infrastructure.GlobalExceptionHandling;
using ToDoListWebApi.Services.ToDoListService;
using ToDoListWebApi.Services.ToDoListService.Models.Requests;
using ToDoListWebApi.Services.ToDoListService.Models.Responses;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IToDoListService _toDoListService;
    private readonly IStringLocalizer<TasksController> _localizer;

    public TasksController(IToDoListService toDoListService,
        IStringLocalizer<TasksController> localizer)
    {
        _toDoListService = toDoListService;
        _localizer = localizer;
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<GetAllToDoTasksAsyncResponse>> GetAllToDoTasksAsync()
    {
        var result = await _toDoListService.GetAllToDoTasksAsync();

        if (!result.Error)
        {
            result.SuccessMessage = "Successfully done";
            return result;
        }
        else
        {
            result.ErrorMessage = "Done with error";
            return BadRequest(result);
        }
    }

    [HttpGet("{taskId:int}")]
    public async Task<ActionResult<GetToDoTaskByIdAsyncResponse>> GetToDoTaskByIdAsync(int taskId)
    {
        var result = await _toDoListService.GetToDoTaskByIdAsync(new GetToDoTaskByIdAsyncRequest()
        {
            TaskId = taskId
        });

        if (!result.Error)
        {
            if (result.ToDoTaskViewModel != null)
            {
                result.SuccessMessage = "Successfully done";
                return result;
            }
            else
            {
                throw new NotFoundException(_localizer["TaskNotFoundById", taskId.ToString()]);
            }
        }
        else
        {
            result.ErrorMessage = "Done with error";
            return BadRequest(result);
        }
    }

    [HttpPost]
    public async Task<ActionResult<AddNewToDoTaskAsyncResponse>> AddNewToDoTaskAsync(ToDoTaskAddViewModel toDoTaskAddViewModel)
    {
        var result = await _toDoListService.AddNewToDoTaskAsync(new AddNewToDoTaskAsyncRequest()
        {
            ToDoTaskViewModel = new ToDoTaskViewModel()
            {
                CreatedOn = DateTime.Now,
                TaskBodyText = toDoTaskAddViewModel.TaskBodyText
            }
        });

        if (!result.Error)
        {
            result.SuccessMessage = "Successfully done";
            return result;
        }
        else
        {
            result.ErrorMessage = "Done with error";
            return BadRequest(result);
        }
    }

    [HttpDelete]
    public async Task<ActionResult<DeleteToDoTaskAsyncResponse>> DeleteToDoTaskAsync(int taskId)
    {
        var result = await _toDoListService.DeleteToDoTaskAsync(new DeleteToDoTaskAsyncRequest()
        {
            TaskId = taskId
        });

        if (!result.Error)
        {
            result.SuccessMessage = "Successfully done";
            return result;
        }
        else
        {
            result.ErrorMessage = "Done with error";
            return BadRequest(result);
        }
    }
}
