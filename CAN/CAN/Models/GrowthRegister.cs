using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
  public  class GrowthRegister
    {
        private DateTime _doe = new DateTime(2015, 01, 01);
        [PrimaryKey]
        public Guid GrowthId { get; set; }
        public Guid ChildId { get; set; }
        public int DataMonthId { get; set; }
        public DateTime MeasurementDate { get; set; }
        public decimal? WeightInKg { get; set; }
        public decimal? LengthHeight { get; set; }
        public decimal? MUAC { get; set; }
        public bool AnyRedFlag { get; set; }
        public int ReceiveTakeHomeRation { get; set; } // rename ReceiveTakeHomeRation
        public bool AdmittedToAWC { get; set; } // rename AdmittedToAWC
        public DateTime Doe
        {
            get
            {
                return _doe;
            }

            set
            {
                if (_doe == value) return;
                _doe = value;
            }
        }
        public DateTime Dou { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public double? W4LHZ { get; set; }
        public double? W4AZ { get; set; }
        public double? H4AZ { get; set; }
        public double? BMI { get; set; }
        public double? BMIZ { get; set; }
        public bool IsZScoreRedFlag { get; set; } // new
        public int AgeInDays { get; set; } //Entry
        public int GenderID { get; set; }
        public bool IsBreastfeeding { get; set; }
        public bool HealthCheckUpDone { get; set; }
        public string AnyDisability { get; set; }
        public string AnyIllness { get; set; }
        public int NumberofDaysIll { get; set; } // entery number
        public string TypeOfIllness { get; set; }
        public int ReasonAnthropometryNotTaken { get; set; } // dropdown 
        public int CurrentlyAttends { get; set; } // dropdown 
        public bool ReceiveCookedFood { get; set; }
        public int ReceiveAAY { get; set; } // dropdown 
        public string Remark { get; set; }
        public bool ChildIllPreviousMonth { get; set; }
        public string ImmunisationCard { get; set; }
        public int ReceiveAAYEggInDays { get; set; }
        public int  ReceiveAAYBananaInDays { get; set; }

    }
}
