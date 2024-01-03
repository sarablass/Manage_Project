
using DO;

namespace BO;
/// <summary>
/// 
/// </summary>
public class Task
{
    public int Id { get; init; }//
    public string Description { get; set; }//
    public string Alias { get; set; }//
    public MilestoneInTask? Milestone { get; set; }//
    public Status? Status { get; set; }//
    public DateTime CreateAt { get; set; }//
    public DateTime? BaselineStartDate { get; set; }//
    public DateTime? StartDate { get; set; }//
    public DateTime? ForecastDate { get; set; }//
    public DateTime? DeadlineDate { get; set; }//
    public DateTime? CompleteDate { get; set; }//
    public string? Deliverables { get; set; }//   
    public string? Remarks { get; set; }//
    public EngineerInTask? Engineer { get; set; }
    public EngineerExperience? ComplexityLevel { get; set; }// 
    public List<TaskInList>? Dependencies { get; set; }
    public bool IsActive { get; set; }

    public override string ToString() => this.ToStringProperty();
}
