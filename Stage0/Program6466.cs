namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6466();
            Welcome1671();

            Console.ReadKey();
        }

        static partial void Welcome1671();
       private static  void Welcome6466() 
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
    }
}