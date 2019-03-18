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
	public partial class ListOfMonthlyMonitoring : ContentPage
	{
        public ListOfMonthlyMonitoring()
        {
            InitializeComponent();
             BindDatamonths();
            BindList();
        }
        public void BindDatamonths()
        {
            List<DataM> listdataMonths = new List<DataM>();
            int index = 0;
            var ListOfDatamonth = App.DAUtil.GetDataMonthsFormate();
            for (int i = 0; i < ListOfDatamonth.Count; i++)
            {
                DateTime dt = new DateTime();
              
                DataM dataMonths = new DataM();
                dataMonths.Datamonthid = ListOfDatamonth[i].Datamonthid;
                string formatted = ListOfDatamonth[i].Datamonth.ToString("MMM-yyyy");
                dataMonths.Datamonth = formatted;
                listdataMonths.Add(dataMonths);
            }
            ddlDataMonth.ItemsSource = listdataMonths;
            for (int j = 0; j < ListOfDatamonth.Count; j++)
            {
                string formatted = ListOfDatamonth[j].Datamonth.ToString("MMM-yyyy");
                DateTime dtcurrent = new DateTime();
                dtcurrent = DateTime.Now;
                string dt = dtcurrent.ToString("MMM-yyyy");
                if (dt == formatted)
                {
                    ddlDataMonth.SelectedIndex = j;
                    StaticClass.DataMonthId = ListOfDatamonth[j].Datamonthid;
                }
            }
         }
        private void DdlDataMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            BindList();
        }
        private void BindList()
        {
            var monitoringData = App.DAUtil.GetMonthlyMonitoringById(StaticClass.DataMonthId);
            listView.ItemsSource = monitoringData;
        }

        private async void BtnAdd_ItemTapped(object sender, EventArgs e)
        {

            StaticClass.PageButtonText = "Save";
          
            await Navigation.PushAsync(new MonthlyMonitoringPage());
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Image)sender;

            var d = item.Source.BindingContext;
            MonthlyMonitoring monthlyMonitoring = (MonthlyMonitoring)d;
            Guid MID = monthlyMonitoring.MonthlyMonitorId;
            StaticClass.PageData = MID;
            
            StaticClass.PageButtonText = "Update";
            await Navigation.PushAsync(new MonthlyMonitoringPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;
            MonthlyMonitoring monthlyMonitoring = (MonthlyMonitoring)d;
            Guid MID = monthlyMonitoring.MonthlyMonitorId;
            StaticClass.PageData = MID;
            StaticClass.PageButtonText = "Update";
            await Navigation.PushAsync(new MonthlyMonitoringPage());
        }
    }
}