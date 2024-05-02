using Assignment_1.Enums;
using System.Collections.Generic;

namespace Assignment_1.Utils
{
    /// <summary>
    /// Represents a user in the library system.
    /// </summary>
    internal class User
    {
        /// <summary>
        /// Gets the unique identifier of the user.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Gets the name of the user.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the type of the user (Student or Teacher).
        /// </summary>
        public UserType Type { get; }

        /// <summary>
        /// Gets the list of books borrowed by the user.
        /// </summary>
        public List<Book> BorrowedBooks { get; } = new List<Book>();

        /// <summary>
        /// Gets or sets the fine amount imposed on the user.
        /// </summary>
        public double Fine { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <param name="name">The name of the user.</param>
        /// <param name="type">The type of the user (Student or Teacher).</param>
        public User(int userId, string name, UserType type)
        {
            this.UserId = userId;
            this.Name = name;
            this.Type = type;
        }

        /// <summary>
        /// Adds a fine amount to the user's total fine.
        /// </summary>
        /// <param name="fine">The amount to add to the fine.</param>
        public void AddFine(double fine)
        {
            Fine += fine;
        }
    }
}
