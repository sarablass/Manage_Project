namespace DalList;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the dependency entity.
internal class DependencyImplementation : IDependency
{
    //A method that creates a new dependency.
    public int Create(Dependency item)
    {
        //for entities with auto id
        int id = DataSource.Config.NextDependencyId; //Inserting the value of the next running number into the Id variable.
        Dependency copy = item with { Id = id }; //Creating a copy of the new object that arrived as a parameter and updating the id with the new runner number.
        DataSource.Dependencies.Add(copy); //Adding the copy reference to the list of objects of type dependency.
        return id; //Returning the new id of the newly added object to the list.
    }

    //A method that requests/receives a single object.
    public Dependency? Read(int id)
    {
        return DataSource.Dependencies.FirstOrDefault(x => x.Id == id);
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        return DataSource.Dependencies.FirstOrDefault(x => filter(x));
    }

    //A method that requests/receives all objects of a certain type.
    public IEnumerable<Dependency> ReadAll(Func<Dependency, bool>? filter = null) 
    {
        if (filter != null)
        {
            return from item in DataSource.Dependencies
                   where filter(item)
                   select item;
        }
        return from item in DataSource.Dependencies
               select item;
    }

    //A method that updates an existing object.
    public void Update(Dependency item)
    {
        Dependency? removeDependency = Read(item.Id)!;
        if (Read(item.Id) is null) //Checking if there is an object with the same ID number, in the list.
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} doesn't exist"); //A suitable exception throw.

        DataSource.Dependencies.Remove(removeDependency); //Removes the reference to an existing object from a list.
        DataSource.Dependencies.Add(item); //Added to the list the reference to the updated object received as a parameter.
    }

    public void Delete(int id)
    {
            if (Read(id) is null) //checks if it exists in the list.
                throw new DalDoesNotExistException($"Dependency with ID={id} doesn't exist"); //A suitable exception throw.
            DataSource.Dependencies.Remove(Read(id)!); //Deleting the object from the list.
    }

    public void Reset()
    {
        DataSource.Dependencies.Clear();
    }
}
