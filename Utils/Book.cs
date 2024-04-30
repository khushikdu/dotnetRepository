using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Utils
{
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
        public Book(int bookId, string title, string author,string genre)
        {
            this.BookId = bookId;
            this.Title = title;
            this.Author = author;
            this.Genre = genre;
        }
        public void SetIssueDate(DateTime date)
        {
            IssueDate = date;
        }

        public void ResetIssueDate()
        {
            IssueDate = null;
        }
        public void DisplayBookInfo(Book book)
        {
            Console.WriteLine($"{book.BookId}\t{book.Title,-30}\t{book.Author,-20}\t{book.Genre,-15}\t{(book.IsAvailable ? "Available" : "Not Available")}");

        }
        public void DisplayIssuees()
        {
            foreach (User user in Issuees)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
