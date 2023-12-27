using DalApi;
using System.Diagnostics;

namespace Dal;

sealed internal class DalXml : IDal
{
    private static readonly Lazy<DalXml> lazyInstance = new Lazy<DalXml>(() => new DalXml(),true);
    public static IDal Instance => lazyInstance.Value;

    private DalXml() { }
    
    public ITask Task => new TaskImplementation();

    public IEngineer Engineer => new EngineerImplementation();

    public IDependency Dependency => new DependencyImplementation();

    public void Reset()
    {
        Engineer.Reset();
        Dependency.Reset();
        Task.Reset();
    }
}
