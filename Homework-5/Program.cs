namespace Homework_5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> numbers = Enumerable.Range(1, 10).ToList();

            List<int> squaredNumbers = numbers.Select(x => x * x).ToList();

            List<int> evenNumbers = numbers.Where(x => x % 2 == 0).ToList();

            int sumOfEvenNumbers = evenNumbers.Sum();

            Console.WriteLine("Original List:");
            PrintList(numbers);

            Console.WriteLine("\nSquared List:");
            PrintList(squaredNumbers);

            Console.WriteLine("\nFiltered List (Even Numbers Only):");
            PrintList(evenNumbers);

            Console.WriteLine($"\nSum of Even Numbers: {sumOfEvenNumbers}");
        }

        static void PrintList(List<int> list)
        {
            foreach (var item in list)
            {
                Console.Write($"{item} ");
            }
            Console.WriteLine();
        }
    
    }
}
