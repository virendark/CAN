using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.ViewModels
{
 public   class MotherWithChildDetails
    {
        public Guid GrowthId { get; set; }
        public Guid FamilyId { get; set; }

        public int FamilyType { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }

        public int LocationId { get; set; }
        public bool? ChildExpected { get; set; }
       // public DateTime? AwcregistrationDate { get; set; }
        public int status { get; set; }
        public int DataMonthId { get; set; }
        public string HighRiskMotherHistory { get; set; }
        public string FamilyCode { get; set; }
        public bool? IsLactating { get;set; }
    }
}
