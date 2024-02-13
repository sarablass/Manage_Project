using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    private BO.MilestoneInTask? CalculateMilestone(DO.Task doTask)
    {
        DO.Dependency? dependency = _dal.Dependency.ReadAll(dep => dep.DependsOnTask == doTask.Id && _dal.Task.Read(dep.DependentTask)!.Milestone).FirstOrDefault();
        if (dependency is null)
            return null;
        DO.Task task = _dal.Task.Read(dependency!.DependentTask)!;
        return  new BO.MilestoneInTask() { Id = task.Id, Alias = task.Alias };
    }



    //private IEnumerable<BO.TaskInList>? CalculateTaskInList(int id)
    //{
    //    List<DO.Dependency?> DependenciesList = _dal.Dependency.ReadAll(dependency => dependency?.DependentTask == id).ToList();  //Creating a list of all dependencies whose id of our current task equals the id of the dependent task.
    //    List<DO.Task>? tasksList = null; //Initialize the list of relevant tasks.
    //    foreach (DO.Dependency? dependency in DependenciesList) //A loop that goes through each of the dependencies.
    //    {
    //        tasksList?.Add(_dal.Task.Read((int)dependency?.DependsOnTask!)!); //Add the previous tasks to the list of relevant tasks.
    //    }
    //    IEnumerable<BO.TaskInList> TasksInList = //Allocating a list of type TaskInList.
    //     from DO.Task doTask in tasksList! //For each task 
    //     select new BO.TaskInList() //Create an object of type TaskInList.
    //     {
    //         Id = doTask.Id,
    //         Description = doTask.Description!,
    //         Alias = doTask.Alias!,
    //         Status = CalculateStatus(doTask)
    //     };
    //    return TasksInList; //Return the TaskInList list.   
    //}

    public List<TaskInList> CalculateTaskInList(int id)
    {
        var dependenciesList = _dal.Dependency.ReadAll(); //Creating a list of all dependencies whose id of our current task equals the id of the dependent task.
        //A loop that goes through each of the dependencies.
        var dependentTasks = (from dependence in dependenciesList
                              where dependence.DependentTask == id
                              let taskDependOn = _dal.Task.Read(dependence.DependsOnTask)
                              select new BO.TaskInList()
                              {
                                  Id = dependence.Id,
                                  Description = taskDependOn?.Description,
                                  Alias = taskDependOn?.Alias,
                                  Status = CalculateStatus(taskDependOn)
                              });
        return dependentTasks.ToList();
    }

    private BO.Status CalculateStatus(DO.Task? doTask)
    {
        if (doTask is null)
        {
            return (BO.Status)0;
        }
        if (doTask.Start == null)//לא מתוכנן
            return (BO.Status)0;
        else if (doTask.Start != null && doTask.Start > DateTime.Now)//מתוזמן
            return (BO.Status)1;
        else if (doTask.ScheduledDate != null && doTask.Complete != null)//על המסלול
            return (BO.Status)2;
        else if (doTask.Complete == null && doTask.Deadline < DateTime.Now)//פיגור
            return (BO.Status)3;
        else
            return (BO.Status)4;//סיום
    }



    private BO.EngineerInTask? CalculateEngineer(DO.Task doTask)
    {      
         
        DO.Engineer engineer = _dal.Engineer.Read(doTask.Id)!;
        if(engineer is null)
            return null;
        else
        {
            return new BO.EngineerInTask()
            {
                Id = engineer.Id,
                Name = engineer.Name
            };
        }
       
    }

    public int Create(BO.Task boTask)
    {
        BO.Tools.ValidationId(boTask.Id);
        BO.Tools.ValidationString(boTask.Alias!);


        DO.Task doTask = new DO.Task
         (boTask.Id, boTask.Description!, boTask.Alias!, false, boTask.CompleteDate - boTask.StartDate, boTask.CreateAt, boTask.StartDate, boTask.BaselineStartDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexityLevel!, boTask.IsActive);

        var dependenciesList = boTask.Dependencies!
            .Select(task => new DO.Dependency
            {
                DependentTask = boTask.Id,
                DependsOnTask = task.Id
            })
            .ToList();
        dependenciesList.ForEach(dependency => _dal.Dependency.Create(dependency));

        int newTaskId = _dal.Task.Create(doTask);
        return newTaskId;
    }

    public void Delete(int id)
    {
        throw new BO.BlDeletionImpossible($"Task with ID={id} cannot be deleted");
    }

    public BO.Task? Read(int id)
    {
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask is null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");

        return new BO.Task()
        {
            Id = doTask.Id,
            Description = doTask.Description,
            Alias = doTask.Alias,
            Milestone = doTask.Milestone ? new BO.MilestoneInTask() : null, /*CalculateMilestone(doTask),*/
            Status = CalculateStatus(doTask),
            CreateAt = doTask!.CreatedAt,
            BaselineStartDate =doTask.ScheduledDate,
            StartDate = doTask!.Start,
            ForecastDate = doTask.Start + doTask.RequiredEffort,
            DeadlineDate = doTask!.Deadline,
            CompleteDate = doTask!.Complete,
            Deliverables = doTask.Deliverables,
            Remarks = doTask.Remarks,
            Engineer = CalculateEngineer(doTask),
            ComplexityLevel = (BO.EngineerExperience)doTask.ComplexityLevel!,
            Dependencies = CalculateTaskInList(doTask.Id)!,
            IsActive = doTask.Active
        };
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task?> tasksList =
           from DO.Task doTask in _dal.Task.ReadAll()
           let taskEngineer = _dal.Engineer.ReadAll(engineer => doTask.EngineerId == engineer.Id).FirstOrDefault()
           select new BO.Task()
           {
               Id = doTask.Id,
               Description = doTask?.Description,
               Alias = doTask?.Alias,
               Milestone = doTask.Milestone ? new BO.MilestoneInTask() : null, /*CalculateMilestone(doTask),*/
               Status = CalculateStatus(doTask),
               CreateAt = doTask!.CreatedAt,
               BaselineStartDate = doTask.ScheduledDate,
               StartDate = doTask!.Start,
               ForecastDate = doTask.Start + doTask.RequiredEffort,
               DeadlineDate = doTask!.Deadline,
               CompleteDate = doTask!.Complete,
               Deliverables = doTask?.Deliverables,
               Remarks = doTask?.Remarks,
               Engineer = taskEngineer == null ? null : new BO.EngineerInTask()
               {
                   Id = taskEngineer.Id,
                   Name = taskEngineer?.Name
               },
               ComplexityLevel = (BO.EngineerExperience?)doTask.ComplexityLevel!,
               Dependencies = (List<BO.TaskInList>)CalculateTaskInList(doTask.Id)!,
               IsActive = doTask.Active
           };
        if (filter == null)
            return tasksList!;
        return tasksList.Where(filter!)!;
    }

    public void Update(BO.Task boTask)
    {
        BO.Tools.ValidationId(boTask.Id);
        BO.Tools.ValidationString(boTask.Alias!);

        DO.Task doTask = new DO.Task
        (boTask.Id, boTask.Description!, boTask.Alias!, false, boTask.CompleteDate - boTask.StartDate, boTask.CreateAt, boTask.StartDate, boTask.BaselineStartDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexityLevel!, boTask.IsActive);
        try
        {
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }
    }
}
