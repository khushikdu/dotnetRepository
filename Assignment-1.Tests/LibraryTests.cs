using Xunit;
using System;
using System.IO;
using System.Linq;
using Assignment_1.Enums;
using Assignment_1.Utils;

namespace Assignment_1.Tests
{
    /// <summary>
    /// Test class for testing the functionalities of the Library system.
    /// </summary>
    public class LibraryTests
    {
        private readonly Library _library;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assignment_1Tests"/> class.
        /// </summary>
        public LibraryTests()
        {
            _library = new Library();
        }

        /// <summary>
        /// Tests if a book can be issued to a user.
        /// </summary>
        [Fact]
        public void IssueBook_ShouldIssueBookToUser()
        {
            // Arrange
            var book = new Book(1, "Harry Potter", "JK Rowling", "Fantasy");
            _library.AddBooks(book);
            var user = new User(1, "Khushi", UserType.Student);
            _library.users.Add(user);
            var admin = new Admin(1, "Admin", "1234");
            _library.admins.Add(admin);

            // Act
            _library.IssueBook(book, user, admin);

            // Assert
            Assert.False(book.IsAvailable);
            Assert.Contains(book, user.BorrowedBooks);
            Assert.Contains(user, book.Issuees);
            Assert.Equal(admin.AdminName, book.Issuer);
        }

        /// <summary>
        /// Tests if issuing an unavailable book throws an exception.
        /// </summary>
        [Fact]
        public void IssueBook_ShouldNotIssueUnavailableBook()
        {
            // Arrange
            var book = new Book(1, "Harry Potter", "JK Rowling", "Fantasy");
            _library.AddBooks(book);
            var user1 = new User(1, "Harry", UserType.Student);
            var user2 = new User(2, "Hermione", UserType.Student);
            _library.users.Add(user1);
            _library.users.Add(user2);
            var admin = new Admin(1, "Admin", "1234");
            _library.admins.Add(admin);

            // Act
            _library.IssueBook(book, user1, admin);
            var exception = Assert.Throws<InvalidOperationException>(() => _library.IssueBook(book, user2, admin));

            // Assert
            Assert.False(book.IsAvailable);
            Assert.Contains(book, user1.BorrowedBooks);
            Assert.Contains(user1, book.Issuees);
            Assert.Equal(admin.AdminName, book.Issuer);
            Assert.Equal("The book is not available for borrowing.", exception.Message);
        }

        /// <summary>
        /// Tests if issuing a book to an ineligible user throws an exception.
        /// </summary>
        [Fact]
        public void IssueBook_ShouldNotIssueBookWhenUserIneligible()
        {
            // Arrange
            var book1 = new Book(1, "Harry Potter and The Prisoner Azkaban", "JK Rowling", "Fantasy");
            var book2 = new Book(2, "The Hobbit", "J.R.R. Tolkien", "Fantasy");
            var book3 = new Book(3, "1984", "George Orwell", "Dystopian");
            _library.AddBooks(book1);
            _library.AddBooks(book2);
            _library.AddBooks(book3);
            var user = new User(1, "Khushi", UserType.Student);
            _library.users.Add(user);
            var admin = new Admin(1, "Admin", "1234");
            _library.admins.Add(admin);

            // Issue 3 books to the user
            _library.IssueBook(book1, user, admin);
            _library.IssueBook(book2, user, admin);
            _library.IssueBook(book3, user, admin);

            var newBook = new Book(4, "The Catcher in the Rye", "J.D. Salinger", "Fiction");
            _library.AddBooks(newBook);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _library.IssueBook(newBook, user, admin));
            Assert.Equal("User is not eligible to issue more books.", exception.Message);
        }

        /// <summary>
        /// Tests if a book can be returned and the correct fine is calculated.
        /// </summary>
        [Fact]
        public void ReturnBook_ShouldReturnBookAndCalculateFine()
        {
            // Arrange
            var book = new Book(1, "Harry Potter and The Half Blood Prince", "JK Rowling", "Fantasy");
            _library.AddBooks(book);
            var user = new User(1, "Khushi", UserType.Student);
            _library.users.Add(user);
            var admin = new Admin(1, "Admin", "1234");
            _library.admins.Add(admin);
            _library.IssueBook(book, user, admin);

            // Simulate book issued 10 days ago
            book.SetIssueDate(DateTime.Now.AddDays(-10));

            // Act
            _library.ReturnBook(user, book);

            // Assert
            Assert.True(book.IsAvailable);
            Assert.DoesNotContain(book, user.BorrowedBooks);
            Assert.Equal(30, user.Fine); // Fine should be 30 for 3 extra days
        }

        /// <summary>
        /// Tests if returning a book not issued to the user throws an exception.
        /// </summary>
        [Fact]
        public void ReturnBook_ShouldNotReturnBookNotIssuedToUser()
        {
            // Arrange
            var book = new Book(1, "Harry Potter and The Chamber of Secrets", "JK Rowling", "Fantasy");
            _library.AddBooks(book);
            var user = new User(1, "Khushi", UserType.Student);
            var otherUser = new User(2, "Aarav", UserType.Student);
            _library.users.Add(user);
            _library.users.Add(otherUser);
            var admin = new Admin(1, "Admin", "1234");
            _library.admins.Add(admin);
            _library.IssueBook(book, user, admin);

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _library.ReturnBook(otherUser, book));
            Assert.Equal("The book was not issued by this user.", exception.Message);
        }

        /// <summary>
        /// Tests if the fine calculation is correct for overdue books.
        /// </summary>
        [Fact]
        public void CalculateFine_ShouldReturnCorrectFine()
        {
            // Arrange
            var book = new Book(1, "Harry Potter and the Philosopher's Stone", "JK Rowling", "Fantasy");
            _library.AddBooks(book);
            var user = new User(1, "Khushi", UserType.Student);
            _library.users.Add(user);
            var admin = new Admin(1, "Admin", "1234");
            _library.admins.Add(admin);
            _library.IssueBook(book, user, admin);

            // Simulate book issued 10 days ago
            book.SetIssueDate(DateTime.Now.AddDays(-10));

            // Act
            var fine = _library.CalculateFine(user);

            // Assert
            Assert.Equal(30, fine); // Fine should be 30 for 3 extra days
        }

        /// <summary>
        /// Tests if no fine is calculated for non-overdue books.
        /// </summary>
        [Fact]
        public void CalculateFine_ShouldReturnZeroIfNoOverdueBooks()
        {
            // Arrange
            var book = new Book(1, "Harry Potter and The Order of Phoenix", "JK Rowling", "Fantasy");
            _library.AddBooks(book);
            var user = new User(1, "Khushi", UserType.Student);
            _library.users.Add(user);
            var admin = new Admin(1, "Admin", "1234");
            _library.admins.Add(admin);
            _library.IssueBook(book, user, admin);

            // Simulate book issued 5 days ago
            book.SetIssueDate(DateTime.Now.AddDays(-5));

            // Act
            var fine = _library.CalculateFine(user);

            // Assert
            Assert.Equal(0, fine);
        }
    }
}