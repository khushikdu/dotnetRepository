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
    public class BookTests
    {
        private readonly Library _library;

        /// <summary>
        /// Initializes a new instance of the <see cref="Assignment_1Tests"/> class.
        /// </summary>
        public BookTests()
        {
            _library = new Library();
        }

        /// <summary>
        /// Tests if a new book can be added to the library.
        /// </summary>
        [Fact]
        public void AddBook_ShouldAddNewBook()
        {
            // Arrange
            var book = new Book(1, "Harry Potter and The Deathly Hallows", "JK Rowling", "Fantasy");

            // Act
            _library.AddBooks(book);

            // Assert
            var addedBook = _library.books.SingleOrDefault(b => b.BookId == book.BookId);
            Assert.NotNull(addedBook);
            Assert.Equal("Harry Potter and The Deathly Hallows", addedBook.Title);
            Assert.Equal("JK Rowling", addedBook.Author);
            Assert.Equal("Fantasy", addedBook.Genre);
        }

        /// <summary>
        /// Tests if a book can be removed from the library.
        /// </summary>
        [Fact]
        public void RemoveBook_ShouldRemoveBook()
        {
            // Arrange
            var book = new Book(1, "Harry Potter and The Goblet of Fire", "JK Rowling", "Fantasy");
            _library.AddBooks(book);

            // Act
            _library.RemoveBooks(book);

            // Assert
            var removedBook = _library.books.SingleOrDefault(b => b.BookId == book.BookId);
            Assert.Null(removedBook);
        }
    }
}