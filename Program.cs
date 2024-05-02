using Assignment_1.Enums;
using Assignment_1.Utils;
using static System.Reflection.Metadata.BlobBuilder;

/// <summary>
/// Represents the main program for managing a library system.
/// </summary>
class Program
{
    /// <summary>
    /// The entry point of the application.
    /// </summary>
    /// <param name="args">The command-line arguments.</param>
    static void Main(string[] args)
    {
        Library library = new Library();

        Admin defaultAdmin = new Admin(1, "khushi", "1234");
        library.admins.Add(defaultAdmin);
        Admin currentAdmin = library.admins[0];

        Book book1 = new Book(1, "The Great Gatsby", "F. Scott Fitzgerald", "Fiction");
        Book book2 = new Book(2, "To Kill a Mockingbird", "Harper Lee", "Fiction");
        Book book3 = new Book(3, "1984", "George Orwell", "Science Fiction");
        Book book4 = new Book(4, "Harry Potter and TPA", "J.K. Rowling", "Fantasy");
        Book book5 = new Book(5, "The Hobbit", "J.R.R. Tolkien", "Fantasy");

        library.AddBooks(book1);
        library.AddBooks(book2);
        library.AddBooks(book3);
        library.AddBooks(book4);
        library.AddBooks(book5);

        User user1 = new User(1, "Khushi", UserType.Student);
        User user2 = new User(2, "Prof. K", UserType.Teacher);
        library.users.Add(user1);
        library.users.Add(user2);

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nMenu:\n" +
                  "1. Add a new User\n"+
                  "2. Issue a book\n" +
                  "3. Return a book\n" +
                  "4. Details of a book\n" +
                  "5. List of Issuees for a book\n" +
                  "6. Display fine for a User\n" +
                  "7. Display all books issued by a User\n" +
                  "8. Display all books in the library\n" +
                  "9. Add a new admin\n" +
                  "10. Add a new book to the library\n" +
                  "11. Remove a book from the library\n" +
                  "12. Change current admin\n" +
                  "13. Print Logs\n" +
                  "14. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter user ID: ");
                    int userId = int.Parse(Console.ReadLine());
                    User user = library.users.Find(u => u.UserId == userId);
                    if(user!=null)
                    {
                        Console.WriteLine($"User with id {userId} already exists.");
                    }
                    else if (user == null)
                    {
                        AddUser(library, userId);
                    }
                    break;
                case "2":
                    Console.Write("Enter user ID: ");
                    userId = int.Parse(Console.ReadLine());

                    user = library.users.Find(u => u.UserId == userId);
                    if (user == null)
                    {
                        Console.WriteLine("User not found. Please enter user details:");
                        AddUser(library,userId);
                    }
                    Book book = FindBook(library);
                    if (book != null)
                    {
                        ValidateAdmin(library);
                        library.IssueBook(book, user, currentAdmin);
                    }
                    break;
                case "3":
                    user = findUser(library);
                    book = FindBook(library);
                    if (book != null)
                        library.ReturnBook(user, book);
                    break;
                case "4":
                    book = FindBook(library);
                    if (book != null)
                    {
                        Console.WriteLine($"Book ID\tTitle\t\t\t\tAuthor\t\t\tGenre\t\tAvailability");
                        book.DisplayBookInfo(book);
                    }
                    break;
                case "5":
                    book = FindBook(library);
                    if (book != null)
                        book.DisplayIssuees();
                    break;
                case "6":
                    user = findUser(library);
                    if (user != null)
                    {
                        library.DisplayFine(user);
                    }
                    break;
                case "7":
                    user = findUser(library);
                    Console.WriteLine($"Books issued by {user.Name}");
                    if (user != null)
                    {
                        if (user.BorrowedBooks.Count == 0)
                        {
                            Console.WriteLine($"No active Issues by {user.Name}");
                            break;
                        }
                        foreach (Book books in user.BorrowedBooks)
                        {
                            Console.WriteLine($"{books.BookId}\t{books.Title}\tIssued by: {books.Issuer}");
                        }
                    }
                    break;
                case "8":
                    ValidateAdmin(library);
                    Console.WriteLine("Books in the library : ");
                    Console.WriteLine($"Book ID\tTitle\t\t\t\tAuthor\t\t\tGenre\t\tAvailability");
                    foreach (Book books in library.books)
                    {
                        books.DisplayBookInfo(books);
                    }
                    break;
                case "9":
                    ValidateAdmin(library);
                    library.AddAdmin(library);
                    break;

