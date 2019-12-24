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
            txtactive.IsVisible = false;
            lblmessage.IsVisible = false;
            BindDatamonths();
        }
           public void BindDatamonths()
           {

            StaticClass.dataManthFormat.Clear();
            StaticClass.dataManths.Clear();
            StaticClass.listOfStatus.Clear();

            StaticClass.dataManths = App.DAUtil.GetDataMonthsFormate().OrderByDescending(x => x.Datamonthid).ToList();
            for (int i = 0; i < StaticClass.dataManths.Count; i++)
            {
                DataM dataMonths = new DataM();
                dataMonths.Datamonthid = StaticClass.dataManths[i].Datamonthid;
                string formatted = StaticClass.dataManths[i].Datamonth.ToString("MMM-yyyy");
                dataMonths.Datamonth = formatted;
                StaticClass.dataManthFormat.Add(dataMonths);
            }
            StaticClass.listOfStatus= App.DAUtil.GetColumnValuesBytext(12);


        }
        private void BindList()
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
                    villageClass.VillageName = Name + "(" + villagelist[i].DCType + ")" +" ("+ villagelist[i].locationId+")";
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

        private async void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            txtactive.IsVisible = true;
            lblmessage.IsVisible = true;
            await Task.Delay(1000);
            VillageClass villageN = (VillageClass)listView.SelectedItem;
            //string villageName = villageN.VillageName;
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
                Application.Current.MainPage = new MasterNavigationPage();

            }
            else
            {
               StaticClass.PageName = "MonthlyMonitoring";
               Application.Current.MainPage = new MasterNavigationPage();
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