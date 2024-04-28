using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_1.Utils
{
    internal class Admin
    {
        public int AdminId { get; }
        public string AdminName { get; }
        public string AdminPassword { get; }
        public Admin(int adminId, string adminName, string adminPassword)
        {
            this.AdminId = adminId;
            this.AdminName = adminName;
            this.AdminPassword = adminPassword;
        }
    }
}
