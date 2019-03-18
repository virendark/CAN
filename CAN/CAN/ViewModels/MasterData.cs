using CAN.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.ViewModels
{
  public  class MasterData
    {
        // public List<ColumnTypeData> columnType { get; set; }
        //public List<ColumnValueData> columnValue { get; set; }
        //public List<Location> location { get; set; }
        //public List<LmSBMI> lmS_BMI { get; set; }
        //public List<LmSHAZ> lmS_HAZ { get; set; }
        //public List<LmSWAZ> lmS_WAZ { get; set; }
        //public List<LmSWFH> lmS_WFH { get; set; }
        //public List<LmSWFL> lmS_WFL { get; set; }
        public List<DataMonth> dataMonths { get; set; }
        public List<ColumnTypeData> columnType { get; set; }
        public List<ColumnValueData> columnValue { get; set; }
        public List<LocationData> location { get; set; }
        public List<LmSBMI> lmS_BMI { get; set; }
        public List<LmSHAZ> lmS_HAZ { get; set; }
        public List<LmSWAZ> lmS_WAZ { get; set; }
        public List<LmSWFH> lmS_WFH { get; set; }
        public List<LmSWFL> lmS_WFL { get; set; }

    }

    public class DataMonth
    {
        public int datamonthid { get; set; }
        public DateTime datamonth { get; set; }
        public bool locked { get; set; }
    }

    public class LocationData
    {
        public int locationId { get; set; }
        public string locationName { get; set; }
        public int locationTypeId { get; set; }
        public int? parentId { get; set; }
        public string locationType { get; set; }
        public string DCType { get; set; }
    }

    public class LmSBMI
    {
        public int genderId { get; set; }
        public int ageInDays { get; set; }
        public double l { get; set; }
        public double m { get; set; }
        public double s { get; set; }
    }

    public class LmSHAZ
    {
        public int genderId { get; set; }
        public int ageInDays { get; set; }
        public double l { get; set; }
        public double m { get; set; }
        public double s { get; set; }
    }

    public class LmSWAZ
    {
        public int genderId { get; set; }
        public int ageInDays { get; set; }
        public double l { get; set; }
        public double m { get; set; }
        public double s { get; set; }
    }

    public class LmSWFH
    {
        public int genderId { get; set; }
        public double heightInCm { get; set; }
        public double l { get; set; }
        public double m { get; set; }
        public double s { get; set; }
    }

    public class LmSWFL
    {
        public int genderId { get; set; }
        public double lengthInCm { get; set; }
        public double l { get; set; }
        public double m { get; set; }
        public double s { get; set; }
    }

    public class ColumnTypeData
    {
        public int columnTypeId { get; set; }
        public string typeName { get; set; }
        public List<ColumnValueData> columnValue { get; set; }

    }

    public class ColumnValueData
    {
        public int columnValueId { get; set; }
        public int columnTypeId { get; set; }
        public string columnValue { get; set; }
    }
    //public class LmSBMI
    //{

    //    public int genderId { get; set; }
    //    public int ageInDays { get; set; }
    //    public double l { get; set; }
    //    public double m { get; set; }
    //    public double s { get; set; }

    //}
    //public class LmSHAZ
    //{
    //    public int genderId { get; set; }
    //    public int ageInDays { get; set; }
    //    public double l { get; set; }
    //    public double m { get; set; }
    //    public double s { get; set; }
    //}
    //public class LmSWAZ
    //{
    //    public int genderId { get; set; }
    //    public int ageInDays { get; set; }
    //    public double l { get; set; }
    //    public double m { get; set; }
    //    public double s { get; set; }
    //}
    //public class LmSWFH
    //{
    //    public int genderId { get; set; }
    //    public double heightInCm { get; set; }
    //    public double l { get; set; }
    //    public double m { get; set; }
    //    public double s { get; set; }
    //}

    //public class LmSWFL
    //{
    //    public int genderId { get; set; }
    //    public double lengthInCm { get; set; }
    //    public double l { get; set; }
    //    public double m { get; set; }
    //    public double s { get; set; }
    //}
}
