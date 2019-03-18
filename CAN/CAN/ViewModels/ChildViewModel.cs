using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CAN.ViewModels
{
  public  class ChildViewModel
    {
        public Guid ChildId { get; set; }

        public Guid FamilyId { get; set; }

        public string ChildName { get; set; }
       

        public DateTime DOB { get; set; }

        public string Gender { get; set; }
        public Image Img { get; set; }
        public  byte[] Photograph { get; set; }
        public ImageSource PhotoData { get; set; }
        public string BirthWeightInKg { get; set; }
        public string W4HZ { get; set; }
        public string W4AZ { get; set; }
    }
}
