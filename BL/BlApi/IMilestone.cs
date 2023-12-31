namespace BlApi;

public interface IMilestone
{
    public int Create();
    public BO.Milestone? Read(int id);
    public void Update(BO.Milestone item);
}
