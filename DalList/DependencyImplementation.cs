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
        return DataSource.Dependencies.Find(x => x.Id == id);
    }

    //A method that requests/receives all objects of a certain type.
    public List<Dependency> ReadAll()
    {
        return new List<Dependency>(DataSource.Dependencies);
    }

    //A method that updates an existing object.
    public void Update(Dependency item)
    {
        if (Read(item.Id) is null) //Checking if there is an object with the same ID number, in the list.
            throw new Exception($"Dependency with ID={item.Id} doesn't exist"); //A suitable exception throw.
        DataSource.Dependencies.Remove(item); //Removes the reference to an existing object from a list.

        DataSource.Dependencies.Add(item); //Added to the list the reference to the updated object received as a parameter.
    }

    public void Delete(int id)
    {
        throw new Exception("You cannot delete an entity of type dependency");
    }
}