                case "10":
                    ValidateAdmin(library);
                    Console.WriteLine("Adding a new book to the library:");
                    int newBookId = library.books.Count + 1;

                    Console.Write("Enter book title: ");
                    string newBookTitle = Console.ReadLine();

                    Console.Write("Enter book author: ");
                    string newBookAuthor = Console.ReadLine();

                    Console.Write("Enter book genre: ");
                    string newBookGenre = Console.ReadLine();

                    Book newBook = new Book(newBookId, newBookTitle, newBookAuthor, newBookGenre);
                    library.AddBooks(newBook);
                    Console.WriteLine("New book added to the library successfully.");
                    break;
                case "11":
                    ValidateAdmin(library);
                    Console.Write("Enter book ID to remove: ");
                    int removeBookId = int.Parse(Console.ReadLine());

                    Book removeBook = library.books.Find(b => b.BookId == removeBookId);
                    if (removeBook != null)
                    {
                        library.RemoveBooks(removeBook);
                        Console.WriteLine($"Book '{removeBook.Title}' removed successfully from the library.");
                    }
                    else
                    {
                        Console.WriteLine("Book not found.");
                    }
                    break; 
                case "12":
                    ValidateAdmin(library);
                    currentAdmin=library.ChangeAdmin(library);
                    break;
                case "13":
                    library.logManager.PrintLogs();
                    break;
                case "14":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    /// <summary>
    /// Validates the current admin's password.
    /// </summary>
    /// <param name="library">The library instance.</param>
    static void ValidateAdmin(Library library)
    {
        bool isLoggedIn = false;
        Admin currentAdmin = null;

        while (!isLoggedIn)
        {
            Console.Write("Enter current admin password: ");
            string password = Console.ReadLine();

            foreach (Admin admin in library.admins)
            {
                if (admin.AdminPassword == password)
                {
                    isLoggedIn = true;
                    currentAdmin = admin;
                    break;
                }
            }

            if (!isLoggedIn)
            {
                Console.WriteLine("Invalid password. Try again.");
            }
        }
    }

    /// <summary>
    /// Finds a user by their ID.
    /// </summary>
    /// <param name="library">The library instance.</param>
    /// <returns>The found user or null if not found.</returns>
    static User findUser(Library library)
    {
        Console.Write("Enter user ID: ");
        int userId = int.Parse(Console.ReadLine());

        User user = library.users.Find(u => u.UserId == userId);
        if (user == null)
        {
            Console.WriteLine("User not found.");
        }
        return user;
    }

    /// <summary>
    /// Finds a book by its ID.
    /// </summary>
    /// <param name="library">The library instance.</param>
    /// <returns>The found book or null if not found.</returns>
    static Book FindBook(Library library)
    {
        Console.Write("Enter book ID: ");
        int bookId = int.Parse(Console.ReadLine());

        Book book = library.books.Find(b => b.BookId == bookId);
        if (book == null)
        {
            Console.WriteLine("Book not found.");
        }
        return book;
    }

    /// <summary>
    /// Adds a new user to the library.
    /// </summary>
    /// <param name="library">The library instance.</param>
    /// <param name="userId">The ID of the user to add.</param>
    static void AddUser(Library library, int userId)
    {
        string userName;
        do
        {
            Console.Write("Enter user name: ");
            userName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("Name cannot be empty. Please enter a valid name.");
            }
        } while (string.IsNullOrWhiteSpace(userName));

        UserType userType;
        do
        {
            Console.WriteLine("Enter user type (Student/Teacher): ");
            string userTypeStr = Console.ReadLine();

            if (!Enum.TryParse(userTypeStr, true, out userType))
            {
                Console.WriteLine("Invalid user type. Please enter either 'Student' or 'Teacher'.");
            }
        } while (!Enum.IsDefined(typeof(UserType), userType));

        User user = new User(userId, userName, userType);
        library.users.Add(user);
    }
}