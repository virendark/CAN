using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
  public  class ColumnType
    {
        [PrimaryKey]
        public int ColumnTypeId { get; set; }
        public string TypeName { get; set; }
    }
}
