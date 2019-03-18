using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
  public  class TblGrowthRegisterMother
    {
        public Guid GrowthId { get; set; }
        public Guid FamilyId { get; set; }
        public int DataMonthId { get; set; }
        public bool ChildExpected { get; set; }
        public DateTime? ExpectedDeliveryDate { get; set; }
        public bool IsFirstPregnancy { get; set; } // new
        public bool IsLactating { get; set; }
        public DateTime? LastDeliveryDate { get; set; }//  public DateTime? LastDeliveryDate { get; set; }
        public bool AwcregistrationDate { get; set; }// public DateTime? AwcregistrationDate { get; set; }
        public int AncregistraionDate { get; set; }//
        public bool ReceivedIfatablets { get; set; }
        public bool ReceivedCalciumTablets { get; set; }
        public bool ConsumedIfatablets { get; set; }
        public decimal MotherWeightIn1Trimester { get; set; }
        public decimal MotherWeightIn2Trimester { get; set; }
        public decimal MotherWeightInDeliveryTime { get; set; }
        public DateTime DateOfMeasurement { get; set; }
        public bool IsGettingTreatment { get; set; }
        public int GettingTreatmentFrom { get; set; }
        public bool IsUnderMedication { get; set; }
        public bool ReceivedDietUnderAay { get; set; }
        public bool ReceivedMealFromAwcunderAay { get; set; }
        public int HowManyReceivedMealFromAwcunderAay { get; set; }
        public int TypeOfMeal { get; set; }
        public int EggReceived { get; set; }
        public bool ReceivedSnacksUnderAay { get; set; }
        public int ReceivedSnacksFromAwc { get; set; }
        public int EatMealRegularlyFromAwc { get; set; }
        public int IsMealEnough { get; set; }
        public int QualityOfAwcfood { get; set; }
        public int ResonForNotEatingAwcmeal { get; set; }
        public DateTime DOE { get; set; }
        public DateTime DOU { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public string TotalPregnancyMonths { get; set; }
        public string HighRiskMotherHistory { get; set; }
        public string ANCCheckups { get; set; }
        public string ANMMarkHighRiskScreening { get; set; }
        public bool GetMealAwcUnderAay { get; set; }
        // public decimal AncregistrationMotherWeightInKg { get; set; }
        //public bool CheckupDoneInFirstTrimester { get; set; }
        //public int HeightMeasured { get; set; }
        //public int WeightMeasured { get; set; }
        //public int Bpmeasured { get; set; }
        //public int Hbmeasured { get; set; }
        //public int Ttgiven { get; set; }
        //public int AbdomenalCheckup { get; set; }
        //public int SonographyDone { get; set; }
        //public bool IsHighRisk { get; set; }
        //public int HighBp { get; set; }
        //public int EarlierCesarianDelivery { get; set; }
        //public int AbnormalWeightOrIncreaseInWeight { get; set; }
        //public int AbnormalHeightLessThan4Ft10Inches { get; set; }
        //public int EarlierAbortionStillBirthInfacntDeath { get; set; }
        //public int HasDiabetes { get; set; }
        //public int TwinsOrFrequentPregnancy { get; set; }
        //public int PregnancyAfterLongTreatment { get; set; }
        //public int FirstDeliveryAfterAge35 { get; set; }
        //public int LowHb { get; set; }
        //public int CriticalDisease { get; set; }
        //public int PreEclampsiaSymptoms { get; set; }
        //public int FrequentVomitAfter4Months { get; set; }

        //public decimal WeightInKgDuringRegistration { get; set; }


    }
}
