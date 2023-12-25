namespace DO;

/// <summary>
/// Task entity represents a task with all its props.
/// </summary>
/// <param name="Id">Unique task identifier, automatic runner number</param>
/// <param name="Description">Description of the task</param>
/// <param name="Alias">Short name of a task</param>
/// <param name="Milestone">                </param>
/// <param name="CreatedAt">Task creation date</param>
/// <param name="Start">Task start date</param>
/// <param name="ScheduledDate">Estimated date for completion of the task</param>
/// <param name="Deadline">Last date for completing the task</param>
/// <param name="Complete">Actual assignment completion date</param>
/// <param name="Deliverables">Deliverables of the task</param>
/// <param name="Remarks">Remarks for the task</param>
/// <param name="EngineerId">The engineer ID assigned to the task</param>
/// <param name="ComplexityLevel">Copmlexity level of the task</param>
public record Task
(
    int Id,
    string Description,
    string Alias,
    bool Milestone,
    DateTime? CreatedAt,
    DateTime? Start,
    DateTime? ScheduledDate,
    DateTime? Deadline,
    DateTime? Complete,
    string? Deliverables,
    string? Remarks,
    int EngineerId,
    EngineerExperience? ComplexityLevel=EngineerExperience.AdvancedBeginner,
    bool Active=true
)
{
    public Task() : this(0, "", "", false, DateTime.Now, null, null, null, null, "", "", 0, new EngineerExperience(), true)
    {
       
    }
}
