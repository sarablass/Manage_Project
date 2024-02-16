namespace BO;

public class TaskInList // Declaring a class to represent a task in a list
{
    public int Id { get; init; } //ID of the task
    public string? Description { get; set; } //description of the task (nullable)
    public string? Alias { get; set; } //alias of the task (nullable)
    public Status? Status { get; set; } //status of the task (nullable)
    public override string ToString() => this.ToStringProperty(); //Method to override the ToString() method and return a string representation of the task

}
