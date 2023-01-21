namespace ToDoListWebApi.Persistence.Models
{
    public class ToDoTask
    {
        public int Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public string? TaskBodyText { get; set; }
    }
}
