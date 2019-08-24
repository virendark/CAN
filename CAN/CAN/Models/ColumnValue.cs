using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
   public class ColumnValue
    {
        [PrimaryKey]
        public int columnValueId { get; set; }
        public int columnTypeId { get; set; }
        public string columnValue { get; set; }
        public string columnValueSecondary { get; set; }
        public int sequenceNo { get; set; }
    }
}
