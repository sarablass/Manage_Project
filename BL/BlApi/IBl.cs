namespace BlApi;
/// <summary>
/// 
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public ITask Task { get; }
}
