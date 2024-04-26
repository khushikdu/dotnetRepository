using System;

namespace Homework2
{
    class Person
    {
        private string _name;
        private int _age;

        public Person()
        {
            _name = "Unknown";
            _age = 0;
        }

        public Person(string name, int age)
        {
            this._name = name;
            this._age = age;
        }

        ~Person()
        {
            Console.WriteLine("Object Destroyed successfully");
        }

        public void Display()
        {
            Console.WriteLine($"Hello! My name is {_name} and I am {_age} years old.");
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            Person person = new Person();
            person.Display();

            person = new Person("Sanskar", 18);
            person.Display();
            Console.ReadKey();

        }
    }
}