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
	public partial class ListOfVillage : ContentPage
	{
        List<VillageClass> BindVillageList = new List<VillageClass>();
        public ListOfVillage ()
		{
			InitializeComponent ();
           
            var villagelist = App.DAUtil.GetVillageLocations(9);

            listView.ItemsSource = villagelist;
            
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            txtactive.IsVisible = true;
            lblmessage.IsVisible = true;
            Location villageN = (Location)listView.SelectedItem;
            string villageName = villageN.locationName;
            StaticClass.VillageID = villageN.locationId;
            StaticClass.PageName = "HomePage";

            Application.Current.MainPage = new MasterDetailPage1();
        }
    }
}