using Microsoft.AspNetCore.Mvc;
using ToDoListWebApi.Services;
using ToDoListWebApi.Services.Models.Requests;
using ToDoListWebApi.ViewModels.ToDoListViewModels;

namespace ToDoListWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoListService _toDoListService;

        public ToDoListController(IToDoListService toDoListService)
        {
            _toDoListService = toDoListService;
        }

        [HttpGet]
        [Route("GetAllToDoTasksAsync")]
        public async Task<ActionResult> GetAllToDoTasksAsync()
        {
            var result = await _toDoListService.GetAllToDoTasksAsync();

            if (!result.Error)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("GetToDoTaskByIdAsync")]
        public async Task<ActionResult> GetToDoTaskByIdAsync(int taskId)
        {
            var result = await _toDoListService.GetToDoTaskByIdAsync(new GetToDoTaskByIdAsyncRequest()
            {
                TaskId = taskId
            });

            if (!result.Error)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost]
        [Route("AddNewToDoTaskAsync")]
        public async Task<ActionResult> AddNewToDoTaskAsync(ToDoTaskAddViewModel toDoTaskAddViewModel)
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
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete]
        [Route("DeleteToDoTaskAsync")]
        public async Task<ActionResult> DeleteToDoTaskAsync(int taskId)
        {
            var result = await _toDoListService.DeleteToDoTaskAsync(new DeleteToDoTaskAsyncRequest()
            {
                TaskId = taskId
            });

            if (!result.Error)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
