using System;

namespace Homework2
{
    /// <summary>
    /// Represents a person with a name and age.
    /// </summary>
    class Person
    {
        private string _name;
        private int _age;

        /// <summary>
        /// Default constructor to initialize a person with default values.
        /// </summary>
        public Person()
        {
            _name = "Unknown";
            _age = 0;
        }

        /// <summary>
        /// Parameterized constructor to initialize a person with specified name and age.
        /// </summary>
        /// <param name="name">The name of the person.</param>
        /// <param name="age">The age of the person.</param>
        public Person(string name, int age)
        {
            this._name = name;
            this._age = age;
        }

        /// <summary>
        /// Destructor to perform cleanup when the object is destroyed.
        /// </summary>
        ~Person()
        {
            Console.WriteLine("Object Destroyed successfully");
        }

        /// <summary>
        /// Displays information about the person.
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"Hello! My name is {_name} and I am {_age} years old.");
        }
    }

    /// <summary>
    /// Main class to demonstrate the Person class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        public static void Main(string[] args)
        {
            Person person = new Person();
            person.Display();

            person = new Person("Khushi", 23);
            person.Display();
            Console.ReadKey();

        }
    }
}