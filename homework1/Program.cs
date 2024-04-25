using System;

namespace Homework1
{
    class BankAccount
    {
        private int _balance;

        public BankAccount()
        {
            _balance = 0;
        }

        public void Deposit(int amount)
        {
            _balance += amount;
        }

        public void Withdraw(int amount)
        {
            if (amount <= _balance)
            {
                _balance -= amount;
            }
            else
            {
                Console.WriteLine($"\nInsufficient Balance.");
            }
        }

        public void Display()
        {
            Console.WriteLine($"\nBalance : Rs. {_balance}");
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            BankAccount bankAccount = new BankAccount();
            int choice = -1,
                amount;
            do
            {
                Console.Write(
                    $"\n1. Deposit \n2. Withdraw \n3. Display Amount \n4. Exit \nEnter you choice : "
                );
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Enter deposit amount: ");
                        amount = int.Parse(Console.ReadLine());
                        bankAccount.Deposit(amount);
                        bankAccount.Display();
                        break;
                    case 2:
                        Console.Write("Enter withdraw amount: ");
                        amount = int.Parse(Console.ReadLine());
                        bankAccount.Withdraw(amount);
                        bankAccount.Display();
                        break;
                    case 3:
                        bankAccount.Display();
                        break;
                    case 4:
                        return;
                    default:
                        Console.Write("Invalid Choice");
                        break;
                }
            } while (choice != 4);
        }
    }
}
