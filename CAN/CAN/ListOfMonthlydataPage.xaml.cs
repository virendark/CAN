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
    public partial class ListOfMonthlydataPage : ContentPage
    {
        int previousValue = 0;
        long id;
        
        public ListOfMonthlydataPage()
        {
            InitializeComponent();
            listView.IsVisible = false;
            this.Title = StaticClass.LocationName;
            BindDatamonths();
            BindDataStatus();
        }
        //private void BindList()
        //{
        //    long id = StaticClass.VillageID;
        //    var ListData = App.DAUtil.GetAllFamilyByLocation(id);
        //    if (ListData.Count > 0)
        //    {

        //        listView.IsVisible = true;
        //        listView.ItemsSource = App.DAUtil.GetMonthDataList(id);
        //    }

        //}
        //private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    var item = (Xamarin.Forms.Image)sender;

        //    var d = item.Source.BindingContext;
        //    ChildRegister child = (ChildRegister)d;
        //    StaticClass.GrouthChildID = child.ChildId;
        //    //  string FamilyId = child.ChildId.ToString();
        //    await Navigation.PushAsync(new MonthlyRegisterPage());
        //}
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

            ddlDataMonth.ItemsSource = StaticClass.dataManthFormat;// listdataMonths;
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
        public void BindDataStatus()
        {
           // var ListOfddlStatusCheck = App.DAUtil.GetColumnValuesBytext(12);
            ddlStatusCheck.ItemsSource = StaticClass.listOfStatus;
         //   ddlStatusCheck.SelectedIndex = 0;
            StaticClass.GrouthStatus = ddlStatusCheck.SelectedIndex;
            ddlStatusCheck.SelectedIndex = StaticClass.GrouthStatus == -1 ? 0 : StaticClass.GrouthStatus;
        }
        private void BindList()
        {
         try
            {
                btnPrivious.IsEnabled = false;
                btnPriviousnext.IsEnabled = true;
                int StatusId = 0;
                long id = StaticClass.VillageID;
                //var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                List<ChildMonthlyData> childMonthlyData = new List<ChildMonthlyData>();
                List<ChildMonthlyData> ListChildMonthlyData = new List<ChildMonthlyData>();
             //   if (ListData != null)
               // {
                    var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
                if (selectedStatusId == null)
                {
                  
                  var  statusId = StaticClass.listOfStatus.Where(x => x.columnValueId == 49).FirstOrDefault();
                    StatusId = statusId.columnValueId;
                }
                else
                {
                    StatusId = selectedStatusId.columnValueId;
                }
                 // for(int i=0;i< ListData.Count;i++)
                   // {
                        var DataMId = (DataM)ddlDataMonth.SelectedItem;
                        int DataID = DataMId.Datamonthid;
                        childMonthlyData = App.DAUtil.GetChildMonthlyData(id, StatusId, DataID).Take(10).Where(f=>f.AnyRedFlag==false).ToList();
                       // var FCode = App.DAUtil.FindFamilyId(ListData[i].FamilyId).FirstOrDefault();
                        for (int j = 0; j < childMonthlyData.Count; j++)
                        {
                                ChildMonthlyData MonthlyData = new ChildMonthlyData();
                                MonthlyData.DOB = childMonthlyData[j].DOB.Date;
                                MonthlyData.ChildName = childMonthlyData[j].ChildName;
                                MonthlyData.GrowthId = childMonthlyData[j].GrowthId;
                                MonthlyData.ChildId = childMonthlyData[j].ChildId;
                                MonthlyData.GenderID = childMonthlyData[j].GenderID;
                               MonthlyData.FamilyCode = childMonthlyData[j].FamilyCode; //FCode.FamilyCode;
                               MonthlyData.ChildCode = childMonthlyData[j].ChildCode;
                              MonthlyData.GenderName = childMonthlyData[j].GenderName;
                                //if (childMonthlyData[j].GenderID != 0)
                                //{
                                //    var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                                //    for (int k = 0; k < ListofGender.Count; k++)
                                //    {
                                //        if (ListofGender[k].columnValueId == childMonthlyData[j].GenderID)
                                //        {
                                //            MonthlyData.GenderName = ListofGender[k].columnValue;
                                //        }
                                //    }
                                //}
                                if (childMonthlyData[j].DataMonthId != 0)
                                {
                                    var dateId = App.DAUtil.GetDataMonthByID(childMonthlyData[j].DataMonthId);
                                    if (dateId.Count > 0)
                                        MonthlyData.DataMonth = dateId[0].Datamonth.ToString("MMM-yyyy");
                                    MonthlyData.IsvisuaEdit = true;
                                    MonthlyData.IsvisuaAdd = false;
                                }
                                else
                                {
                                if (childMonthlyData[j].ChildStatus == 49)
                                {
                                    MonthlyData.IsvisuaEdit = false;
                                    MonthlyData.IsvisuaAdd = true;
                                    MonthlyData.DataMonth = "Null";
                                }
                                }
                                ListChildMonthlyData.Add(MonthlyData);
                         }
                // }
                listView.IsVisible = true;
                listView.ItemsSource = ListChildMonthlyData;


                //  }

            }
            catch(Exception EX)
            {

            }

        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Image)sender;
            var d = item.Source.BindingContext;
            ChildMonthlyData Child = (ChildMonthlyData)d;
            StaticClass.GrouthChildID = Child.ChildId;
            StaticClass.GrouthMonthlyId = Child.GrowthId;
            StaticClass.ChildName = Child.ChildName;
           
             StaticClass.PageButtonText = "Update";
             Navigation.PushAsync(new MonthlyRegisterPage());
            
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
           
            var item = (Xamarin.Forms.Image)sender;
           var d = item.Source.BindingContext;
         
            ChildMonthlyData child = (ChildMonthlyData)d;
            StaticClass.GrouthChildID = child.ChildId;
            StaticClass.ChildName = child.ChildName;
           StaticClass.GenderId = child.GenderID;
         StaticClass.PageButtonText = "Save";
            Navigation.PushAsync(new MonthlyRegisterPage());
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
            StaticClass.GrouthStatus = ddlStatusCheck.SelectedIndex;
            BindList();
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;

            ChildMonthlyData child = (ChildMonthlyData)d;
            StaticClass.GrouthChildID = child.ChildId;
            StaticClass.GenderId = child.GenderID;
            StaticClass.ChildName = child.ChildName;
           StaticClass.PageButtonText = "Save";
            Navigation.PushAsync(new MonthlyRegisterPage());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;
            ChildMonthlyData Child = (ChildMonthlyData)d;
            StaticClass.GrouthChildID = Child.ChildId;
            StaticClass.GrouthMonthlyId = Child.GrowthId;
            StaticClass.ChildName = Child.ChildName;
           StaticClass.PageButtonText = "Update";
            Navigation.PushAsync(new MonthlyRegisterPage());
        }

        private void BtnPrivious_Clicked(object sender, EventArgs e)
        {
            if (previousValue > 0)
            {
                // btnPrivious.IsEnabled = true;
                btnPriviousnext.IsEnabled = true;
                previousValue -= 10;
                try
                {
                long id = StaticClass.VillageID;
               // var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                List<ChildMonthlyData> childMonthlyData = new List<ChildMonthlyData>();
                List<ChildMonthlyData> ListChildMonthlyData = new List<ChildMonthlyData>();
              //  if (ListData != null)
              //  {
                    var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
                    int StatusId = selectedStatusId.columnValueId;

                   // for (int i = 0; i < ListData.Count; i++)
                   // {
                        var DataMId = (DataM)ddlDataMonth.SelectedItem;
                        int DataID = DataMId.Datamonthid;
                            childMonthlyData = App.DAUtil.GetChildMonthlyData(id, StatusId, DataID).Skip(previousValue).Take(10).Where(f=>f.AnyRedFlag== false).ToList();
                        //var FCode = App.DAUtil.FindFamilyId(ListData[i].FamilyId).FirstOrDefault();
                        for (int j = 0; j < childMonthlyData.Count; j++)
                        {
                            ChildMonthlyData MonthlyData = new ChildMonthlyData();
                            MonthlyData.DOB = childMonthlyData[j].DOB.Date;
                            MonthlyData.ChildName = childMonthlyData[j].ChildName;
                            MonthlyData.GrowthId = childMonthlyData[j].GrowthId;
                            MonthlyData.ChildId = childMonthlyData[j].ChildId;
                            MonthlyData.GenderID = childMonthlyData[j].GenderID;
                            MonthlyData.FamilyCode = childMonthlyData[j].FamilyCode; //FCode.FamilyCode;
                            MonthlyData.ChildCode = childMonthlyData[j].ChildCode;
                        MonthlyData.GenderName = childMonthlyData[j].GenderName;
                        //if (childMonthlyData[j].GenderID != 0)
                            //{
                            //    var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                            //    for (int k = 0; k < ListofGender.Count; k++)
                            //    {
                            //        if (ListofGender[k].columnValueId == childMonthlyData[j].GenderID)
                            //        {
                            //            MonthlyData.GenderName = ListofGender[k].columnValue;
                            //        }
                            //    }
                            //}
                        if (childMonthlyData[j].DataMonthId != 0)
                            {
                                var dateId = App.DAUtil.GetDataMonthByID(childMonthlyData[j].DataMonthId);
                                if (dateId.Count > 0)
                                    MonthlyData.DataMonth = dateId[0].Datamonth.ToString("MMM-yyyy");
                                MonthlyData.IsvisuaEdit = true;
                                MonthlyData.IsvisuaAdd = false;
                            }
                            else
                            {
                                if (childMonthlyData[j].ChildStatus == 49)
                                {
                                    MonthlyData.IsvisuaEdit = false;
                                    MonthlyData.IsvisuaAdd = true;
                                    MonthlyData.DataMonth = "Null";
                                }
                            }
                            ListChildMonthlyData.Add(MonthlyData);
                        }
                   // }
                        listView.IsVisible = true;
                   // listView.ItemsSource = ListChildMonthlyData;
                    if (ListChildMonthlyData.Count== 0)
                        {
                            btnPrivious.IsEnabled = false;
                        }
                        else
                        {
                            btnPrivious.IsEnabled = true;
                            listView.ItemsSource = null;
                         listView.ItemsSource = ListChildMonthlyData;
                            
                        }
                      
             //   }

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

        private void BtnPriviousnext_Clicked(object sender, EventArgs e)
        {
            if (previousValue >= 0)
            {
                // btnPriviousnext.IsEnabled = true;
                btnPrivious.IsEnabled = true;
                previousValue += 10;
                try
                {
                    long id = StaticClass.VillageID;
                  //  var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                    List<ChildMonthlyData> childMonthlyData = new List<ChildMonthlyData>();
                    List<ChildMonthlyData> ListChildMonthlyData = new List<ChildMonthlyData>();

                    //if (ListData != null)
                   // {
                        var selectedStatusId = (ColumnValue)ddlStatusCheck.SelectedItem;
                        int StatusId = selectedStatusId.columnValueId;

                       // for (int i = 0; i < ListData.Count; i++)
                       // {
                            var DataMId = (DataM)ddlDataMonth.SelectedItem;
                            int DataID = DataMId.Datamonthid;
                            childMonthlyData = App.DAUtil.GetChildMonthlyData(id, StatusId, DataID).Skip(previousValue).Take(10).Where(f=>f.AnyRedFlag==false).ToList();
                            //childMonthlyData = App.DAUtil.GetChildMonthlyData(ListData[i].FamilyId.ToString(), StatusId, DataID).Skip(previousValue).Take(5).OrderByDescending(x => x.FamilyCode).ToList();
                            //var FCode = App.DAUtil.FindFamilyId(ListData[i].FamilyId).FirstOrDefault();
                            for (int j = 0; j < childMonthlyData.Count; j++)
                            {
                                ChildMonthlyData MonthlyData = new ChildMonthlyData();
                                MonthlyData.DOB = childMonthlyData[j].DOB.Date;
                                MonthlyData.ChildName = childMonthlyData[j].ChildName;
                                MonthlyData.GrowthId = childMonthlyData[j].GrowthId;
                                MonthlyData.ChildId = childMonthlyData[j].ChildId;
                                MonthlyData.GenderID = childMonthlyData[j].GenderID;
                                MonthlyData.FamilyCode = childMonthlyData[j].FamilyCode; //FCode.FamilyCode;
                                MonthlyData.ChildCode = childMonthlyData[j].ChildCode;
                                MonthlyData.GenderName = childMonthlyData[j].GenderName;
                                //if (childMonthlyData[j].GenderID != 0)
                                //{
                                //    var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                                //    for (int k = 0; k < ListofGender.Count; k++)
                                //    {
                                //        if (ListofGender[k].columnValueId == childMonthlyData[j].GenderID)
                                //        {
                                //            MonthlyData.GenderName = ListofGender[k].columnValue;
                                //        }
                                //    }
                                //}
                                if (childMonthlyData[j].DataMonthId != 0)
                                {
                                    var dateId = App.DAUtil.GetDataMonthByID(childMonthlyData[j].DataMonthId);
                                    if (dateId.Count > 0)
                                        MonthlyData.DataMonth = dateId[0].Datamonth.ToString("MMM-yyyy");
                                    MonthlyData.IsvisuaEdit = true;
                                    MonthlyData.IsvisuaAdd = false;
                                }
                                else
                                {
                                    if (childMonthlyData[j].ChildStatus == 49)
                                    {
                                        MonthlyData.IsvisuaEdit = false;
                                        MonthlyData.IsvisuaAdd = true;
                                        MonthlyData.DataMonth = "Null";
                                    }
                                }
                                ListChildMonthlyData.Add(MonthlyData);
                            }
                    //    }
                        listView.IsVisible = true;
                   // listView.ItemsSource = ListChildMonthlyData;
                      if (ListChildMonthlyData.Count== 0)
                        {
                            btnPriviousnext.IsEnabled = false;
                           
                        }
                        else
                        {
                            btnPriviousnext.IsEnabled = true;
                            listView.ItemsSource = null;
                        listView.ItemsSource = ListChildMonthlyData;
                    }
                    
                    // }

                }
                catch
                {

                }
            }
           
        }
    }

    public class ChildMonthlyData
    {
        public Guid GrowthId { get; set; }
        public Guid ChildId { get; set; }
        public int DataMonthId { get; set; }
        public string DataMonth { get; set; }
        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public string ChildName { get; set; }
        public DateTime DOB { get; set; }
        public int ChildStatus { get; set; }
        public bool IsvisuaAdd { get; set; }
        public bool IsvisuaEdit { get; set; }
        public bool AnyRedFlag { get; set; }
        public string ChildCode { get; set; }
        public string FamilyCode { get; set; }
        public int LocationId { get; set; }
    }
}