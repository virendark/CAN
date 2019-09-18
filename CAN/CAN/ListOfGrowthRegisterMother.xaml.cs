using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CAN.Models;
using CAN.ViewModels;
namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfGrowthRegisterMother : ContentPage
    {
        int previousValue = 0;
        long id;
        int a = 0;
        public ListOfGrowthRegisterMother()
        {
            InitializeComponent();
            listView.IsVisible = false;
            this.Title = StaticClass.LocationName;
            BindDatamonths();
            BindDataStatus();
           // BindPageLoad();
        }
        public void BindDatamonths()
        {
           // List<DataM> listdataMonths = new List<DataM>();

            var ListOfDatamonth = StaticClass.dataManths; //App.DAUtil.GetDataMonthsFormate().OrderByDescending(x => x.Datamonthid).ToList();
            //for (int i = 0; i < ListOfDatamonth.Count; i++)
            //{
            //    DataM dataMonths = new DataM();
            //    dataMonths.Datamonthid = ListOfDatamonth[i].Datamonthid;
            //    string formatted = ListOfDatamonth[i].Datamonth.ToString("MMM-yyyy");
            //    dataMonths.Datamonth = formatted;
            //    listdataMonths.Add(dataMonths);
            //}

            ddlDataMonth.ItemsSource = StaticClass.dataManthFormat;
            for (int j = 0; j< ListOfDatamonth.Count; j++)
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
        public void BindDataStatus()
        {
             var ListOfddlStatusCheck = App.DAUtil.GetColumnValuesBytext(55);
            ddlStatusCheck.ItemsSource =ListOfddlStatusCheck;
            StaticClass.MotherStatus = ddlStatusCheck.SelectedIndex;
            ddlStatusCheck.SelectedIndex = StaticClass.MotherStatus==-1?0: StaticClass.MotherStatus;
            
        }
       
        private void BindList()
        {
              try
                {
                    long id = StaticClass.VillageID;
                btnPrivious.IsEnabled = false;
                btnPriviousnext.IsEnabled = true;
                //   var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                List<MotherWithChildDetails> MotherWithChildDetails = new List<MotherWithChildDetails>();
            
                List<MotherMonthlyData> ListmotherWithChildDetails = new List<MotherMonthlyData>();
                //if (ListData != null)
                //    {
                //var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
                       int StatusId = 0; //= selectedStatusId.columnValueId;
                        var DataMId = (DataM)ddlDataMonth.SelectedItem;
                        int DataID = DataMId.Datamonthid;
                var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
                if (selectedStatusId == null)
                {

                    
                    StatusId = 122;
                }
                else
                {
                    StatusId = selectedStatusId.columnValueId;
                }
                // MotherWithChildDetails = App.DAUtil.GetMotherWithChildDetailsWithOutDataId(id, StatusId);
                MotherWithChildDetails = App.DAUtil.GetMotherWithChildDetails(id, DataID, StatusId).Take(10).ToList(); 
                   // int check = 0;
                        for (int i = 0; i < MotherWithChildDetails.Count; i++)
                        {
                       // check = i;
                            MotherMonthlyData motherWithChildDetails = new MotherMonthlyData();
                            motherWithChildDetails.ChildExpected = MotherWithChildDetails[i].ChildExpected == null ? MotherWithChildDetails[i].ChildExpected : null;
                            motherWithChildDetails.FamilyId = MotherWithChildDetails[i].FamilyId;
                            motherWithChildDetails.FatherName = MotherWithChildDetails[i].FatherName;
                            motherWithChildDetails.MotherName = MotherWithChildDetails[i].MotherName;
                            motherWithChildDetails.GrowthId = MotherWithChildDetails[i].GrowthId;
                        motherWithChildDetails.FamilyCode = MotherWithChildDetails[i].FamilyCode;
                        motherWithChildDetails.IsPregrent= MotherWithChildDetails[i].ChildExpected == true ? "True" : "False";
                        motherWithChildDetails.IsLactating = MotherWithChildDetails[i].IsLactating == true ? "True" : "False";
                        if (MotherWithChildDetails[i].HighRiskMotherHistory!=null)
                        {

                            motherWithChildDetails.colobind = "PaleVioletRed";
                        }
                        else
                        {
                            motherWithChildDetails.colobind = "White";
                        }
                            motherWithChildDetails.HighRiskMotherHistory = MotherWithChildDetails[i].HighRiskMotherHistory == null? "Mother having history:- Normal" : "Mother having history:- HighRisk";
                            if (MotherWithChildDetails[i].DataMonthId != 0)
                            {
                                var dateId = App.DAUtil.GetDataMonthByID(MotherWithChildDetails[i].DataMonthId);
                            if (dateId.Count > 0)
                            {
                                motherWithChildDetails.DataMonthId = dateId[0].Datamonth.ToString("MMM-yyyy");
                                motherWithChildDetails.IsvisuaAdd = false;
                                motherWithChildDetails.IsvisuaEdit = true;
                            }
                            }
                            else
                            {
                            if(MotherWithChildDetails[i].status ==122)
                            {
                                motherWithChildDetails.IsvisuaAdd = true;
                                motherWithChildDetails.IsvisuaEdit = false;
                                motherWithChildDetails.DataMonthId = "Null";
                            }
                            else
                            {
                                motherWithChildDetails.IsvisuaEdit = true;
                            }
                            
                            }
                             ListmotherWithChildDetails.Add(motherWithChildDetails);
                        }
                    
                        listView.IsVisible = true;
                        listView.ItemsSource = ListmotherWithChildDetails;
                    }
                  
               // }
                catch
                {
               
                 }
    
        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
          var item = (Xamarin.Forms.Image)sender;
          var d = item.Source.BindingContext;
            MotherMonthlyData family = (MotherMonthlyData)d;
            StaticClass.GrouthMotherID = family.GrowthId;
            var checkMotherGrouthdata = App.DAUtil.FindGrowthRegisterMotherSingleData(StaticClass.GrouthMotherID.ToString());
            if (checkMotherGrouthdata.Count > 0)
            {
                StaticClass.PageButtonText = "Update";
                Navigation.PushAsync(new GrowthRegisterMother());
            }
            else
            {
                DependencyService.Get<Toast>().Show("Can't be Edit Please Add");
                return;
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            var item = (Xamarin.Forms.Image)sender;
           var d = item.Source.BindingContext;
            MotherMonthlyData family = (MotherMonthlyData)d;
            StaticClass.GrouthFamilyId = family.FamilyId;
            StaticClass.PageButtonText = "Save";
            Navigation.PushAsync(new GrowthRegisterMother());
        }

        private void DdlDataMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
             
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            BindList();

        }

        private void DdlStatusCheck_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
            int id = selectedStatusId.columnValueId;
            StaticClass.MotherStatus = ddlStatusCheck.SelectedIndex;
            BindList();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;
            MotherMonthlyData family = (MotherMonthlyData)d;
            StaticClass.GrouthFamilyId = family.FamilyId;
            StaticClass.PageButtonText = "Save";
            Navigation.PushAsync(new GrowthRegisterMother());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;
            MotherMonthlyData family = (MotherMonthlyData)d;
            StaticClass.GrouthMotherID = family.GrowthId;
            var checkMotherGrouthdata = App.DAUtil.FindGrowthRegisterMotherSingleData(StaticClass.GrouthMotherID.ToString());
            if (checkMotherGrouthdata.Count > 0)
            {
                StaticClass.PageButtonText = "Update";
                Navigation.PushAsync(new GrowthRegisterMother());
            }
            else
            {
                DependencyService.Get<Toast>().Show("Can't be Edit Please Add");
                return;
            }
        }

        private void BtnPriviousnext_Clicked(object sender, EventArgs e)
        {
            if (previousValue >= 0)
            {
                btnPrivious.IsEnabled = true;
                previousValue += 10;

                try
                {
                    long id = StaticClass.VillageID;
                   // var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                    List<MotherWithChildDetails> MotherWithChildDetails = new List<MotherWithChildDetails>();
                    List<MotherMonthlyData> ListmotherWithChildDetails = new List<MotherMonthlyData>();
                    //if (ListData != null)
                    //{
                        var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
                        int StatusId = selectedStatusId.columnValueId;
                        var DataMId = (DataM)ddlDataMonth.SelectedItem;
                        int DataID = DataMId.Datamonthid;
                        // MotherWithChildDetails = App.DAUtil.GetMotherWithChildDetailsWithOutDataId(id, StatusId);
                        MotherWithChildDetails = App.DAUtil.GetMotherWithChildDetails(id, DataID, StatusId).Skip(previousValue).Take(10).ToList();
                        int check = 0;
                        for (int i = 0; i < MotherWithChildDetails.Count; i++)
                        {
                            check = i;
                            MotherMonthlyData motherWithChildDetails = new MotherMonthlyData();
                            motherWithChildDetails.ChildExpected = MotherWithChildDetails[i].ChildExpected == null ? MotherWithChildDetails[i].ChildExpected : null;
                            motherWithChildDetails.FamilyId = MotherWithChildDetails[i].FamilyId;
                            motherWithChildDetails.FatherName = MotherWithChildDetails[i].FatherName;
                            motherWithChildDetails.MotherName = MotherWithChildDetails[i].MotherName;
                            motherWithChildDetails.GrowthId = MotherWithChildDetails[i].GrowthId;
                            motherWithChildDetails.FamilyCode = MotherWithChildDetails[i].FamilyCode;
                            motherWithChildDetails.IsPregrent = MotherWithChildDetails[i].ChildExpected == true ? "True" : "False";
                            motherWithChildDetails.IsLactating = MotherWithChildDetails[i].IsLactating == true ? "True" : "False";
                            if (MotherWithChildDetails[i].HighRiskMotherHistory != null)
                            {

                                motherWithChildDetails.colobind = "PaleVioletRed";
                            }
                            else
                            {
                                motherWithChildDetails.colobind = "White";
                            }
                            motherWithChildDetails.HighRiskMotherHistory = MotherWithChildDetails[i].HighRiskMotherHistory == null ? "Mother having history:- Normal" : "Mother having history:- HighRisk";
                            if (MotherWithChildDetails[i].DataMonthId != 0)
                            {
                                var dateId = App.DAUtil.GetDataMonthByID(MotherWithChildDetails[i].DataMonthId);
                                if (dateId.Count > 0)
                                {
                                    motherWithChildDetails.DataMonthId = dateId[0].Datamonth.ToString("MMM-yyyy");
                                    motherWithChildDetails.IsvisuaAdd = false;
                                    motherWithChildDetails.IsvisuaEdit = true;
                                }
                            }
                            else
                            {
                                if (MotherWithChildDetails[i].status == 122)
                                {
                                    motherWithChildDetails.IsvisuaAdd = true;
                                    motherWithChildDetails.IsvisuaEdit = false;
                                    motherWithChildDetails.DataMonthId = "Null";
                                }
                                else
                                {
                                    motherWithChildDetails.IsvisuaEdit = true;
                                }

                            }


                            ListmotherWithChildDetails.Add(motherWithChildDetails);
                        }
                        listView.IsVisible = true;

                        if (ListmotherWithChildDetails.Count == 0)
                        {
                        btnPriviousnext.IsEnabled = false;

                        }
                        else
                        {

                        btnPriviousnext.IsEnabled = true;
                        listView.ItemsSource = null;
                        listView.ItemsSource = ListmotherWithChildDetails;
                        }
                    //}


                }
                catch
                {

                }

            }
        }

        private void BtnPrivious_Clicked(object sender, EventArgs e)
        {
            if (previousValue > 0)
            {
                btnPriviousnext.IsEnabled = true;
                previousValue -= 10;
               
                try
                {
                    long id = StaticClass.VillageID;
                  //  var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                    List<MotherWithChildDetails> MotherWithChildDetails = new List<MotherWithChildDetails>();
                    List<MotherMonthlyData> ListmotherWithChildDetails = new List<MotherMonthlyData>();
                    //if (ListData != null)
                    //{
                        var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
                        int StatusId = selectedStatusId.columnValueId;
                        var DataMId = (DataM)ddlDataMonth.SelectedItem;
                        int DataID = DataMId.Datamonthid;
                        // MotherWithChildDetails = App.DAUtil.GetMotherWithChildDetailsWithOutDataId(id, StatusId);
                        MotherWithChildDetails = App.DAUtil.GetMotherWithChildDetails(id, DataID, StatusId).Skip(previousValue).Take(10).ToList();
                        int check = 0;
                        for (int i = 0; i < MotherWithChildDetails.Count; i++)
                        {
                            check = i;
                            MotherMonthlyData motherWithChildDetails = new MotherMonthlyData();
                            motherWithChildDetails.ChildExpected = MotherWithChildDetails[i].ChildExpected == null ? MotherWithChildDetails[i].ChildExpected : null;
                            motherWithChildDetails.FamilyId = MotherWithChildDetails[i].FamilyId;
                            motherWithChildDetails.FatherName = MotherWithChildDetails[i].FatherName;
                            motherWithChildDetails.MotherName = MotherWithChildDetails[i].MotherName;
                            motherWithChildDetails.GrowthId = MotherWithChildDetails[i].GrowthId;
                            motherWithChildDetails.FamilyCode = MotherWithChildDetails[i].FamilyCode;
                            motherWithChildDetails.IsPregrent = MotherWithChildDetails[i].ChildExpected == true ? "True" : "False";
                            motherWithChildDetails.IsLactating = MotherWithChildDetails[i].IsLactating == true ? "True" : "False";
                            if (MotherWithChildDetails[i].HighRiskMotherHistory != null)
                            {

                                motherWithChildDetails.colobind = "PaleVioletRed";
                            }
                            else
                            {
                                motherWithChildDetails.colobind = "White";
                            }
                            motherWithChildDetails.HighRiskMotherHistory = MotherWithChildDetails[i].HighRiskMotherHistory == null ? "Mother having history:- Normal" : "Mother having history:- HighRisk";
                            if (MotherWithChildDetails[i].DataMonthId != 0)
                            {
                                var dateId = App.DAUtil.GetDataMonthByID(MotherWithChildDetails[i].DataMonthId);
                                if (dateId.Count > 0)
                                {
                                    motherWithChildDetails.DataMonthId = dateId[0].Datamonth.ToString("MMM-yyyy");
                                    motherWithChildDetails.IsvisuaAdd = false;
                                    motherWithChildDetails.IsvisuaEdit = true;
                                }
                            }
                            else
                            {
                                if (MotherWithChildDetails[i].status == 122)
                                {
                                    motherWithChildDetails.IsvisuaAdd = true;
                                    motherWithChildDetails.IsvisuaEdit = false;
                                    motherWithChildDetails.DataMonthId = "Null";
                                }
                                else
                                {
                                    motherWithChildDetails.IsvisuaEdit = true;
                                }

                            }


                            ListmotherWithChildDetails.Add(motherWithChildDetails);
                        }
                        listView.IsVisible = true;
                       
                        if (ListmotherWithChildDetails.Count == 0)
                        {
                        btnPrivious.IsEnabled = false;

                    }
                        else
                        {
                        btnPrivious.IsEnabled = true;
                            listView.ItemsSource = null;
                            listView.ItemsSource = ListmotherWithChildDetails;
                        }
                   // }
                   

                }
                catch
                {

                }
              
            }
            else
            {
                btnPrivious.IsEnabled = false;
            }
        }
    }
    public class DataM
    {
        public int Datamonthid { get; set; }
        public string Datamonth { get; set; }
        public int Locked { get; set; }
    }
    public class MotherMonthlyData
    {
        public Guid GrowthId { get; set; }
        public Guid FamilyId { get; set; }
        public int FamilyType { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
       
        public int LocationId { get; set; }
        public bool ?  ChildExpected { get; set; }
        public DateTime ? AwcregistrationDate { get; set; }
        public string FamilyCode { get; set; }
        public string DataMonthId { get; set; }
        public bool IsvisuaAdd { get; set; }
        public bool IsvisuaEdit { get; set; }
        public string HighRiskMotherHistory { get; set; }
        public string colobind { get; set; }
        public string IsLactating { get; set; }
        public string IsPregrent { get; set; }
    }
}