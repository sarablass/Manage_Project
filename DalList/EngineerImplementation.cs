namespace DalList;
using DalApi;
using DO;
using System.Collections.Generic;

//Implementation of the methods of the engineer entity.
public class EngineerImplementation : IEngineer
{
    //A method that produces a new engineer.
    public int Create(Engineer item)
    {       
        //for entities with normal id (not auto id)
        if (Read(item.Id) is not null) //Checking if there is an object with the same ID number, in the list.
            throw new Exception($"Engineer with ID={item.Id} already exists"); //A suitable exception throw.
        DataSource.Engineers.Add(item); //Adding the received object reference to the object list.
        return item.Id; //Returning the new id of the newly added object to the list.
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Engineer? Read(int id)
    {
        throw new NotImplementedException();
    }

    public List<Engineer> ReadAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Engineer item)
    {
        throw new NotImplementedException();
    }
}
