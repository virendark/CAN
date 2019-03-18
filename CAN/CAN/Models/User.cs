using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
    public class User
    {
        [PrimaryKey]
        public int UserId { get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        [MaxLength(50)]
        public string Password { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Email { get; set; }
        [MaxLength(100)]
        public string Location { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string Token { get; set; }
    }
}
