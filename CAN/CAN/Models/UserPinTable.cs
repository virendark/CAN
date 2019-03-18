using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
   public class UserPinTable
    {
        [PrimaryKey]
        public int UserId { get; set; }
        [MaxLength(100)]
        public int SetPin { get; set; }
       
    }
}
