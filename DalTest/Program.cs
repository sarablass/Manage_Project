﻿using DalApi;
using DalList;
using DO;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DalTest;

internal class Program
{
    private static ITask? s_dalTask = new TaskImplementation();
    private static IEngineer? s_dalEngineer = new EngineerImplementation();
    private static IDependency? s_dalDependency = new DependencyImplementation();

    public static void EntityTask(char choice)
    {
        switch (choice)
        {
            case '0':
                break;

            case 'a'://add
                Console.WriteLine("enter task's describtion");
                string _description = Console.ReadLine()!;
                Console.WriteLine("enter task's alias");
                string _alias = Console.ReadLine()!;
                Console.WriteLine("enter milestone");
                bool _milestone = bool.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's date of created");
                DateTime _createdAt = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's date of start");
                DateTime? _start = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter an estimated task completion date");
                DateTime? _scheduledDate = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter a deadline for completing a task");
                DateTime? _deadline = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter the actual end date of a task");
                DateTime? _complete = Convert.ToDateTime(Console.ReadLine());
                Console.WriteLine("enter task's deliverables");
                string? _deliverables = Console.ReadLine();
                Console.WriteLine("enter task's remarks");
                string? _remarks = Console.ReadLine();
                Console.WriteLine("enter engineer ld");
                int _engineerld = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter task's level - press 0 for expert,1 for Junior,2 for Rookie");
                EngineerExperience? _complexityLevel = (EngineerExperience)int.Parse(Console.ReadLine()!);
                DO.Task t = new DO.Task(0, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _deadline, _complete, _deliverables, _remarks, _engineerld, _complexityLevel);
                try
                {

                    s_dalTask!.Create(t);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'b'://read by id
                Console.WriteLine("enter task's id to read");
                int _id = int.Parse(Console.ReadLine()!);
                try
                {
                    Console.WriteLine(s_dalTask!.Read(_id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'c'://read all
                Console.WriteLine("all the tasks:");
                List<DO.Task> ReadAllTasks = s_dalTask!.ReadAll();
                foreach (var item in ReadAllTasks)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of task to update");
                int _updateId = int.Parse(Console.ReadLine()!); //search of the id to update
                try
                {
                    Console.WriteLine(s_dalTask!.Read(_updateId));
                    Console.WriteLine("enter details to update");
                    Console.WriteLine("enter task's describtion");
                     _description = Console.ReadLine()!;
                    Console.WriteLine("enter task's alias");
                    _alias = Console.ReadLine()!;
                    Console.WriteLine("enter milestone");
                    _milestone = bool.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's date of created");
                    _createdAt = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's date of start");
                    _start = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter an estimated task completion date");
                    _scheduledDate = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter a deadline for completing a task");
                    _deadline = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter the actual end date of a task");
                    _complete = Convert.ToDateTime(Console.ReadLine());
                    Console.WriteLine("enter task's deliverables");
                    _deliverables = Console.ReadLine();
                    Console.WriteLine("enter task's remarks");
                    _remarks = Console.ReadLine();
                    Console.WriteLine("enter engineer ld");
                    _engineerld = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter task's level - press 0 for expert,1 for Junior,2 for Rookie");
                    _complexityLevel = (EngineerExperience)int.Parse(Console.ReadLine()!);
                     t = new DO.Task(0, _description, _alias, _milestone, _createdAt, _start, _scheduledDate, _deadline, _complete, _deliverables, _remarks, _engineerld, _complexityLevel);
                    s_dalTask!.Update(t);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'e': //delete task
                Console.WriteLine("enter id of task to delete");
                _id = int.Parse(Console.ReadLine()!);
                try
                {
                    s_dalTask!.Delete(_id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            default:
                break;
        }
    }

    public static void EntityEngineer(char choice)
    {
        switch (choice)
        {
            case '0':
                break;

            case 'a': //add
                Console.WriteLine("enter engineer's id to add");
                int _id = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter engineer's name");
                string _name = Console.ReadLine()!;
                Console.WriteLine("enter engineer's email");
                string _email = Console.ReadLine()!;
                Console.WriteLine("enter engineer's level - press 0 for expert,1 for Junior,2 for Rookie");
                EngineerExperience? _level = (EngineerExperience)int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter engineer's cost");
                double? _cost = double.Parse(Console.ReadLine()!);
                Engineer e = new Engineer(_id, _name, _email, _level, _cost);
                try
                {
                    s_dalEngineer!.Create(e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'b'://read by id
                Console.WriteLine("enter engineer's id to read");
                _id = int.Parse(Console.ReadLine()!);
                try
                {
                    Console.WriteLine(s_dalEngineer!.Read(_id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'c'://read all
                Console.WriteLine("all the engineers:");
                List<Engineer> ReadAllEngineers = s_dalEngineer!.ReadAll();
                foreach (var item in ReadAllEngineers)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of engineer to update");
                int _updateId = int.Parse(Console.ReadLine()!);//search of the id to update
                try
                {
                    Console.WriteLine(s_dalEngineer!.Read(_updateId));
                    _id = _updateId;
                    Console.WriteLine("enter Engineer's name");
                    _name = Console.ReadLine()!;
                    Console.WriteLine("enter Engineer's email");
                    _email = Console.ReadLine()!;
                    Console.WriteLine("enter engineer's level - press 0 for expert,1 for Junior,2 for Rookie");
                    _level = (EngineerExperience)int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter engineer's cost");
                    _cost = double.Parse(Console.ReadLine()!);
                    e = new Engineer(_id, _name, _email, _level, _cost);
                    s_dalEngineer.Update(e);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'e'://delete an order
                Console.WriteLine("enter id of engineer to delete");
                int _deleteId = int.Parse(Console.ReadLine()!);
                try
                {
                    s_dalEngineer!.Delete(_deleteId);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            default:
                break;
        }
    }

    public static void EntityDependency(char choice)
    {
        switch (choice)
        {
            case '0':
                break;

            case 'a': //add
                Console.WriteLine("enter pending task id number");
                int _idDependentTask = int.Parse(Console.ReadLine()!);
                Console.WriteLine("enter a previous task id number");
                int _idDependsOnTask = int.Parse(Console.ReadLine()!);
                Dependency d = new Dependency(0, _idDependentTask, _idDependsOnTask);
                try
                {
                    s_dalDependency?.Create(d);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'b'://read by id
                Console.WriteLine("enter dependency's id to read");
                int _id = int.Parse(Console.ReadLine()!);
                try
                {
                    Console.WriteLine(s_dalDependency?.Read(_id));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            case 'c'://read all
                Console.WriteLine("all the dependencies:");
                List<DO.Dependency> ReadAllDependencys = s_dalDependency!.ReadAll();
                foreach (var item in ReadAllDependencys)
                    Console.WriteLine(item);
                break;

            case 'd'://update
                Console.WriteLine("enter id of dependency to update");
                int _updateId = int.Parse(Console.ReadLine()!); //search of the id to update
                try
                {
                    Console.WriteLine(s_dalDependency?.Read(_updateId));
                    _id = _updateId;
                    Console.WriteLine("enter pending task id number");
                    _idDependentTask = int.Parse(Console.ReadLine()!);
                    Console.WriteLine("enter a previous task id number");
                    _idDependsOnTask = int.Parse(Console.ReadLine()!);
                    Dependency depUpdate = new Dependency(_id, _idDependentTask, _idDependsOnTask);
                    s_dalDependency!.Update(depUpdate);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                break;

            default:
                break;
        }
    }

    static void Main(string[] args)
    {
        Initialization.Do(s_dalTask, s_dalEngineer, s_dalDependency);

        Console.WriteLine("for Task press 1 \nfor Engineer press 2 \nfor Dependency press 3 \nfor exit press 0");
 
        int select = int.Parse(Console.ReadLine()!);
        char choice;
        while (select != 0)
        {
            switch (select)
            {
                case 1:
                    Console.WriteLine("for exit press 0 \nfor add a Task press a \nfor read a Task press b \nfor read all Tasks press c \nfor update a Task press d \nfor delete a Task press e");
                    choice = char.Parse(Console.ReadLine()!);
                    EntityTask(choice);
                    break;

                case 2:
                    Console.WriteLine("for exit press 0 \nfor add a Engineer press a \nfor read a Engineer press b \nfor read all Engineers press c \nfor update a Engineer press d \nfor delete a Engineer press e");
                    choice = char.Parse(Console.ReadLine()!);
                    EntityEngineer(choice);
                    break;

                case 3:
                    Console.WriteLine("for exit press 0 \nfor add a dependency press a \nfor read a dependency press b \nfor read all dependencies press c \nfor update a dependency press d \nfor delete a dependency press e");
                    choice = char.Parse(Console.ReadLine()!);
                    EntityDependency(choice);
                    break;

                default:
                    break;
            }
           
            Console.WriteLine("\nfor Task press 1 \nfor Engineer press 2 \nfor Dependency press 3 \nfor exit press 0");
            select = int.Parse(Console.ReadLine()!);
        }
    }
}
