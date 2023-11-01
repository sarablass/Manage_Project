
namespace DalList;

//A class called DataSource in which a list is defined for each entity.
internal static class DataSource 
{
    //A class that will generate automatic running numbers for us, for the fields that are defined as "running ID number" in task and dependency entities.
    internal static class Config
    {
        internal const int startTaskId = 1; //A const numeric field, with the internal permission, that will receive an initial value for the number running, the smallest identifying number.
        private static int nextTaskId = startTaskId; //A static numeric field, with the private permission, that will receive as an initial value the previous fixed field.
        internal static int NextTaskId { get => nextTaskId++; } //A property with only get that will advance the private field automatically, with a number greater than the previous one by 1.


        internal const int startDependencyId = 1; //A const numeric field, with the internal permission, that will receive an initial value for the number running, the smallest identifying number.
        private static int nextDependencyId = startDependencyId; //A static numeric field, with the private permission, that will receive as an initial value the previous fixed field.
        internal static int NextDependencyId { get => nextDependencyId++; } //A property with only get that will advance the private field automatically, with a number greater than the previous one by 1.

    }

    internal static List<DO.Task> Tasks { get; } = new(); //A list containing all references to entities of type task.
    internal static List<DO.Engineer> Engineers { get; } = new(); //A list containing all references to entities of type engineer.
    internal static List<DO.Dependency> Dependencies { get; } = new(); //A list containing all entity references of dependency type.


}
