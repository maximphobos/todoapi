namespace ToDoListWebApi.ViewModels.ToDoListViewModels;

public class ToDoTaskViewModel
{
    public int Id { get; set; }

    public DateTime CreatedOn { get; set; }

    public string TaskBodyText { get; set; } = String.Empty;
}
