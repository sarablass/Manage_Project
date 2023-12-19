using DalApi;
using DO;
using System.Linq;

namespace Dal;

internal class TaskImplementation : ITask
{
    public int Create(DO.Task item)
    {
        int newId = Config.NextTaskId;
        DO.Task copyItem= item with { Id= newId };
        List<DO.Task> tasksList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        if (Read(copyItem.Id) is not null)
            throw new DalAlreadyExistsException($"Task with ID={item.Id} already exists");

        tasksList.Add(copyItem);
        XMLTools.SaveListToXMLSerializer<DO.Task>(tasksList, "tasks");
        return copyItem.Id;
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"Task with ID={id} cannot be deleted");
    }

    public DO.Task? Read(int id)
    {
        List<DO.Task> tasksList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
         DO.Task? task = tasksList.FirstOrDefault(x => x.Id == id);
        return task;
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        List<DO.Task> tasksList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        DO.Task? task = tasksList.FirstOrDefault(filter);
        return task;
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        List<DO.Task> tasksList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");

        if (filter is not null)
        {
            IEnumerable<DO.Task> tasks = tasksList.Where(filter);
            if (tasks.Any())
                return tasks;
            throw new DalDoesNotExistException($"There is no Task to read.");
        }
        else
        {
            if (tasksList.Any())
                return tasksList;
            throw new DalDoesNotExistException($"There is no Task to read.");
        }
    }

    public void Update(DO.Task item)
    {
        List<DO.Task> tasksList = XMLTools.LoadListFromXMLSerializer<DO.Task>("tasks");
        DO.Task? task = tasksList.FirstOrDefault(x => x.Id == item.Id);
        if (task is null)
            throw new DalDoesNotExistException($"Task with ID={item.Id} doesn't exist");
        tasksList.Remove(task);
        tasksList.Add(item);
        XMLTools.SaveListToXMLSerializer<DO.Task>(tasksList, "tasks");
    }
}
