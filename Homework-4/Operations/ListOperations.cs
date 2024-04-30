using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_4.Operations
{
    internal class ListOperations
    {
        private List<int> listItems = new List<int>();
        public void PrintMenu(String collection)
        {
            Console.WriteLine($"\nPlease enter a number from the given options :");
            Console.WriteLine($"1. Add an element to the {collection}.");
            Console.WriteLine($"2. Print all the elements present in the {collection}.");
            Console.WriteLine($"3. Delete the first element from the {collection}.");
            Console.WriteLine($"4. Delete the middle element from the {collection}.");
            Console.WriteLine($"5. Delete the last element from the {collection}.");
            Console.WriteLine($"6. Calculate the average of the elements present in the {collection}.");
            Console.WriteLine($"7. Go back to the previous menu.");
            Console.WriteLine($"8. Exit the application.");
        }
        public void Operations()
        {
            while (true)
            {
                PrintMenu("List");
                Console.Write("Enter your choice : ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }
                switch (choice)
                {
                    case 1:
                        AddElement();
                        break;
                    case 2:
                        PrintElements();
                        break;
                    case 3:
                        DeleteElement(0, "First");
                        break;
                    case 4:
                        DeleteElement(listItems.Count / 2, "Middle");
                        break;
                    case 5:
                        DeleteElement(listItems.Count-1, "Last");
                        break;
                    case 6:
                        CalculateAverage();
                        break;
                    case 7:
                        return;
                        break;
                    case 8:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        break;
                }
            }
        }
        public void AddElement()
        {
            Console.Write("Enter the number to be added : ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                listItems.Add(value);
                Console.WriteLine("Item added successfully");
            }
            else
            {
                Console.WriteLine("Invalid Input");
            }
        }
        public void PrintElements()
        {
            Console.WriteLine("List Items");
            foreach (int items in listItems)
            {
                Console.WriteLine(items);
            }
        }
        public void DeleteElement(int index, string position)
        {
            if (listItems.Count > 0)
            {
                listItems.RemoveAt(index);
                Console.WriteLine($"{position} item deleted successfully");
            }
            else
            {
                Console.WriteLine("List is empty");
            }
        }
        public void CalculateAverage()
        {
            if (listItems.Count > 0)
            {
                double average = listItems.Average();
                Console.WriteLine($"Average : {average}");
            }
            else
            {
                Console.WriteLine("List is empty");
            }
        }
    }
}