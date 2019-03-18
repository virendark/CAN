using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
namespace CAN.Models
{
    public class FamilyRegister
    {
        [PrimaryKey]
        public Guid FamilyId { get; set; }
        public int FamilyType { get; set; }
        public string FatherName { get; set; }
       // public DateTime FatherDOB { get; set; }//extra
        //public string FatherAadhaaId { get; set; } //extra
        public int FatherEducation { get; set; }
        public int FatherOccupation { get; set; }
        public string FatherPastDisease { get; set; }
        public string MotherName { get; set; }
        public DateTime ? MotherDOB { get; set; }
       // public string MotherAadhaarId { get; set; } //extra
        public int MotherEducation { get; set; }
        public int MotherOccupation { get; set; }
        public string MotherPastDisease { get; set; }
        public int IsMotherWorking { get; set; }
       // public bool IsMotherPragnent { get; set; } //extra
       // public DateTime ExpectedDeliveryDate { get; set; } // extra
        //public bool IsMotherLactating { get; set; } // extra
        public int NumberofChildenAlive { get; set; }
        public int NumberofChildenDied { get; set; }
        public int CasteTribe { get; set; }
        public string CasteTribeName { get; set; }
        public int Religion { get; set; }
        public int RationCardType { get; set; }
       
        public bool OwnAgriculturalLand { get; set; }
        public int HouseType { get; set; }
        public int WaterSupplyType { get; set; }
        public int SourceDrinkingWater { get; set; }

        public bool ElectricityAvailable { get; set; }
        public bool CookingGasAvailable { get; set; }
        public string Assets { get; set; }
        public int LocationId { get; set; }
        //public string Remark { get; set; }// extra
        public DateTime RegisterDate { get; set; }
        public DateTime DOE { get; set; }
        public DateTime DOU { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        public int MotherWorkType { get; set; }
        public int MotherWorkHours { get; set; }
        public int ToiletFacility { get; set; }
        public int FamilyMigrateToEarn { get; set; } 
        public int MigrationMonthsPerYear { get; set; }  //Entry Type
        public int MigrationFor { get; set; }
        public string FamilyCode { get; set; }
        public int status { get; set; }
      
        
    }
}
public class WaterTypeClass
{
    public int Id { get; set; }
    public string WaterType { get; set; }
}
public class MotherPregnant
{
    public int Id { get; set; }
    public string Pregnant { get; set; }
}
public class MotherLocating
{
    public int Id { get; set; }
    public string Locating { get; set; }
}
public class MotherWork
{
    public int Id { get; set; }
    public string IsMotherWork { get; set; }
}
public class Genders
{
    public int Id { get; set; }
    public string Gender { get; set; }
}
public class RationCard
{
    public int Id { get; set; }
    public string CartType { get; set; }
}
public class FamilyType
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class OwnAgriculturalLand
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class House
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class ElectricityAvailable
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class CookingGasAvailable
{
    public int Id { get; set; }
    public string Name { get; set; }
}
//public class Location
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//}
public class RedFlag
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class SchoolType
{
    public int Id { get; set; }
    public string Name { get; set; }
}
public class MonthlyMonitoringData
{
    public int Id { get; set; }
    public string Name { get; set; }
}