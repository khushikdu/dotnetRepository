using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Utils
{
    /// <summary>
    /// Represents a book in the library system.
    /// </summary>
    internal class Book
    {
        public int BookId { get; }
        public string Title { get; }
        public string Author { get; }
        public string Genre { get; }
        public bool IsAvailable { get; set; } = true;
        public List<User> Issuees { get; } = new List<User>();
        public DateTime? IssueDate { get; private set; }
        public string Issuer { get; set; }

        // <summary>
        /// Initializes a new instance of the Book class with the specified ID, title, author, and genre.
        /// </summary>
        /// <param name="bookId">The ID of the book.</param>
        /// <param name="title">The title of the book.</param>
        /// <param name="author">The author of the book.</param>
        /// <param name="genre">The genre of the book.</param>
        public Book(int bookId, string title, string author,string genre)
        {
            this.BookId = bookId;
            this.Title = title;
            this.Author = author;
            this.Genre = genre;
        }

        /// <summary>
        /// Sets the issue date of the book.
        /// </summary>
        /// <param name="date">The issue date to set.</param>
        public void SetIssueDate(DateTime date)
        {
            IssueDate = date;
        }

        /// <summary>
        /// Resets the issue date of the book.
        /// </summary>
        public void ResetIssueDate()
        {
            IssueDate = null;
        }

        /// <summary>
        /// Displays information about the book.
        /// </summary>
        /// <param name="book">The book to display information about.</param>
        public void DisplayBookInfo(Book book)
        {
            Console.WriteLine($"{book.BookId}\t{book.Title,-30}\t{book.Author,-20}\t{book.Genre,-15}\t{(book.IsAvailable ? "Available" : "Not Available")}");

        }

        /// <summary>
        /// Displays the names of users who have issued the book.
        /// </summary>
        public void DisplayIssuees()
        {
            foreach (User user in Issuees)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
