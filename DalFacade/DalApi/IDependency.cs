namespace DalApi;
using DO;

//Interface for the dependency entity.
public interface IDependency
{
    int Create(Dependency item); //Creates new entity object in DAL
    Dependency? Read(int id); //Reads entity object by its ID
    List<Dependency> ReadAll(); //Reads all entity objects
    void Update(Dependency item); //Updates entity object
}
