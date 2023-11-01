namespace DO;

/// <summary>
/// Dependency entity represents a dependency with all its props.
/// </summary>
/// <param name="Id">Unique dependency identifier, automatic runner number</param>
/// <param name="DependentTask">ID number of pending task</param>
/// <param name="DependsOnTask">ID number of previous task</param>
public record Dependency
(
    int Id,
    int DependentTask,
    int DependsOnTask
);
