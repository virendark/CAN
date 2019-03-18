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
	public partial class ListOfEntryPage : ContentPage
	{
		public ListOfEntryPage ()
		{
			InitializeComponent ();
            this.Title = StaticClass.LocationName;

        }

        private async void Fregister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfFamilyPage());
        }

        private async void CRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfChildPage());
        }

        private async void Mregister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfMonthlydataPage());
        }

        private async void RedRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ListOfRedFlagPage());
        }
    }
}