using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Services;
using ToDoListWebApi.Services.Models.Requests;
using ToDoListWebApi.Services.Models.Responses;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IToDoListService _toDoListService;

    public TasksController(IToDoListService toDoListService)
    {
        _toDoListService = toDoListService;
    }

    [HttpGet]
    public async Task<ActionResult<GetAllToDoTasksAsyncResponse>> GetAllToDoTasksAsync()
    {
        var result = await _toDoListService.GetAllToDoTasksAsync();

        if (!result.Error)
        {
            return result;
        }
        else
        {
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
            return result;
        }
        else
        {
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
            return result;
        }
        else
        {
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
            return result;
        }
        else
        {
            return BadRequest(result);
        }
    }
}
