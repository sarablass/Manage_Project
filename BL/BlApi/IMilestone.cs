namespace BlApi;
/// <summary>
/// A milestone interface with actions: adding a milestone, reading a milestone and updating a milestone.
/// </summary>
public interface IMilestone
{
    public int Create();
    public BO.Milestone? Read(int id);
    public void Update(BO.Milestone item);
}
