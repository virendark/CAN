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
    public partial class VillagePage : ContentPage
    {
        
        public VillagePage()
        {
            InitializeComponent();
            BindList();
        }
        private void BindList()
        {
            List<VillageClass> BindVillageList = new List<VillageClass>();
            var villagelist = App.DAUtil.GetVillageLocations(9);
            for (int i = 0; i<villagelist.Count; i++)
            {
                VillageClass villageClass = new VillageClass();
                if (villagelist[i].DCType == "Intensive")
                {
                    villageClass.Id = villagelist[i].locationId;
                    villageClass.TabId = 0;
                    string Name = villagelist[i].locationName;
                    villageClass.VillageName = Name + "(" + villagelist[i].DCType + ")";
                }
                else
                {
                    villageClass.Id = villagelist[i].locationId;
                   
                    villageClass.TabId = villagelist[i].locationId;
                    villageClass.VillageName = villagelist[i].locationName;
                }
                BindVillageList.Add(villageClass);
            }
            listView.ItemsSource = BindVillageList;
        }

        private void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            VillageClass villageN = (VillageClass)listView.SelectedItem;
            string villageName = villageN.VillageName;
            StaticClass.LocationName = villageN.VillageName;
            StaticClass.VillageID = villageN.Id;

            if (StaticClass.MonthlyMonitoringName != "a")
            {
                if (villageN.TabId != 0)
                {
                    StaticClass.PageName = "HomePage";
                    StaticClass.RedFlagButton = "false";
                }
                else
                {
                    StaticClass.PageName = "HomePage";
                    StaticClass.RedFlagButton = "true";
                }
                Application.Current.MainPage = new MasterDetailPage1();
            }
            else
            {
               StaticClass.PageName = "MonthlyMonitoring";
               Application.Current.MainPage = new MasterDetailPage1();
            }
            // Navigation.PushModalAsync(new EntryPage());
        }
    }
}
public class VillageClass
{
    public int Id { get; set; }
    public int TabId { get; set; }
    public string VillageName { get; set; }
    public string DCType { get; set; }
}