namespace DalList;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the dependency entity.
public class DependencyImplementation : IDependency
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

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Dependency? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Dependency> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Dependency item)
    {
        throw new NotImplementedException();
    }
}
