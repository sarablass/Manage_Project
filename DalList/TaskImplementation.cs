namespace DalList;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the task entity.
internal class TaskImplementation : ITask
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

    //A method that requests/receives a single object.
    public Task? Read(int id)
    {
       return DataSource.Tasks.FirstOrDefault(x => x.Id == id);
    }

    public Task? Read(Func<Task, bool> filter)
    { 
        return DataSource.Tasks.FirstOrDefault(x=>filter(x));
    }

    //A method that requests/receives all objects of a certain type.
    public IEnumerable<Task> ReadAll(Func<Task, bool>? filter = null) 
    {
        if (filter != null)
        {
            return from item in DataSource.Tasks
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Tasks
               select item;
    }

    //A method that updates an existing object.
    public void Update(Task item)
    {
        Task? removeTask = Read(item.Id)!;
        if (Read(item.Id) is null) //Checking if there is an object with the same ID number, in the list.
            throw new DalDoesNotExistException($"Task with ID={item.Id} doesn't exist"); //A suitable exception throw.

        DataSource.Tasks.Remove(removeTask); //Removes the reference to an existing object from a list.
        DataSource.Tasks.Add(item); //Added to the list the reference to the updated object received as a parameter.
    }

    //A method that deletes an existing object.
    public void Delete(int id)
    {
        if((DataSource.Dependencies.FirstOrDefault(x => x.DependentTask == id)) is not null) //Checks if the entity is an entity that should not be deleted - checks if there is a dependency to the task.
            throw new DalDeletionImpossible($"Task with ID={id} cannot be deleted"); //A suitable exception throw.
        else
        {
            if(Read(id) is null) //If it is allowed to delete the entity - check if it exists in the list.
                throw new DalDoesNotExistException($"Task with ID={id} doesn't exist"); //A suitable exception throw.
            DataSource.Tasks.Remove(Read(id)!); //Deleting the object from the list.
        }

    }
}