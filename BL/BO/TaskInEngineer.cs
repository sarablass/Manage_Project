namespace BO;

public class TaskInEngineer // Declaring a class to represent a task assigned to an engineer
{
    public int Id { get; init; } //ID of the task
    public string? Alias { get; set; } //alias of the task (nullable)
    public override string ToString() => this.ToStringProperty(); //Method to override the ToString() method and return a string representation of the task
}
