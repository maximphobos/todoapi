namespace ToDoListWebApi.Persistence.Models.Responses;

public class BaseResponse
{
    public bool Error { get; set; }

    public string ErrorMessage { get; set; } = String.Empty;

    public string SuccessMessage { get; set; } = String.Empty;
}
