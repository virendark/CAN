using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAN
{

    public class MasterNavigationPageMenuItem
    {
        public MasterNavigationPageMenuItem()
        {
            TargetType = typeof(MasterNavigationPageDetail);
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string ImgIcon { get; set; }
        public Type TargetType { get; set; }
    }
}