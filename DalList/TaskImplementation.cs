
namespace DalList;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the task entity.
public class TaskImplementation : ITask
{
    //A method that generates a new task.
    public int Create(Task item) 
    {
        //for entities with auto id
        int id = DataSource.Config.NextTaskId; //Inserting the value of the next running number into the Id variable. 
        Task copy = item with { Id = id }; //Creating a copy of the new object that arrived as a parameter and updating the id with the new runner number.
        DataSource.Tasks.Add(copy); //Adding the reference of the copy to the list of objects of type task.
        return id; //Returning the new id of the newly added object to the list.
    }

    public Task? Read(int id)
    {
        if (DataSource.Tasks.Find(x => x.Id == id) == null)
            return null;
       // return Tasks.
           

    }

    public List<Task> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Task item)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }
}