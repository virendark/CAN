using CAN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HeaderTabbed : TabbedPage
    {
        public HeaderTabbed ()
        {
            InitializeComponent();
            this.Title = StaticClass.LocationName;
            
            CurrentPage = Children[StaticClass.TabbedIndex];
        }
    }
}