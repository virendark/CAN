using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace CAN.Models
{
  public  class MonthlyMonitoring
    {
        
   [PrimaryKey]
   public Guid MonthlyMonitorId { get; set; }
   public int DataMonthId { get; set; }
   public int LocationId { get; set; }//[bigint]
   public int NoOfDaysDistributionAAYForMothers { get; set; }
   public int NoOfDaysDistributionAAYForChild { get; set; }
   public int PregnantandLactatingMothers { get; set; } //[bigint] ;
   public int Childrenfrom3yrsto6yrs { get; set; }//[bigint] NULL,
   public int DidMalnourishedChildrenReceiveBenifits { get; set; } //[bigint] NULL,
   public bool ImmunisationforchildrencondutedlastmonthANM { get; set; } //[bit] NULL,
   public bool HealthCheckupsforpregnantwomenconductedmonthlyAW { get; set; }// [bit] NULL,
   public bool VHNDconductedlastmonth { get; set; } //[bit] NULL,
   public bool AWfunctions4hoursdaily { get; set; } //[bit] NULL,
   public int ReceivedAAYamountpregnantandlactatingwomen35 { get; set; }// [bigint] NULL,
   public int ReceivedAAYamountforchildren5 { get; set; }// [bigint] NULL,
   public int ReceivedAWWremunerationforpreparingAAY500 { get; set; }// [bigint] NULL,
   public int ReceivedHelperremunerationpreparingAAY500 { get; set; } //[bigint] NULL,
    public string Remarks { get; set; }



    }
}
