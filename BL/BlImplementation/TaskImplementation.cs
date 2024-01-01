

using BO;
using DalApi;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    public int Create(BO.Task boTask)
    {
        Tools.ValidationId(boTask.Id);
        Tools.ValidationString(boTask.Alias!);


        DO.Task doTask = new DO.Task
         (boTask.Id, boTask.Description, boTask.Alias, false, boTask.CreateAt, boTask.RequiredEffortTime, (DO.EngineerExperience)boTask.ComplexityLevel!, boTask.IsActive, boTask.Start, boTask.ForecastDate, boTask.Deadline, boTask.Complete, boTask.Deliverables, boTask.Remarks, boTask.Engineer.Id);
        try
        {
            var dependenciesToCreate = item.Dependencies
                .Select(task => new DO.Dependency
                {
                    DependentTask = item.Id,
                    DependsOnTask = task.Id
                })
                .ToList();
            dependenciesToCreate.ForEach(dependency => _dal.Dependency.Create(dependency));

            int newId = _dal.Task.Create(doTask);

            return newId;
        }
        catch (DO.DalAlreadyExistsException ex)
        {
            throw new BO.BlAlreadyExistsException($"Task with ID={item.Id} already exists", ex);
        }







        }

        public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(int id)
    {
        throw new NotImplementedException();
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Reset()
    {
        throw new NotImplementedException();
    }

    public void Update(DO.Task item)
    {
        throw new NotImplementedException();
    }
}
