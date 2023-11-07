namespace DalTest;
using DalApi;
using DO;

public static class Initialization
{
    private static ITask? s_dalTask;
    private static IEngineer? s_dalEngineer;
    private static IDependency? s_dalDependency;
    private static readonly Random s_rand = new();

    private static void createTasks()
    {

    }
    private static void createEngineers()
    {
        int _min_id = 200000000, _max_id= 400000000;
        
        (string,string)[] engineers =
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

        for (int i = 0; i < 4; i++)
        {
            foreach (var engineer in engineers)
            {
                int _id;
                do
                    _id = s_rand.Next(_min_id, _max_id);
                while (s_dalEngineer!.Read(_id) != null);

                string _name,_email;
                _name = engineer.Item1;
                _email= engineer.Item2;

                EngineerExperience _level;
                int _rand_level = s_rand.Next(0, Enum.GetNames<EngineerExperience>().Count());
                _level = (EngineerExperience)_rand_level;



                //  Engineer newEng = new(_id, _name, _email, _level, _cost);
            }
        }
       
    }
    private static void createDependencies()
    {

    }
}
