﻿namespace DalList;
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

    //A method that requests/receives a single object.
    public Engineer? Read(int id)
    {
        return DataSource.Engineers.Find(x => x.Id == id);
    }

    //A method that requests/receives all objects of a certain type.
    public List<Engineer> ReadAll()
    {
        return new List<Engineer>(DataSource.Engineers);
    }

    //A method that updates an existing object.
    public void Update(Engineer item)
    {
        if (Read(item.Id) is null) //Checking if there is an object with the same ID number, in the list.
            throw new Exception($"Engineer with ID={item.Id} doesn't exist"); //A suitable exception throw.
        DataSource.Engineers.Remove(item); //Removes the reference to an existing object from a list.

        DataSource.Engineers.Add(item); //Added to the list the reference to the updated object received as a parameter.
    }

    //A method that deletes an existing object.
    public void Delete(int id)
    {
        if ((DataSource.Tasks.Find(x => x.EngineerId == id)) is not null) //Checks if the entity is an entity that must not be deleted - checks if there is an engineer assignment.
            throw new Exception($"Engineer with ID={id} cannot be deleted"); //A suitable exception throw.
        else
        {
            if (Read(id) is null) //If it is allowed to delete the entity - check if it exists in the list.
                throw new Exception($"Engineer with ID={id} doesn't exist"); //A suitable exception throw.
            DataSource.Engineers.Remove(Read(id)); //Deleting the object from the list.
        }
    }
}
