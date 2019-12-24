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
            bindList();
        }

        private void bindList()
        {
           
            List<VillageClass> BindVillageList = new List<VillageClass>();
            var villagelist = App.DAUtil.GetVillageLocations(9).OrderBy(v=>v.locationName).ToList();
            for (int i = 0; i<villagelist.Count; i++)
            {
                VillageClass villageClass = new VillageClass();
                if (villagelist[i].DCType == "I")
                {
                    villageClass.Id = villagelist[i].locationId;
                    villageClass.TabId = 0;
                    string Name = villagelist[i].locationName;
                    villageClass.VillageName = Name + "(" + villagelist[i].DCType + ")" +" (" + villagelist[i].locationId + ")";
                }
                else
                {
                    villageClass.Id = villagelist[i].locationId;
                   
                    villageClass.TabId = villagelist[i].locationId;
                    villageClass.VillageName = villagelist[i].locationName + " (" + villagelist[i].locationId + ")";
                }
                BindVillageList.Add(villageClass);
            }
            listView.ItemsSource = BindVillageList;
       
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            txtactive.IsVisible = true;
            lblmessage.IsVisible = true;
            VillageClass villageN = (VillageClass)listView.SelectedItem;
            //string villageName = villageN.VillageName;
            StaticClass.LocationName = villageN.VillageName;
            StaticClass.VillageID = villageN.Id;
            StaticClass.PageName = "HomePage";

            Application.Current.MainPage = new MasterNavigationPage();
        }
    }
}