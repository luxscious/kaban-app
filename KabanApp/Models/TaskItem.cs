namespace KabanApp.Models;

public class TaskItem
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TaskStatus Status { get; set; } = TaskStatus.Backlog;
    public required DateTime CreatedDate { get; set; }
}


