using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Dal;

internal class DependencyImplementation : IDependency
{
    //A method that creates a new dependency.
    public int Create(Dependency item)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        int newId = Config.NextDependencyId;

        XElement newDependency = new XElement("Dependency",
            new XElement("Id", newId),
            new XElement("DependentTask", item.DependentTask),
            new XElement("DependsOnTask", item.DependsOnTask));

        xmlDependencies.Add(newDependency);

        XMLTools.SaveListToXMLElement(xmlDependencies, "dependencies");

        return newId;
    }

    public void Delete(int id)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");

        if (Read(id) is null) 
            throw new DalDoesNotExistException($"Dependency with ID={id} doesn't exist");
        
        XElement? findDependency = xmlDependencies.Elements("Dependency").FirstOrDefault(x => (int)x.Element("Id")! == id);
        findDependency!.Remove();
        XMLTools.SaveListToXMLElement(xmlDependencies, "dependencies");
    }

    public Dependency? Read(int id)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        XElement? dependency = xmlDependencies.Descendants("Dependency").FirstOrDefault(x => (int)x.Element("Id")! == id);
        if (dependency is null)
            return null;
        Dependency readDependency = new Dependency((int)dependency.Element("Id")!, (int)dependency.Element("DependentTask")!, (int)dependency.Element("DependsOnTask")!);
        return readDependency;
    }

    public Dependency? Read(Func<Dependency, bool> filter)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        Dependency? dependency = xmlDependencies.Elements("Dependency")
            .Select(x => new Dependency((int)x.Element("Id")!, (int)x.Element("DependentTask")!, (int)x.Element("DependsOnTask")!))
            .FirstOrDefault(filter);

        return dependency;
    }

    public IEnumerable<Dependency?> ReadAll(Func<Dependency, bool>? filter = null)
    {
        XElement? xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        IEnumerable<Dependency?> dependenciesList = xmlDependencies.Elements("Dependency")
            .Select(x => new Dependency((int)x.Element("Id")!, (int)x.Element("DependentTask")!, (int)x.Element("DependsOnTask")!));
        if (filter != null)
        {
            dependenciesList= dependenciesList.Where(filter!);
        }
        return dependenciesList;
    }

    public void Reset()
    {
        XElement xmlDataConfig = XMLTools.LoadListFromXMLElement("data-config");
        xmlDataConfig.Element("NextDependencyId")?.SetValue((250).ToString());
        XMLTools.SaveListToXMLElement(xmlDataConfig, "data-config");
        XElement xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        xmlDependencies.RemoveAll();
        XMLTools.SaveListToXMLElement(xmlDependencies, "dependencies");
    }

    public void Update(Dependency item)
    {
        if (Read(item.Id) is null) 
            throw new DalDoesNotExistException($"Dependency with ID={item.Id} doesn't exist");

        XElement xmlDependencies = XMLTools.LoadListFromXMLElement("dependencies");
        XElement? dependency = xmlDependencies.Elements("Dependency")
            .FirstOrDefault(x => (int)x.Element("Id")! == item.Id);

        dependency!.SetElementValue("DependentTask", item.DependentTask);
        dependency.SetElementValue("DependsOnTask", item.DependsOnTask);

        XMLTools.SaveListToXMLElement(xmlDependencies, "dependencies");
    }
}
