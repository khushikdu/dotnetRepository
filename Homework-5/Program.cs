using System;
using System.Collections.Generic;
using System.Linq;

namespace Homework_5
{
    internal class Program
    {
        /// <summary>
        /// Main method to execute the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            List<int> originalList = Enumerable.Range(1, 10).ToList();

            List<int> squaredList = originalList.Select(x => x * x).ToList();

            List<int> evenNumbersList = originalList.Where(x => x % 2 == 0).ToList();

            int sumOfEvenNumbers = evenNumbersList.Sum();
            do
            {
                Console.WriteLine("\nChoose an option:");
                Console.WriteLine("1. Print Original List");
                Console.WriteLine("2. Print Squared List");
                Console.WriteLine("3. Print Filtered List (Even Numbers Only)");
                Console.WriteLine("4. Print Sum of Elements in Filtered List");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice : ");
                int choice;
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("\nOriginal List:");
                        PrintList(originalList);
                        break;
                    case 2:
                        Console.WriteLine("\nSquared List:");
                        PrintList(squaredList);
                        break;
                    case 3:
                        Console.WriteLine("\nFiltered List (Even Numbers Only):");
                        PrintList(evenNumbersList);
                        break;
                    case 4:
                        Console.WriteLine("\nSum of Elements in Filtered List: " + sumOfEvenNumbers);
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose again.");
                        break;
                }
            } while (true);

        }

        /// <summary>
        /// Method to print list elements.
        /// </summary>
        /// <param name="list">The list to be printed.</param>
        static void PrintList(List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write(item + " ");
            }
            Console.WriteLine();
        }
    }
}