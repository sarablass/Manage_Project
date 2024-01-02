
using BlApi;



namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = DalApi.Factory.Get;
    //private BO.MilestoneInTask? calculateMilestone(List<BO.TaskInList?>? tasksInList)
    //{
    //    if (tasksInList == null) return null;
    //    BO.TaskInList? task = tasksInList.Where(task => task != null && _dal.Task.Read(task.Id)!.Milestone == true).FirstOrDefault();
    //    if (task == null) return null;
    //    return new BO.MilestoneInTask() { Id = task.Id, Alias = task.Alias };
    //}

    private BO.MilestoneInTask? CalculateMilestone(int id)
    {
    //השראה הגאונית של ברכי לא משנה שיש פה אדום העיקרררררר הכווונה
        List<BO.MilestoneInTask?>? milestoneInTask;

        if (milestoneInTask == null) return null;
        BO.MilestoneInTask? milestoneTask = milestoneInTask.Where(task => task!.Id== id).FirstOrDefault();
        if (milestoneTask == null) return null;
        return new BO.MilestoneInTask() { Id = milestoneTask.Id, Alias = milestoneTask.Alias };
    }

    private IEnumerable<BO.TaskInList>? CalculateTaskInList(int id)
    {
            List<DO.Dependency?> DependenciesList = _dal.Dependency.ReadAll(dependency => dependency?.DependentTask == id).ToList();  //Creating a list of all dependencies whose id of our current task equals the id of the dependent task.
            List<DO.Task>? tasksList = null; //Initialize the list of relevant tasks.
            foreach (DO.Dependency? dependency in DependenciesList) //A loop that goes through each of the dependencies.
        {
               tasksList?.Add(_dal.Task.Read((int)dependency?.DependsOnTask!)!); //Add the previous tasks to the list of relevant tasks.
        }
            IEnumerable<BO.TaskInList> TasksInList = //Allocating a list of type TaskInList.
             from DO.Task doTask in tasksList! //For each task 
             select new BO.TaskInList() //Create an object of type TaskInList.
             {
                 Id = doTask.Id,
                 Description = doTask.Description!,
                 Alias = doTask.Alias!,
                 Status = CalculateStatus(doTask)
             };
            return TasksInList; //Return the TaskInList list.   
    }

    private BO.Status CalculateStatus(DO.Task doTask)
    {
        if (doTask.Start == null )//לא מתוכנן
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

    

    //private BO.EngineerInTask? CalculateEngineer(DO.Task doTask)
    //{
    //    BO.EngineerInTask engineer;
    //    if (doTask?.Engineerid == null)
    //    {
    //        engineer = new BO.EngineerInTask()
    //        {
    //            Id = doTask!.Engineerid,
    //            Name = _dal.Engineer.Read(doTask.Id)?.Name ?? string.Empty
    //        };
    //    }
    //    else
    //    {
    //        engineer = null!;
    //    }
    //    return engineer;
    //}

    public int Create(BO.Task boTask)
    {
        BO.Tools.ValidationId(boTask.Id);
        BO.Tools.ValidationString(boTask.Alias!);


        DO.Task doTask = new DO.Task
         (boTask.Id, boTask.Description, boTask.Alias, false, boTask.CompleteDate - boTask.StartDate, boTask.CreateAt, boTask.StartDate, boTask.ScheduledDate, boTask.DeadlineDate, boTask.CompleteDate, boTask.Deliverables, boTask.Remarks, boTask.Engineer!.Id, (DO.EngineerExperience)boTask.ComplexityLevel!, boTask.IsActive);

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
            Description =doTask.Description ,
            Alias =doTask.Alias ,
            Milestone = CalculateMilestone(doTask.Id),
            Status = CalculateStatus(doTask),
            CreateAt =(DateTime)doTask.CreatedAt! ,
            BaselineStartDate=,
            StartDate= (DateTime)doTask.Start!,
            ScheduledDate= (DateTime)doTask.ScheduledDate!,
            ForecastDate= doTask.Start + doTask.RequiredEffort,
            DeadlineDate= (DateTime)doTask.Deadline!,
            CompleteDate= (DateTime)doTask.Complete!,
            Deliverables= doTask.Deliverables,
            Remarks= doTask.Remarks,           
            Engineer= ,
            ComplexityLevel= (BO.EngineerExperience)doTask.ComplexityLevel!,
            Dependencies= (List<BO.TaskInList>)CalculateTaskInList(doTask.Id)!,
            IsActive=doTask.Active
        };
    }

    public IEnumerable<BO.Task> ReadAll(Func<BO.Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(BO.Task item)
    {
        throw new NotImplementedException();
    }
}
