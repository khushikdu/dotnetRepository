using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Homework_4.Operations
{
    internal class DictionaryOpeartions
    {
        private Dictionary<int, double> dictionaryItems = new Dictionary<int, double>(){
            { 1, 1 },
            { 2,2 },
            { 3, 3 },
            { 4, 4 },
            { 5, 5 }
        };

        
        public void Operations() {
            ListOperations listOperations = new ListOperations();
            while (true)
            {
                listOperations.PrintMenu("Dictionary");
                Console.Write("Enter your choice : ");
                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    continue;
                }
                switch(choice)
                {
                    case 1:
                        AddElement();
                        break;
                    case 2:
                        PrintElements();
                        break;
                    case 3:
                        DeleteElement(0,"First");
                        break;
                    case 4:
                        int middleIndex=(dictionaryItems.Count%2==0)? dictionaryItems.Count / 2-1: dictionaryItems.Count / 2;
                        DeleteElement(middleIndex, "Middle");
                        break;
                    case 5:
                        DeleteElement(dictionaryItems.Count-1, "Last");
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
            Console.Write("Enter the key : ");
            if (int.TryParse(Console.ReadLine(), out int key))
            {
                Console.Write("Enter the value : ");
                if (double.TryParse(Console.ReadLine(), out double value))
                {
                    dictionaryItems[key] = value;
                    Console.WriteLine("Element added successfully.");
                }
                else
                {
                    Console.WriteLine("Invalid input.");
                }
            }
            else
            {
                Console.WriteLine("Invalid key. Please enter a valid integer key.");
            }
        }

        public void PrintElements()
        {
            Console.WriteLine("Dictionary Items");
            foreach(var item in dictionaryItems)
            {
                Console.WriteLine($"Key: {item.Key}, Value: {item.Value}");
            }
        }
        public void DeleteElement(int index, string position)
        {
            if (dictionaryItems.Count > 0)
            {
                if (index >= 0 && index < dictionaryItems.Count)
                {
                    int keyToRemove = dictionaryItems.ElementAt(index).Key;
                    dictionaryItems.Remove(keyToRemove);
                    Console.WriteLine($"{position} item deleted successfully");
                }
                else
                {
                    Console.WriteLine($"Invalid index. {position} element does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Dictionary is empty");
            }
        }
        public void CalculateAverage()
        {
            if (dictionaryItems.Count > 0)
            {
                double sum = 0;
                int count = 0;
                foreach (var value in dictionaryItems.Values)
                {
                    
                        sum += value;
                        count++;
                    
                }

                if (count > 0)
                {
                    double average = sum / count;
                    Console.WriteLine($"Average : {average}");
                }
                else
                {
                    Console.WriteLine("No valid integer values found in the dictionary.");
                }
            }
            else
            {
                Console.WriteLine("Dictionary is empty");
            }
        }

    }
}
