
namespace BlApi;
/// <summary>
/// A task interface with actions: adding a task, reading a task, reading tasks, updating a task and deleting a task.
/// </summary>
public interface ITask
{
    public int Create(BO.Task item);
    public BO.Task? Read(int id);
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null);
    public void Update(BO.Task item);
    public void Delete(int id);
}
