using Homework_4.Operations;

namespace Homework_4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Collection Management Application!");

            while (true)
            {
                Console.WriteLine("\nPlease enter a number from the given menu:");
                Console.WriteLine("1. Continue the application using List.");
                Console.WriteLine("2. Continue the application using Dictionary.");
                Console.WriteLine("3. Exit the application.");
                Console.Write("Enter your choice : ");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ListOperations listOperations = new ListOperations();
                        listOperations.Operations();
                        break;
                    case 2:
                        DictionaryOpeartions dictionaryOpeartions = new DictionaryOpeartions();
                        dictionaryOpeartions.Operations();
                        break;
                    case 3:
                        Console.WriteLine("Exiting the application.");
                        return;
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        break;
                }
            }
        }
    }
}
