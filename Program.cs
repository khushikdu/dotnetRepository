using Assignment_1.Enums;
using Assignment_1.Utils;

class Program
{
    static void Main(string[] args)
    {
        Library library = new Library();

        Admin admin1 = new Admin(1, "khushi", "1234");
        library.admins.Add(admin1);
        Admin currentAdmin = library.admins[0];

        Book book1 = new Book(1, "The Great Gatsby", "F. Scott Fitzgerald", "Fiction");
        Book book2 = new Book(2, "To Kill a Mockingbird", "Harper Lee", "Fiction");
        Book book3 = new Book(3, "1984", "George Orwell", "Science Fiction");
        Book book4 = new Book(4, "Harry Potter and the Prisnor of Azkaban", "J.K. Rowling", "Fantasy");
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
            Console.WriteLine("\nMenu:");
            Console.WriteLine("1. Issue a book");
            Console.WriteLine("2. Return a book");
            Console.WriteLine("3. Display fine for a User");
            Console.WriteLine("4. Display all books issued by a User");
            Console.WriteLine("5. Display all books in the library");
            Console.WriteLine("6. Add a new admin");
            Console.WriteLine("7. Add a new book to the library");
            Console.WriteLine("8. Change current admin");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ValidateAdmin(library);
                    Console.Write("Enter user ID: ");
                    int userId = int.Parse(Console.ReadLine());

                    User user = library.users.Find(u => u.UserId == userId);
                    if (user == null)
                    {
                        Console.WriteLine("User not found. Please enter user details:");

                        Console.Write("Enter user name: ");
                        string userName = Console.ReadLine();

                        Console.WriteLine("Enter user type (Student/Teacher): ");
                        string userTypeStr = Console.ReadLine();
                        UserType userType;
                        Enum.TryParse(userTypeStr, true, out userType);

                        user = new User(userId, userName, userType);
                        library.users.Add(user);
                    }

                    Console.Write("Enter book ID: ");
                    int bookId = int.Parse(Console.ReadLine());

                    Book book = library.books.Find(b => b.BookId == bookId);
                    if (book == null)
                    {
                        Console.WriteLine("Book not found.");
                        break;
                    }

                    library.IssueBook(book, user, currentAdmin);
                    break;
                case "2":
                    Console.Write("Enter user ID: ");
                    userId = int.Parse(Console.ReadLine());

                    user = library.users.Find(u => u.UserId == userId);
                    if (user == null)
                    {
                        Console.WriteLine("User not found. Please enter user details:");
                    }
                    Console.Write("Enter book ID: ");
                    bookId = int.Parse(Console.ReadLine());

                    book = library.books.Find(b => b.BookId == bookId);
                    if (book == null)
                    {
                        Console.WriteLine("Book not found.");
                        break;
                    }
                    library.ReturnBook(user, book);
                    break;
                case "3":            

                    break;
                case "4":           

                    break;
                case "5": DisplayBooks(library);
                    break;
                case "6":         

                    break;
                case "7":           

                    break;
                case "8":     

                    break;
                case "9":
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
    static void DisplayBooks(Library library)
    {
        foreach(Book book in library.books)
        {
            Console.WriteLine($"Book ID: {book.BookId}, Title: {book.Title}, Author: {book.Author}, Genre: {book.Genre}, Availability: {(book.IsAvailable ? "Available" : "Not Available")}");
        }
    }
    static void ValidateAdmin(Library library)
    {
        bool isLoggedIn = false;
        Admin currentAdmin = null;

        while (!isLoggedIn)
        {
            Console.Write("Enter admin password: ");
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
}
