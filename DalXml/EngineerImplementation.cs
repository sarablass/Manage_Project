using DalApi;
using DO;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        if (Read(item.Id) is not null)
            throw new DalAlreadyExistsException($"Engineer with ID={item.Id} already exists");
             
         engineersList.Add(item);
         XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
         return item.Id;   
    }

    public void Delete(int id)
    {
        throw new DalDeletionImpossible($"Engineer with ID={id} cannot be deleted");
    }

    public Engineer? Read(int id)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineersList.FirstOrDefault(x => x.Id == id);
        return engineer;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineersList.FirstOrDefault(filter);
        return engineer;
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");

        if (filter is not null)
        {
            IEnumerable<Engineer> engineers = engineersList.Where(filter);
            if (engineers.Any())
                return engineers;
            throw new DalDoesNotExistException($"There is no Engineers to read.");
        }
        else
        {
            if (engineersList.Any())
                return engineersList;
            throw new DalDoesNotExistException($"There is no Engineers to read.");
        }
    }

    public void Reset()
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        engineersList.Clear();
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
    }

    public void Update(Engineer item)
    {
        List<Engineer> engineersList = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineersList.FirstOrDefault(x => x.Id == item.Id);
        if (engineer is null)
            throw new DalDoesNotExistException($"Engineer with ID={item.Id} doesn't exist"); 
        engineersList.Remove(engineer);
        engineersList.Add(item);
        XMLTools.SaveListToXMLSerializer<Engineer>(engineersList, "engineers");
    }
}
