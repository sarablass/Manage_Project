namespace DalApi;
using DO;

//Interface for the Engineer entity.
public interface IEngineer
{
    int Create(Engineer item); //Creates new entity object in DAL
    Engineer? Read(int id); //Reads entity object by its ID
    List<Engineer> ReadAll(); //Reads all entity objects
    void Update(Engineer item); //Updates entity object
    void Delete(int id); //Deletes an object by is Id
}
