using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Utils
{
    /// <summary>
    /// Represents an admin user in the system.
    /// </summary>
    internal class Admin
    {
        public int AdminId { get; }
        public string AdminName { get; }
        public string AdminPassword { get; }

        /// <summary>
        /// Initializes a new instance of the Admin class with the specified ID, name, and password.
        /// </summary>
        /// <param name="adminId">The admin ID.</param>
        /// <param name="adminName">The admin name.</param>
        /// <param name="adminPassword">The admin password.</param>
        public Admin(int adminId, string adminName, string adminPassword)
        {
            this.AdminId = adminId;
            this.AdminName = adminName;
            this.AdminPassword = adminPassword;
        }
    }
}
