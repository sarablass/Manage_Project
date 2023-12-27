
namespace DalApi;

public interface IDal
{
    ITask Task { get; }
    IEngineer Engineer { get; }
    IDependency Dependency { get; }
    public void Reset();

}
