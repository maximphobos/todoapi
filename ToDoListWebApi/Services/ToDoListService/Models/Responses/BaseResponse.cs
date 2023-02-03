namespace ToDoListWebApi.Services.ToDoListService.Models.Responses;

public class BaseResponse
{
    public bool Error { get; set; }

    public string ErrorMessage { get; set; } = string.Empty;

    public string SuccessMessage { get; set; } = string.Empty;
}
