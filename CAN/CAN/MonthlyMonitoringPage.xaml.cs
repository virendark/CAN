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
	public partial class MonthlyMonitoringPage : ContentPage
	{
         List<MonthlyMonitoringData> listMonthlyMonitoringDatas = new List<MonthlyMonitoringData>();
        public MonthlyMonitoringPage ()
		{
			InitializeComponent ();
            BindImmunisationforchildrencondutedlastmonthANM();
            BindddlHealthCheckupsforpregnantwomenconductedmonthlyAW();
            BinddddlVHNDconductedlastmonth();
            BindddlAWfunctions4hoursdaily();
            btnSave.Text = StaticClass.PageButtonText;
            if (StaticClass.PageButtonText == "Update")
            {
                EditData();
            }
        }
        public void BindImmunisationforchildrencondutedlastmonthANM()
        {
            MonthlyMonitoringData monthlyMonitoringData = new MonthlyMonitoringData();
            monthlyMonitoringData.Id = 1;
            monthlyMonitoringData.Name = "Yes";
            listMonthlyMonitoringDatas.Add(monthlyMonitoringData);
            MonthlyMonitoringData monthlyMonitoringData1 = new MonthlyMonitoringData();
            monthlyMonitoringData1.Id = 0;
            monthlyMonitoringData1.Name = "No";
            listMonthlyMonitoringDatas.Add(monthlyMonitoringData1);
            ddlImmunisationforchildrencondutedlastmonthANM.ItemsSource = listMonthlyMonitoringDatas;
        }
        public void BindddlHealthCheckupsforpregnantwomenconductedmonthlyAW()
        {
            ddlHealthCheckupsforpregnantwomenconductedmonthlyAW.ItemsSource = listMonthlyMonitoringDatas;
        }
        public void BinddddlVHNDconductedlastmonth()
        {
            ddlVHNDconductedlastmonth.ItemsSource = listMonthlyMonitoringDatas;
        }
        public void BindddlAWfunctions4hoursdaily()
        {
            ddlAWfunctions4hoursdaily.ItemsSource = listMonthlyMonitoringDatas;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            MonthlyMonitoring monthlyMonitoring = new MonthlyMonitoring();
            if (StaticClass.PageButtonText == "Update")
            {
               
                //var monthlyMonitoringData = App.DAUtil.GetMonthlyMonitoringBySingleData(StaticClass.PageData);
                monthlyMonitoring.MonthlyMonitorId = StaticClass.PageData;
                App.DAUtil.DeleteMonthlyMonitoringByID(StaticClass.PageData.ToString());
            }
            else
            {
                monthlyMonitoring.MonthlyMonitorId = Guid.NewGuid();
               
            }
            
            monthlyMonitoring.DataMonthId = StaticClass.DataMonthId;
            monthlyMonitoring.LocationId = StaticClass.VillageID;
            var selectedAWfunctions4hoursdaily = (MonthlyMonitoringData)ddlAWfunctions4hoursdaily.SelectedItem;
            string IsselectedAWfunctions4hoursdaily = selectedAWfunctions4hoursdaily == null ? "No" : selectedAWfunctions4hoursdaily.Name;
            monthlyMonitoring.AWfunctions4hoursdaily = IsselectedAWfunctions4hoursdaily == "Yes" ? true : false;
            monthlyMonitoring.Childrenfrom3yrsto6yrs= Convert.ToInt32(txtChildrenfrom3yrsto6yrs.Text);
            monthlyMonitoring.DidMalnourishedChildrenReceiveBenifits = Convert.ToInt32(txtDidMalnourishedChildrenReceiveBenifits.Text);
            var selectedHealthCheckupsforpregnantwomenconductedmonthlyAW = (MonthlyMonitoringData)ddlHealthCheckupsforpregnantwomenconductedmonthlyAW.SelectedItem;
            string IsselectedHealthCheckupsforpregnantwomenconductedmonthlyAW = selectedHealthCheckupsforpregnantwomenconductedmonthlyAW == null ? "No" : selectedHealthCheckupsforpregnantwomenconductedmonthlyAW.Name;
            monthlyMonitoring.HealthCheckupsforpregnantwomenconductedmonthlyAW = IsselectedHealthCheckupsforpregnantwomenconductedmonthlyAW == "Yes" ? true : false;
            var selectedImmunisationforchildrencondutedlastmonthANM = (MonthlyMonitoringData)ddlImmunisationforchildrencondutedlastmonthANM.SelectedItem;
            string IsselectedImmunisationforchildrencondutedlastmonthANM = selectedImmunisationforchildrencondutedlastmonthANM == null ? "No" : selectedImmunisationforchildrencondutedlastmonthANM.Name;
            monthlyMonitoring.ImmunisationforchildrencondutedlastmonthANM = IsselectedImmunisationforchildrencondutedlastmonthANM == "Yes" ? true : false;
            monthlyMonitoring.NoOfDaysDistributionAAYForChild = Convert.ToInt32(txtNoOfDaysDistributionAAYForChild.Text);
            monthlyMonitoring.NoOfDaysDistributionAAYForMothers = Convert.ToInt32(txtNoOfDaysDistributionAAYForMothers.Text);
            monthlyMonitoring.PregnantandLactatingMothers = Convert.ToInt32(txtPregnantandLactatingMothers.Text);
            monthlyMonitoring.ReceivedAAYamountforchildren5 = Convert.ToInt32(txtReceivedAAYamountforchildren5.Text);
            monthlyMonitoring.ReceivedAAYamountpregnantandlactatingwomen35 = Convert.ToInt32(txtReceivedAAYamountpregnantandlactatingwomen35.Text);
            monthlyMonitoring.ReceivedAWWremunerationforpreparingAAY500 = Convert.ToInt32(txtReceivedAWWremunerationforpreparingAAY500.Text);
            monthlyMonitoring.ReceivedHelperremunerationpreparingAAY500 = Convert.ToInt32(txtReceivedHelperremunerationpreparingAAY500.Text);
            monthlyMonitoring.Remarks = txtRemarks.Text;
            var selectedVHNDconductedlastmonth = (MonthlyMonitoringData)ddlVHNDconductedlastmonth.SelectedItem;
            string IsselectedVHNDconductedlastmonth = selectedVHNDconductedlastmonth == null ? "No" : selectedVHNDconductedlastmonth.Name;
            monthlyMonitoring.VHNDconductedlastmonth = IsselectedVHNDconductedlastmonth == "Yes" ? true : false;
            App.DAUtil.saveMonthlyMonitoring(monthlyMonitoring);
            StaticClass.PageName = "MonthlyMonitoring";
            Application.Current.MainPage = new MasterNavigationPage();
        }
        private void EditData()
        {
            
            var monthlyMonitoringData = App.DAUtil.GetMonthlyMonitoring().Where(x=>x.MonthlyMonitorId== StaticClass.PageData).ToList();
            if (monthlyMonitoringData.Count > 0)
            {
                try
                {
                 
                    ddlAWfunctions4hoursdaily.SelectedIndex = monthlyMonitoringData[0].AWfunctions4hoursdaily == true ? 0 : 1;
                    txtChildrenfrom3yrsto6yrs.Text = monthlyMonitoringData[0].Childrenfrom3yrsto6yrs.ToString();
                    txtDidMalnourishedChildrenReceiveBenifits.Text = monthlyMonitoringData[0].DidMalnourishedChildrenReceiveBenifits.ToString();
                    ddlHealthCheckupsforpregnantwomenconductedmonthlyAW.SelectedIndex= monthlyMonitoringData[0].HealthCheckupsforpregnantwomenconductedmonthlyAW == true ? 0 : 1;
                    ddlImmunisationforchildrencondutedlastmonthANM.SelectedIndex = monthlyMonitoringData[0].ImmunisationforchildrencondutedlastmonthANM == true ? 0 : 1;
                    txtNoOfDaysDistributionAAYForChild.Text = monthlyMonitoringData[0].NoOfDaysDistributionAAYForChild.ToString();
                    txtNoOfDaysDistributionAAYForMothers.Text = monthlyMonitoringData[0].NoOfDaysDistributionAAYForMothers.ToString();
                    txtPregnantandLactatingMothers.Text = monthlyMonitoringData[0].PregnantandLactatingMothers.ToString();
                    txtReceivedAAYamountforchildren5.Text = monthlyMonitoringData[0].ReceivedAAYamountforchildren5.ToString();
                    txtReceivedAAYamountpregnantandlactatingwomen35.Text = monthlyMonitoringData[0].ReceivedAAYamountpregnantandlactatingwomen35.ToString();
                    txtReceivedAWWremunerationforpreparingAAY500.Text = monthlyMonitoringData[0].ReceivedAWWremunerationforpreparingAAY500.ToString();
                    txtReceivedHelperremunerationpreparingAAY500.Text = monthlyMonitoringData[0].ReceivedHelperremunerationpreparingAAY500.ToString();
                    txtRemarks.Text = monthlyMonitoringData[0].Remarks.ToString();
                    ddlVHNDconductedlastmonth.SelectedIndex = monthlyMonitoringData[0].VHNDconductedlastmonth == true ? 0 : 1;
                    
                }
                catch (Exception ex)
                {

                }


            }
        }
    }
}