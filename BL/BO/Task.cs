using DO;
namespace BO
{
    /// <summary>
    /// Represents a BO task entity.
    /// </summary>
    public class Task
    {
        public int Id { get; init; } // Unique identifier for the task.
        public string? Description { get; set; } // Description of the task.
        public string? Alias { get; set; } // Alias for the task.
        public MilestoneInTask? Milestone { get; set; } // Represents the milestone associated with the task.
        public Status? Status { get; set; } // Represents the current status of the task.
        public DateTime CreateAt { get; set; } // Date when the task was created.
        public DateTime? BaselineStartDate { get; set; } // Baseline start date of the task.
        public DateTime? StartDate { get; set; } // Actual start date of the task.
        public DateTime? ForecastDate { get; set; } // Forecasted completion date of the task.
        public DateTime? DeadlineDate { get; set; } // Deadline for completing the task.
        public DateTime? CompleteDate { get; set; } // Date when the task was completed.
        public string? Deliverables { get; set; } // List of deliverables associated with the task.
        public string? Remarks { get; set; } // Any additional remarks or notes about the task.
        public EngineerInTask? Engineer { get; set; } // Engineer assigned to the task.
        public EngineerExperience? ComplexityLevel { get; set; } // Complexity level of the task.
        public List<TaskInList>? Dependencies { get; set; } // List of tasks that this task depends on.
        public bool IsActive { get; set; } // Indicates whether the task is active or not.

        /// <summary>
        /// Returns a string representation of the Task object.
        /// </summary>
        /// <returns>A string representing the Task object.</returns>
        public override string ToString() => this.ToStringProperty();
    }
}