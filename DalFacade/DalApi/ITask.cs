﻿namespace DalApi;
using DO;

//Interface for the task entity.
public interface ITask
{
    int Create(Task item); //Creates new entity object in DAL.
    Task? Read(int id); //Reads entity object by its ID.
    List<Task> ReadAll(); //Reads all entity objects.
    void Update(Task item); //Updates entity object.
    void Delete(int id); //Deletes an object by is Id.

}
