using System;

namespace Homework1
{
    /// <summary>
    /// Represents a bank account with a balance.
    /// </summary>
    class BankAccount
    {
        private int _balance;

        /// <summary>
        /// Initializes a new instance of the BankAccount class with a zero balance.
        /// </summary>
        public BankAccount()
        {
            _balance = 0;
        }

        /// <summary>
        /// Deposits the specified amount into the account.
        /// </summary>
        /// <param name="amount">The amount to deposit.</param>
        public void Deposit(int amount)
        {
            _balance += amount;
        }

        /// <summary>
        /// Withdraws the specified amount from the account if sufficient balance is available.
        /// </summary>
        /// <param name="amount">The amount to withdraw.</param>
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

        /// <summary>
        /// Displays the current balance of the account.
        /// </summary>
        public void Display()
        {
            Console.WriteLine($"\nBalance : Rs. {_balance}");
        }
    }

    /// <summary>
    /// Main class to demonstrate the BankAccount class.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
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
