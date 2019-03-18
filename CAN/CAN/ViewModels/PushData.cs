using System;
using System.Collections.Generic;
using System.Text;
using CAN.Models;
namespace CAN.ViewModels
{
  public  class PushData
    {
        public string userId { get; set; }
        public string password { get; set; } 
  
        public List<FamilyRegister> familyData { get; set; }
        public List<ChildRegister> childData { get; set; }
       
        public List<GrowthRegister> growthData { get; set; }
        public List<RedFlagRegister> redFlagData { get; set; }
        public List<TblGrowthRegisterMother> growthRegisterMotherData { get; set; }
    }
}
