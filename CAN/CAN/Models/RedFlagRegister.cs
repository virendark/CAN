using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
  public   class RedFlagRegister
    {
        public Guid RedFlagId { get; set; }
        public Guid ChildId { get; set; }
        public int DateMonthId { get; set; }
        public int? ReferredToHealthCenter { get; set; } // dropdown
        public bool ? ChildGoneToHealthCenter { get; set; }
        public bool? ConsultancyProvidedToFamily { get; set; }
        public DateTime MeasurementDate { get; set; }
        public decimal? WeightInKg { get; set; }
        public decimal? LengthHeight { get; set; }
        public decimal? MUAC { get; set; }
        public bool ? SpecialDietProvided { get; set; }
        public int WasOnMedication { get; set; } //dropdown
        public string Observation { get; set; }
        public string Diagnose { get; set; }
        public int CurrentStatus { get; set; }
        public string AnyBlockerInLastDiagnose { get; set; }
        public DateTime ? ASHAVisitDate { get; set; }
        public int IsEatingSufficiently { get; set; } // dropdown
        public bool? IsEatingSufficientlyBool { get; set; }
        public bool HygeineMaintained { get; set; }
        public bool? AWWVisitWithASHA { get; set; }
        public string ChildSeverelyUnderweightSymptoms { get; set; } // change data type int to string (dropdown)
        public bool? CaseOfReferral { get; set; }
        public DateTime? DateOfReferral { get; set; }
        public int ReferradTo { get; set; } // dropdown
        public int ChildAdmittedInCTCNRC { get; set; }
        public DateTime? DateofAdmission { get; set; }
        public DateTime? DateOfDischarge { get; set; }
        public bool FirstFollowUp { get; set; }
        public bool SecondFollowUp { get; set; }
        public bool ThirdFollowUp { get; set; }
        public bool FourthFollowUp { get; set; }
        public int? AdvicedButNotAdmittedReason { get; set; } //dropdown
        public DateTime Doe { get; set; }
        public DateTime Dou { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsCurrentlyIll { get; set; }
        public bool IsExtraFeeding { get; set; }
        public string Remark { get; set; }
        public int OutcomeofReferralbyASHA { get; set; }
        public bool? IsSuggestedReferral { get; set; }
    }
}
