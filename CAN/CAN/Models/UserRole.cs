using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
    public class UserRole
    {
        [PrimaryKey]
        public int UserID { get; set; }
        public int RoleID { get; set; }

    }
}
