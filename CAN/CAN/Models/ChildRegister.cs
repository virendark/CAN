using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
    public class ChildRegister
    {
        [PrimaryKey]
        public Guid ChildId { get; set; }

        // public string AadhaaId { get; set; }// extra

        public Guid FamilyId { get; set; }

        public string ChildName { get; set; }
        //public long LocationId { get; set; }// extra

        public DateTime DOB { get; set; }

        public int GenderID { get; set; }

        //public string SchoolName { get; set; } // extra
        public decimal BirthWeightInKg { get; set; }

        //public int BloodGroup { get; set; } // extra
        //public string AnyExistingDisease { get; set; } // extra
        public int BirthOrder { get; set; }  //Entry
        public DateTime RegisterDate { get; set; }

        public DateTime DOE { get; set; }

        public DateTime DOU { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int ChildStatus { get; set; }
        public decimal BirthLengthHeightInCms { get; set; }
        public int BirthPlace { get; set; } //dropdown
        public int BirthDeliveryType { get; set; }
        public int DeliveryTerm { get; set; }
        public decimal AWCEntryWeightInKG { get; set; } //doute
        public decimal AWCEntryHeightInCms { get; set; }
        public decimal AWCEntryMUAC { get; set; }
        public decimal AWCEntryW4AZ { get; set; }
        public decimal AWCEntryW4HZ { get; set; }
        public byte[] Photograph { get; set; }
        public string ChildCode { get; set; }
        public string AnyDisability { get; set; }
        public string AnyIllness { get; set; }
        public string AnyLongTermIllnessInFamily { get; set; }
        public int GrowthChartGrade { get; set; }
        public int GradeOfChild { get; set; }
    }
}
