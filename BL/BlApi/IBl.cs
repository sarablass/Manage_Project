namespace BlApi;
/// <summary>
/// General interface of all entities.
/// </summary>
public interface IBl
{
    public IEngineer Engineer { get; }
    public IMilestone Milestone { get; }
    public ITask Task { get; }
}
