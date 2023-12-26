//namespace DalList;
//using DalApi;

//sealed internal class DalList : IDal
//{
//    public static IDal Instance { get; } = new DalList();
//    private DalList() { }
//    public ITask Task => new TaskImplementation();

//    public IEngineer Engineer => new EngineerImplementation();

//    public IDependency Dependency => new DependencyImplementation();

//}
namespace DalList
{
    using DalApi;
    using System;

    sealed internal class DalList : IDal
    {
        private static readonly Lazy<DalList> lazyInstance = new Lazy<DalList>(() => new DalList(),true);

        public static IDal Instance => lazyInstance.Value;

        private DalList() { }

        public ITask Task => new TaskImplementation();

        public IEngineer Engineer => new EngineerImplementation();

        public IDependency Dependency => new DependencyImplementation();
    }
}

