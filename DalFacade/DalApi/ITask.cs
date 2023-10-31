namespace DalApi;
//namespace DO;

public interface ITask
{
    int Create(Task item);
    Task? Read(int id);
    List<Task> ReadAll();

}
