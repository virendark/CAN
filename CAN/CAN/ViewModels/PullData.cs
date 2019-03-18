using System;
using System.Collections.Generic;
using System.Text;
using CAN.Models;
namespace CAN.ViewModels
{
  public  class PullData
    {
       
        public List<ChildRegister> childRegisters { get; set; }
        public List<FamilyRegister> familyRegisters { get; set; }
        public List<GrowthRegister> growthRegisters { get; set; }
        public List<RedFlagRegister> redFlagRegisters { get; set; }
        public List<TblGrowthRegisterMother> growthRegisterMothers { get; set; }
    }

    
}
