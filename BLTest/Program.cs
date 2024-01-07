using DalApi;
using System.Xml.Linq;

namespace BLTest
{
    internal class Program
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        //BO.Student? boStude = s_bl.Student.Read(3);

        public static void EntityTask(char choice) 
        {
            switch (choice)
            {
                case '0':
                    break;

                case 'a'://add

                    Console.WriteLine("enter engineer's id to add");
                    int.TryParse(Console.ReadLine() ?? throw new Exception("enter a number"), out int _id);
                    Console.WriteLine("enter engineer's name");
                    string _name = Console.ReadLine()!;
                    Console.WriteLine("enter true/false if the engineer is active");
                    bool.TryParse(Console.ReadLine() ?? throw new Exception("enter a number"), out bool _isActive);
                    Console.WriteLine("enter engineer's email");
                    string _email = Console.ReadLine()!;
                    Console.WriteLine("enter engineer's cost");
                    double.TryParse(Console.ReadLine() ?? throw new Exception("enter a number"), out double _cost);
                    try
                    {
                        Console.WriteLine("enter engineer's level - press 0 for expert,1 for Junior,2 for Rookie");
                        int engineerLevel = int.Parse(Console.ReadLine()!);
                        if (!Enum.TryParse(engineerLevel.ToString(), out BO.EngineerExperience _level))
                            throw new BO.BlIllegalException("enter engineer's level - press 0 for expert,1 for Junior,2 for Rookie");

                        BO.Engineer newEngineer = new BO.Engineer()
                        {
                            Id = _id,
                            Name = _name,
                            Email = _email,
                            Level = _level,
                            Cost = _cost
                        };
                        s_bl!.Engineer!.Create(newEngineer);
                    }
                    catch (Exception ex)
                    {
                        throw new BO.BlCreatFailed("engineer's creation failed", ex);
                    }
                    break;
            }
        }
       

        static void Main(string[] args) 
        {
            Console.Write("Would you like to create Initial data? (Y/N)");
            string? ans = Console.ReadLine() ?? throw new FormatException("Wrong input");
            if (ans == "Y")
                DalTest.Initialization.Do();



        }
    }
}