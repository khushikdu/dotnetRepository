using Assignment_1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Utils
{
    /// <summary>
    /// Represents a library management system.
    /// </summary>
    internal class Library
    {
        public LogManager logManager = new LogManager();
        public List<Book> books { get; set; } =new List<Book>();
        public List<Admin> admins { get; set; } = new List<Admin>();
        public List<User> users { get; set; } = new List<User>();

        /// <summary>
        /// Adds a book to the library.
        /// </summary>
        /// <param name="book">The book to add.</param>
        public void AddBooks(Book book)
        {
            books.Add(book);
            LogEntry logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Action = "Book Added",
                BookTitle = book.Title
            };
            logManager.AddLogEntry(logEntry);
        }

        /// <summary>
        /// Removes a book from the library.
        /// </summary>
        /// <param name="book">The book to remove.</param>
        public void RemoveBooks(Book book)
        {
            books.Remove(book);
            LogEntry logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Action = "Book Removed",
                BookTitle = book.Title
            };
            logManager.AddLogEntry(logEntry);
        }

        /// <summary>
        /// Checks if a user is eligible to issue a book based on their type and the number of books they have borrowed.
        /// </summary>
        /// <param name="user">The user to check eligibility for.</param>
        /// <returns>True if the user is eligible; otherwise, false.</returns>
        private bool IsEligibleToIssue(User user)
        {
            if (user.Type == UserType.Student && user.BorrowedBooks.Count >= 3)
            {
                Console.WriteLine("Students can issue 3 books at a time.");
                return false;
            }

            if (user.Type == UserType.Teacher && user.BorrowedBooks.Count >= 10)
            {
                Console.WriteLine("Teachers can issue 10 books at a time.");
                return false;
            }
            return true;
        }

        /// <summary>
        /// Issues a book to a user.
        /// </summary>
        /// <param name="book">The book to issue.</param>
        /// <param name="user">The user to whom the book is issued.</param>
        /// <param name="admin">The admin issuing the book.</param>
        public void IssueBook(Book book,User user,Admin admin)
        {
            if(!IsEligibleToIssue(user))
            {
                return;
            }

            if (!book.IsAvailable)
            {
                Console.WriteLine("The book is not available for borrowing.");
                return;
            }

            book.IsAvailable = false;
            book.Issuees.Add(user);
            book.SetIssueDate(DateTime.Now); 
            user.BorrowedBooks.Add(book);
            book.Issuer = admin.AdminName;
            Console.WriteLine($"Book '{book.Title}' issued to {user.Name} successfully.\nIssuer: {book.Issuer}.");

            LogEntry logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Action = "Book Issued",
                UserName = user.Name,
                BookTitle = book.Title
            };
            logManager.AddLogEntry(logEntry);
        }

        /// <summary>
        /// Returns a book borrowed by a user.
        /// </summary>
        /// <param name="user">The user returning the book.</param>
        /// <param name="book">The book being returned.</param>
        public void ReturnBook(User user,Book book) 
        {
            double fine = 0;

            if (!book.Issuees.Contains(user))
            {
                Console.WriteLine("The book was not issued by this user.");
                return ;
            }
            book.IsAvailable = true;
            user.BorrowedBooks.Remove(book);
            TimeSpan duration = DateTime.Now - book.IssueDate.Value; 
            if (duration.Days > 7)
            {
                fine += (duration.Days - 7) * 10;
                user.AddFine(fine);
                Console.WriteLine($"Book '{book.Title}' was returned late, hence a amount of Rs. {fine} is imposed as fine.");
            }
            book.ResetIssueDate();
            Console.WriteLine($"Book '{book.Title}' returned by {user.Name} successfully.");
            LogEntry logEntry = new LogEntry
            {
                Timestamp = DateTime.Now,
                Action = "Book Returned",
                UserName = user.Name,
                BookTitle = book.Title
            };
            logManager.AddLogEntry(logEntry);
        }

        /// <summary>
        /// Calculates the total fine imposed on a user for overdue books.
        /// </summary>
        /// <param name="user">The user for whom to calculate the fine.</param>
        /// <returns>The total fine amount.</returns>
        public double CalculateFine(User user)
        {
            double fine = 0;
            foreach (Book book in user.BorrowedBooks)
            {
                TimeSpan duration = DateTime.Now - book.IssueDate.Value;
                if (duration.Days > 7)
                {
                    fine += (duration.Days - 7) * 10;
                }
            }
            user.AddFine(fine);
            return fine;
        }

        /// <summary>
        /// Displays the total fine imposed on a user.
        /// </summary>
        /// <param name="user">The user for whom to display the fine.</param>
        public void DisplayFine(User user)
        {
            Console.WriteLine($"Total fine imposed on {user.Name}: {user.Fine}");
        }

        /// <summary>
        /// Lists the books borrowed by a user.
        /// </summary>
        /// <param name="user">The user whose borrowed books are to be listed.</param>
        public void ListBooksBorrowedByUser(User user)
        {
            Console.WriteLine($"Books borrowed by {user.Name}:");
            foreach (Book book in user.BorrowedBooks)
            {
                Console.WriteLine($"{book.Title} by {book.Author}");
            }
        }

        /// <summary>
        /// Adds a new admin to the library.
        /// </summary>
        /// <param name="library">The library to which the admin is added.</param>
        public void AddAdmin(Library library)
        {
            Console.WriteLine("Adding a new admin:");
            Console.Write("Enter admin name: ");
            string adminName = Console.ReadLine();
            if (library.admins.Any(admin => admin.AdminName == adminName))
            {
                Console.WriteLine("Admin with the same name already exists.");
            }
            else
            {
                Console.Write("Enter admin password: ");
                string adminPassword = Console.ReadLine();
                int adminId = library.admins.Count + 1;
                Admin newAdmin = new Admin(adminId, adminName, adminPassword);
                library.admins.Add(newAdmin);
                Console.WriteLine("New admin added successfully.");
                LogEntry logEntry = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Action = "Admin Added",
                    UserName = adminName
                };
                logManager.AddLogEntry(logEntry);
            }
        }

        /// <summary>
        /// Changes the current admin for the library.
        /// </summary>
        /// <param name="library">The library for which to change the admin.</param>
        /// <returns>The new admin if successfully changed; otherwise, null.</returns>
        public Admin ChangeAdmin(Library library)
        {
            bool isLoggedIn = false;
            Admin currentAdmin = null;
            while (!isLoggedIn)
            {
                Console.Write("Enter \"exit\" to leave.\nEnter admin name: ");
                string name = Console.ReadLine();

                if (name.Equals("exit"))
                {
                    break;
                }

                Console.Write("Enter admin password: ");
                string password = Console.ReadLine();

                foreach (Admin admin in library.admins)
                {
                    if (admin.AdminName == name && admin.AdminPassword == password)
                    {
                        isLoggedIn = true;
                        currentAdmin = admin;
                        break;
                    }
                }

                if (!isLoggedIn)
                {
                    Console.WriteLine("Invalid admin name or password. Try again.");
                }
            }

            if (currentAdmin != null)
            {
                Console.WriteLine($"{currentAdmin.AdminName} logged in successfully.");
                LogEntry logEntry = new LogEntry
                {
                    Timestamp = DateTime.Now,
                    Action = "Admin Changed",
                    UserName = currentAdmin.AdminName
                };
                logManager.AddLogEntry(logEntry);
            }
            else
            {
                Console.WriteLine("Failed to log in as admin.");
            }
            return currentAdmin;
        }
    }
}
