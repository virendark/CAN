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
    public partial class EntryPage : ContentPage
    {
        public EntryPage()
        {
            InitializeComponent();
        }

        private void Fregister_Clicked(object sender, EventArgs e)
        {
            StaticClass.PageName = "FamilyPage";

            Application.Current.MainPage = new MasterDetailPage1();
        }

        private void CRegister_Clicked(object sender, EventArgs e)
        {

            StaticClass.PageName = "ChildPage";

            Application.Current.MainPage = new MasterDetailPage1();
        }

        private void Mregister_Clicked(object sender, EventArgs e)
        {
            StaticClass.PageName = "MonthlyData";

            Application.Current.MainPage = new MasterDetailPage1();
        }

        private void RedRegister_Clicked(object sender, EventArgs e)
        {
            StaticClass.PageName = "RedFlag";

            Application.Current.MainPage = new MasterDetailPage1();
        }
    }
}