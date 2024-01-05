using BlApi;
namespace BlImplementation;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Engineer boEngineer)
    {

        //try?
        BO.Tools.ValidationId(boEngineer.Id);
        BO.Tools.ValidationString(boEngineer.Name!);
        BO.Tools.ValidationEmail(boEngineer.Email!);
        BO.Tools.ValidationCost((double)boEngineer.Cost!);

        DO.Engineer doEngineer = new DO.Engineer
         (boEngineer.Id, boEngineer.Name!, boEngineer.Email!, (DO.EngineerExperience)boEngineer.Level!, (double)boEngineer.Cost!, boEngineer.IsActive);
        try
        {
            int idEng = _dal.Engineer.Create(doEngineer);
            return idEng;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Engineer with ID={boEngineer.Id} already exists", ex);
        }
    }

    public void Delete(int id)
    {
        throw new BO.BlDeletionImpossible($"Engineer with ID={id} cannot be deleted");
    }

    public BO.Engineer? Read(int id)
    {
        DO.Engineer? doEngineer = _dal.Engineer.Read(id);
        if (doEngineer is null)
            throw new BO.BlDoesNotExistException($"Engineer with ID={id} does Not exist");

        DO.Task task = _dal.Task.ReadAll().FirstOrDefault(task => task?.EngineerId == doEngineer.Id)!;
        return new BO.Engineer()
        {
            Id = doEngineer.Id,
            Name = doEngineer.Name,
            Email = doEngineer.Email,
            Level = (BO.EngineerExperience)doEngineer.Level!,
            Cost = (double)doEngineer.Cost!,
            Task = new BO.TaskInEngineer()
            { Id = task!.Id, Alias = task.Alias! }
        };
    }

    public IEnumerable<BO.Engineer> ReadAll(Func<BO.Engineer, bool>? filter = null)
    {
        IEnumerable<BO.Engineer?> boEngineersList =
           from DO.Engineer doEngineer in _dal.Engineer.ReadAll()
           let task = _dal.Task.ReadAll().FirstOrDefault(task => task?.EngineerId == doEngineer.Id)
           select new BO.Engineer()
           {
               Id = doEngineer.Id,
               Name = doEngineer.Name,
               Email = doEngineer.Email,
               Level = (BO.EngineerExperience)doEngineer.Level!,
               Cost = (double)doEngineer.Cost!,
               Task = new BO.TaskInEngineer()
               { Id = task!.Id, Alias = task.Alias! }
           };
        if (filter is null)
            return boEngineersList!;
        return boEngineersList.Where(filter!)!;
    }

    public void Update(BO.Engineer boEngineer)
    {
        //try?
        BO.Tools.ValidationId(boEngineer.Id);
        BO.Tools.ValidationString(boEngineer.Name!);
        BO.Tools.ValidationEmail(boEngineer.Email!);
        BO.Tools.ValidationCost((double)boEngineer.Cost!);

        DO.Engineer doEngineer = new DO.Engineer
         (boEngineer.Id, boEngineer.Name!, boEngineer.Email!, (DO.EngineerExperience)boEngineer.Level!, (double)boEngineer.Cost!, boEngineer.IsActive);
        try
        {
            _dal.Engineer.Update(doEngineer);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }
    }
}
