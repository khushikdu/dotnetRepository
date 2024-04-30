using Assignment_1.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Utils
{
    internal class User
    {
        public int UserId { get; }
        public string Name { get; }
        public UserType Type { get; }
        public List<Book> BorrowedBooks { get; } = new List<Book>();
        public double Fine { get; private set; }
        //public string Issuer { get; set; }

        public User(int userId, string name, UserType type)
        {
            this.UserId = userId;
            this.Name = name;
            this.Type = type;
        }

        public void AddFine(double fine)
        {
            Fine += fine;
        }
    }

}
