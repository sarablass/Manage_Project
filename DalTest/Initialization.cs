namespace DalTest;
using DalApi;
using DO;
using System.Net;
using System.Security.Cryptography;
using System.Xml.Linq;

public static class Initialization
{
    private static ITask? s_dalTask;
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;
    private static readonly Random s_rand = new();

    private static void createTasks()
    {
        string _description;
        string _alias;
        List<Engineer> EngineersList = s_dalEngineer!.ReadAll();

        (string, string)[] tasks =
        {
            ("Serve the prepared food from the restaurant kitchen to the diners","serve"),
            ("Preparation of dough for all types of bread","baker"),
            ("Chopping the vegetables for all types of salads","cutting vegetables"),
            ("Washing the dirty dishes that come back from the diners","dishwashing"),
            ("Washing the dirty floors","washing floors"),
            ("The seasoning of the various dishes in the restaurant","seasoning"),
            ("to plate the dishes served to the diners","plate"),
            ("Reading the orders from the diners","reading"),
            ("Welcoming guests to the restaurant","Hospitality"),
            ("The arrangement of the tables for the diners","arranging tables"),
        };


        for (int i = 0; i < 10; i++)
        {


            foreach (var task in tasks)
            {
                _description = task.Item1;
                _alias = task.Item2;
                DateTime CreatedAt = DateTime.Now;
                DateTime Start = CreatedAt.AddDays(s_rand.Next(0, 7));
                DateTime ScheduledDate = Start.AddMinutes(s_rand.Next(5, 300));
                DateTime Deadline = ScheduledDate.AddMinutes(s_rand.Next(0, 30));
                DateTime? Complete = Deadline.AddMinutes(s_rand.Next(0, 30));
                Task newTask = new(0, _description, _alias, false, CreatedAt, Start, ScheduledDate, Deadline, Complete, null, null, EngineersList[i].Id, EngineersList[i].Level);
                s_dalTask!.Create(newTask);
            }
        }
    }
    private static void createEngineers()
    {
        int _min_id = 200000000, _max_id = 400000000;

        (string, string)[] engineers =
        {
            ("Dani Levi","Dani6464@gmail.com"),
            ("Sara Blass","saraBlass@gmail.com"),
            ("Brachi Kim","Brachikim@gmail.com"),
            ("Ruti Leybuvi","ruti@gmail.com"),
            ("Avraham Cohen","Avi343@gmail.com"),
            ("Netanel Shochat","Net323@gmail.com"),
            ("Noa Tager","Noa3254@gmail.com"),
            ("Tamar Mitelman","TamarM910@gmail.com"),
            ("Yosef Berko","yosBerko@gmail.com"),
            ("Motti Golo","mottigolo2365@gmail.com"),
        };

        int _id;
        string _name, _email;
        double? _cost = 0;
        EngineerExperience _level;

        for (int i = 0; i < 4; i++)
        {
            foreach (var engineer in engineers)
            {
                do
                    _id = s_rand.Next(_min_id, _max_id);
                while (s_dalEngineer!.Read(_id) != null);

                _name = engineer.Item1;
                _email = engineer.Item2;

                int _rand_level = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
                _level = (EngineerExperience)_rand_level;
                switch (_rand_level)
                {
                    case 0: _cost = 1000;
                        break;

                    case 1: _cost = 700;
                        break;

                    case 2: _cost = 400;
                        break;

                }
                Engineer newEng = new(_id, _name, _email, _level, _cost);
                s_dalEngineer!.Create(newEng);
            }
        }

    }
    private static void createDependencies()
    {
        for (int i = 0; i < 250; i++)
        {
            Dependency newDep = new(0, i + 1, i);
            s_dalDependency!.Create(newDep);
        }
        //int num = 0;
        //List<Task> TasksList = s_dalTask!.ReadAll();
        //for (int i = 0; i < 250; i++)
        //{
        //    Dependency newDep = new(0, TasksList[num + 1].Id, TasksList[num].Id);
        //    s_dalDependency!.Create(newDep);
        //    num++;
        //    if (num == 100)
        //    {
        //        num = 0;
        //    }
        //}
    }

    public static void Do(ITask? dalTask, IEngineer? dalEngineer, IDependency? dalDependency)
    {
        s_dalTask = dalTask ?? throw new NullReferenceException("DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException("DAL can not be null!");
        s_dalDependency = dalDependency ?? throw new NullReferenceException("DAL can not be null!");

        createEngineers();
        createTasks();
        createDependencies();
    }

}
