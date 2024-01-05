using BlApi;
using System.Threading;


namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = DalApi.Factory.Get;

    private BO.Status CalculateStatus(DO.Task doTask)
    {
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

    public int Create()
    {
        throw new NotImplementedException();
    }

    public BO.Milestone? Read(int id)
    {
        DO.Task? taskAsMilestone;
        try
        {
            taskAsMilestone = _dal.Task.Read(milestone => milestone.Id == id && milestone.Milestone); //Request a task-type milestone from the data layer.
            if (taskAsMilestone == null) //If the requested task is not a milestone it will throw an error.
                throw new BO.BlDoesNotExistException($"Milstone with ID={id} does Not exist");
        }
        catch (Exception ex)
        {
            throw new BO.BlReadFailed("Failed to build milestone ", ex);
        }
        var idList = _dal.Dependency.ReadAll(id => id.DependentTask == taskAsMilestone.Id) //Find the list of tasks that the milestone depends on.
                                         .Select(id => id!.DependsOnTask);

            List<DO.Task> tasksList = _dal.Task.ReadAll(task => idList.Contains(task.Id)).ToList()!; //Bring the tasks that depend on their ID.

            //Construct for each task that the milestone depends on an object of type TaskInList.
            var tasksInList = tasksList.Select(task => new BO.TaskInList
            {
                Id = task.Id,
                Description = task.Description,
                Alias = task.Alias,
                Status = CalculateStatus(task)
            }).ToList();

            //Build a milestone object according to the received data and calculate additional information.
            return new BO.Milestone()
            {
                Id = taskAsMilestone.Id,
                Description = taskAsMilestone.Description,
                Alias = taskAsMilestone.Alias,
                CreatedAtDate = taskAsMilestone.CreatedAt,
                Status = CalculateStatus(taskAsMilestone),
                ForecastDate = taskAsMilestone.ScheduledDate,
                DeadlineDate = taskAsMilestone.Deadline,
                CompleteDate = taskAsMilestone.Complete,
                CompletionPercentage = (tasksInList.Count(task => task.Status == BO.Status.OnTrack) / tasksInList.Count * 0.1) * 100,
                Remarks = taskAsMilestone.Remarks,
                Dependencies = tasksInList!
            };
        
    }

    public void Update(BO.Milestone boMilestone)
    {
        BO.Tools.ValidationString(boMilestone.Alias!); //Validation check.

        DO.Task? taskAsMilestone;
        try
        {
            taskAsMilestone = _dal.Task.Read(milestone => milestone.Id == boMilestone.Id); //Request a task-type milestone from the data layer.
            if (taskAsMilestone == null) //If the requested task is null it will throw an error.
                throw new BO.BlDoesNotExistException($"Milstone with ID={boMilestone.Id} does Not exist");
        }
        catch (Exception ex)
        {
            throw new BO.BlReadFailed(ex.Message);
        }

        DO.Task doTask = new DO.Task //Create a task to update.
             (boMilestone.Id, boMilestone.Description!, boMilestone.Alias!, taskAsMilestone.Milestone, taskAsMilestone.RequiredEffort, taskAsMilestone.CreatedAt, taskAsMilestone.Start, taskAsMilestone.ScheduledDate, taskAsMilestone.Deadline, taskAsMilestone.Complete, taskAsMilestone.Deliverables, boMilestone.Remarks, taskAsMilestone.EngineerId, (DO.EngineerExperience)taskAsMilestone.ComplexityLevel!, taskAsMilestone.Active);
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
