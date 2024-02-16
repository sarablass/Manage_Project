using BlApi;
using BO;
using DO;
using System.Threading.Tasks;

namespace BlImplementation;

/// <summary>
/// Implementation of the ITask interface.
/// </summary>
internal class TaskImplementation : ITask 
{
    private DalApi.IDal _dal = DalApi.Factory.Get; // Instance of the dal.

    /// <summary>
    /// Method to calculate milestone for a task.
    /// </summary>
    private BO.MilestoneInTask? CalculateMilestone(DO.Task doTask)
    {
        DO.Dependency? dependency = _dal.Dependency.ReadAll(dep => dep.DependsOnTask == doTask.Id && _dal.Task.Read(dep.DependentTask)!.Milestone).FirstOrDefault();
        if (dependency is null)
            return null;
        DO.Task task = _dal.Task.Read(dependency!.DependentTask)!;
        return  new BO.MilestoneInTask() { Id = task.Id, Alias = task.Alias };
    }

    /// <summary>
    /// Method to calculate tasks in a list based on dependencies
    /// </summary>
    public List<TaskInList> CalculateTaskInList(int id)
    {
        var dependenciesList = _dal.Dependency.ReadAll(); //Creating a list of all dependencies whose id of our current task equals the id of the dependent task.
        var dependentTasks = (from dependency in dependenciesList  //goes through each of the dependencies.
                              where dependency.DependentTask == id // Filtering dependencies for the given task ID
                              let taskDependOn = _dal.Task.Read(dependency.DependsOnTask)
                              select new BO.TaskInList() //Creating BO.TaskInList objects for dependent tasks.
                              {
                                  Id = dependency.DependsOnTask,
                                  Description = taskDependOn?.Description,
                                  Alias = taskDependOn?.Alias,
                                  Status = CalculateStatus(taskDependOn) // Calculating status for each dependent task
                              });
        return dependentTasks.ToList(); // Returning list of dependent tasks
    }

    /// <summary>
    /// Method to calculate status for a task.
    /// </summary>
    private BO.Status CalculateStatus(DO.Task? doTask)
    {
      // Calculating status based on task properties
        if (doTask is null)
        {
            return (BO.Status)0;
        }
        if (doTask.Start == null) //An unplanned task
            return (BO.Status)0;
        else if (doTask.Start != null && doTask.Start > DateTime.Now) //scheduled task
            return (BO.Status)1;
        else if (doTask.ScheduledDate != null && doTask.Complete != null) //mission on the track
            return (BO.Status)2;
        else if (doTask.Complete == null && doTask.Deadline < DateTime.Now) //overdue task
            return (BO.Status)3;
        else
            return (BO.Status)4; //task accomplished
    }

    /// <summary>
    /// Method to create a new task
    /// </summary>
    public int Create(BO.Task boTask)
    {
        // Validating task ID and alias
        BO.Tools.ValidationId(boTask.Id);
        BO.Tools.ValidationString(boTask.Alias!);

        // Creating DO.Task object based on BO.Task object
        DO.Task doTask = new DO.Task
         (boTask.Id, boTask.Description!, boTask.Alias!, false, boTask.CompleteDate - boTask.StartDate, boTask.CreateAt, boTask.StartDate, boTask.BaselineStartDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexityLevel!, boTask.IsActive);

        // Creating dependencies if any
        if (boTask.Dependencies != null) {
            var dependenciesList = boTask.Dependencies!
               .Select(task => new DO.Dependency
               {
                   DependentTask = boTask.Id,
                   DependsOnTask = task.Id
               })
               .ToList();
            dependenciesList.ForEach(dependency => _dal.Dependency.Create(dependency));
        }

        // Creating new task in the data store and returning the task ID
        int newTaskId = _dal.Task.Create(doTask);
        return newTaskId;
    }

    /// <summary>
    /// Method to delete a task
    /// </summary>
    public void Delete(int id)
    {
        // Throwing an exception indicating that task deletion is not possible
        throw new BO.BlDeletionImpossible($"Task with ID={id} cannot be deleted");
    }

    /// <summary>
    /// Method to read a task
    /// </summary>
    public BO.Task? Read(int id)
    {
        // Reading a task from the data store
        DO.Task? doTask = _dal.Task.Read(id);
        if (doTask is null)
            throw new BO.BlDoesNotExistException($"Task with ID={id} does Not exist");
        var taskEngineer = _dal.Engineer.ReadAll(engineer => doTask.EngineerId == engineer.Id).FirstOrDefault(); //Looking for the task engineer
        return new BO.Task() //building and returning a BO.Task object
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
            Engineer = taskEngineer == null ? null : new BO.EngineerInTask()
            {
                Id = taskEngineer.Id,
                Name = taskEngineer?.Name
            },
            ComplexityLevel = (BO.EngineerExperience)doTask.ComplexityLevel!,
            Dependencies = CalculateTaskInList(doTask.Id)!,
            IsActive = doTask.Active
        };
    }

    /// <summary>
    /// Method to read all tasks
    /// </summary>
    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        try
        {
            // Reading all tasks from the data store
            IEnumerable<BO.Task?> tasksList =
           from DO.Task doTask in _dal.Task.ReadAll()
           let taskEngineer = _dal.Engineer.ReadAll(engineer => doTask.EngineerId == engineer.Id).FirstOrDefault() //Find the engineer for each task
           select new BO.Task()
           {
               // Constructing a BO.Task object for each task retrieved from the data store
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

            // Applying filter function if provided
            if (filter == null)
              return tasksList!;
           return tasksList.Where(filter!)!;
        }
        catch (DO.DalDoesNotExistException ex)
        {
            // Handling exceptions
            throw new BO.BlDoesNotExistException(ex.Message);
        }
    }

    /// <summary>
    ///Method to update a task
    /// </summary>
    public void Update(BO.Task boTask)
    {
        // Validating task ID and alias
        BO.Tools.ValidationId(boTask.Id);
        BO.Tools.ValidationString(boTask.Alias!);

        // Creating DO.Task object based on BO.Task object
        DO.Task doTask = new DO.Task
        (boTask.Id, boTask.Description!, boTask.Alias!, false, boTask.CompleteDate - boTask.StartDate, boTask.CreateAt, boTask.StartDate, boTask.BaselineStartDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexityLevel!, boTask.IsActive);
        try
        {
            //updating dependencies in task if any
            if (boTask.Dependencies != null)
            {
                var dependenciesList = boTask.Dependencies!
                   .Select(task => new DO.Dependency
                   {
                       DependentTask = boTask.Id,
                       DependsOnTask = task.Id
                   })
                   .ToList();
                dependenciesList.ForEach(dependency => _dal.Dependency.Create(dependency));
            }

            // Updating the task in the data store
            _dal.Task.Update(doTask);
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BO.BlDoesNotExistException(ex.Message);
        }
    }
}
