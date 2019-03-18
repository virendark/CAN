using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
    public class UserToekn
    {
        [PrimaryKey]
        public int UserId { get; set; }
        [MaxLength(100)]
        public string Token { get; set; }
        [MaxLength(100)]
        public string TokenFor { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
