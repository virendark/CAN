using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
  public  class Location
    {
        [PrimaryKey]
        public int locationId { get; set; }
        public string locationName { get; set; }
        public int locationTypeId { get; set; }
        public int? parentId { get; set; }
        
        public string locationType { get; set; }
        public string DCType { get; set; }
    }
}
