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
	public partial class ListOfRedFlagPage : ContentPage
	{
        int previousValue = 0;
        long id;
        public ListOfRedFlagPage()
		{
			InitializeComponent();
            listView.IsVisible = false;
            this.Title = StaticClass.LocationName;
            BindDatamonths();
            BindList();
          //  BindList();
        }
        public void BindDatamonths()
        {
          //  List<DataM> listdataMonths = new List<DataM>();

            var ListOfDatamonth = StaticClass.dataManths; //App.DAUtil.GetDataMonthsFormate().OrderByDescending(x=>x.Datamonthid).ToList();
            //for (int i = 0; i < ListOfDatamonth.Count; i++)
            //{
            //    DataM dataMonths = new DataM();
            //    dataMonths.Datamonthid = ListOfDatamonth[i].Datamonthid;
            //    string formatted = ListOfDatamonth[i].Datamonth.ToString("MMM-yyyy");
            //    dataMonths.Datamonth = formatted;
            //    listdataMonths.Add(dataMonths);
            //}

            ddlDataMonth.ItemsSource = StaticClass.dataManthFormat; //listdataMonths;
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
        private void BindList()
        {
            try
            {
                long id = StaticClass.VillageID;
                var ListData = App.DAUtil.GetAllFamilyByLocation(id);
            //    List<RedFlagDetails> chilRedFlagData = new List<RedFlagDetails>();
                List<RedFlagDetails> ListChildMonthlyData = new List<RedFlagDetails>();
                if (ListData != null)
                {
                   var DataMId = (DataM)ddlDataMonth.SelectedItem;
                    int DataID = DataMId.Datamonthid;
                    for (int l = 0; l < ListData.Count; l++)
                    {
                        var ChildList = App.DAUtil.FindChildId(ListData[l].FamilyId.ToString());
                        for (int i = 0; i < ChildList.Count; i++)
                        {
                            
                            var chilRedFlagData = App.DAUtil.GetListOfRedFlag(ChildList[i].ChildId.ToString(), DataID).Where(x=>x.AnyRedFlag==true).ToList();
                            if (chilRedFlagData.Count > 0)
                            {
                               RedFlagDetails redFlagDetails = new RedFlagDetails();
                                redFlagDetails.ChildName = ChildList[i].ChildName;
                                redFlagDetails.GrowthId = chilRedFlagData[0].GrowthId;
                                redFlagDetails.ChildId = chilRedFlagData[0].ChildId;
                                redFlagDetails.DateMonthId = chilRedFlagData[0].DataMonthId;

                                var RedflagchildId = App.DAUtil.GetAllRedFlagRegisterById(chilRedFlagData[0].ChildId.ToString()).FirstOrDefault();
                                if (RedflagchildId != null)
                                {
                                    redFlagDetails.RedFlagId = RedflagchildId.RedFlagId;
                                }
                                #region Child Code
                                var checkChildata = App.DAUtil.FindChildByData(chilRedFlagData[0].ChildId.ToString()).FirstOrDefault();
                                redFlagDetails.ChildCode = checkChildata.ChildCode;
                                #endregion
                                #region Gender Name
                                if (chilRedFlagData[0].GenderID != 0)
                                {
                                    var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                                    for (int k = 0; k < ListofGender.Count; k++)
                                    {
                                        if (ListofGender[k].columnValueId == chilRedFlagData[0].GenderID)
                                        {
                                            redFlagDetails.GenderName = ListofGender[k].columnValue;
                                        }
                                    }
                                }
                                #endregion
                                #region Datamonth formate and button hide and show
                                if (chilRedFlagData[0].DataMonthId != 0)
                                {
                                    var dateId = App.DAUtil.GetDataMonthByID(chilRedFlagData[0].DataMonthId);

                                    var ChildRedFlagData = App.DAUtil.GetAllRedFlagRegisterById(chilRedFlagData[0].ChildId.ToString());
                                    if (ChildRedFlagData.Count > 0)
                                    {
                                        if (dateId.Count > 0)
                                            redFlagDetails.DataMonth = dateId[0].Datamonth.ToString("MMM-yyyy");

                                        redFlagDetails.IsvisuaEdit = true;
                                        redFlagDetails.IsvisuaAdd = false;
                                    }
                                    else
                                    {
                                        redFlagDetails.IsvisuaEdit = false;
                                        redFlagDetails.IsvisuaAdd = true;
                                        redFlagDetails.DataMonth = "Null";
                                    }
                                }
                                else
                                {
                                    redFlagDetails.IsvisuaEdit = false;
                                    redFlagDetails.IsvisuaAdd = true;
                                    redFlagDetails.DataMonth = "Null";
                                }
                                #endregion
                                ListChildMonthlyData.Add(redFlagDetails);
                            }
                            //}
                        }

                    }
                    listView.IsVisible = true;
                    listView.ItemsSource = ListChildMonthlyData;
                }

            }
            catch(Exception ex)
            {

            }

        }


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            var item = (Xamarin.Forms.Image)sender;
            var d = item.Source.BindingContext;
            RedFlagDetails Child = (RedFlagDetails)d;
            StaticClass.RedFlagChildId = Child.ChildId;
          //  StaticClass.RedFlagId = Child.RedFlagId;
            StaticClass.PageButtonText = "Update";
            Navigation.PushAsync(new ListOfChildInRedFlag());
            // Navigation.PushAsync(new RedFlagPage());

        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            var item = (Xamarin.Forms.Image)sender;
            var d = item.Source.BindingContext;

            RedFlagDetails child = (RedFlagDetails)d;
            StaticClass.RedFlagChildId = child.ChildId;
            StaticClass.PageButtonText = "Save";
            Navigation.PushAsync(new RedFlagPage());
        }

        private void DdlDataMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            BindList();

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;

            RedFlagDetails child = (RedFlagDetails)d;
            StaticClass.RedFlagChildId = child.ChildId;
            StaticClass.PageButtonText = "Save";
            Navigation.PushAsync(new RedFlagPage());
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
            StaticClass.DataMonthId = DataID;
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;
            RedFlagDetails Child = (RedFlagDetails)d;
            StaticClass.RedFlagChildId = Child.ChildId;
           // StaticClass.RedFlagId = Child.RedFlagId;
            StaticClass.PageButtonText = "Update";
            Navigation.PushAsync(new ListOfChildInRedFlag());
        }

        private void BtnPrivious_Clicked(object sender, EventArgs e)
        {
            if (previousValue >= 0)
            {
                btnPrivious.IsEnabled = true;
                previousValue -= 5;
                try
                {
                long id = StaticClass.VillageID;
                var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                List<RedFlagDetails> ListChildMonthlyData = new List<RedFlagDetails>();
                if (ListData != null)
                {
                    var DataMId = (DataM)ddlDataMonth.SelectedItem;
                    int DataID = DataMId.Datamonthid;
                    for (int l = 0; l < ListData.Count; l++)
                    {
                        var ChildList = App.DAUtil.FindChildId(ListData[l].FamilyId.ToString());
                        for (int i = 0; i < ChildList.Count; i++)
                        {
                            var chilRedFlagData = App.DAUtil.GetListOfRedFlag(ChildList[i].ChildId.ToString(), DataID).Skip(previousValue).Take(5).Where(x => x.AnyRedFlag == true).ToList();
                            if (chilRedFlagData.Count > 0)
                            {
                                RedFlagDetails redFlagDetails = new RedFlagDetails();
                                redFlagDetails.ChildName = ChildList[i].ChildName;
                                redFlagDetails.GrowthId = chilRedFlagData[0].GrowthId;
                                redFlagDetails.ChildId = chilRedFlagData[0].ChildId;
                                redFlagDetails.DateMonthId = chilRedFlagData[0].DataMonthId;
                                var RedflagchildId = App.DAUtil.GetAllRedFlagRegisterById(chilRedFlagData[0].ChildId.ToString()).FirstOrDefault();
                                if (RedflagchildId != null)
                                {
                                    redFlagDetails.RedFlagId = RedflagchildId.RedFlagId;
                                }
                                #region Child Code
                                var checkChildata = App.DAUtil.FindChildByData(chilRedFlagData[0].ChildId.ToString()).FirstOrDefault();
                                redFlagDetails.ChildCode = checkChildata.ChildCode;
                                #endregion
                                #region Gender Name
                                if (chilRedFlagData[0].GenderID != 0)
                                {
                                    var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                                    for (int k = 0; k < ListofGender.Count; k++)
                                    {
                                        if (ListofGender[k].columnValueId == chilRedFlagData[0].GenderID)
                                        {
                                            redFlagDetails.GenderName = ListofGender[k].columnValue;
                                        }
                                    }
                                }
                                #endregion
                                #region Datamonth formate and button hide and show
                                if (chilRedFlagData[0].DataMonthId != 0)
                                {
                                    var dateId = App.DAUtil.GetDataMonthByID(chilRedFlagData[0].DataMonthId);

                                    var ChildRedFlagData = App.DAUtil.GetAllRedFlagRegisterById(chilRedFlagData[0].ChildId.ToString());
                                    if (ChildRedFlagData.Count > 0)
                                    {
                                        if (dateId.Count > 0)
                                            redFlagDetails.DataMonth = dateId[0].Datamonth.ToString("MMM-yyyy");

                                        redFlagDetails.IsvisuaEdit = true;
                                        redFlagDetails.IsvisuaAdd = false;
                                    }
                                    else
                                    {
                                        redFlagDetails.IsvisuaEdit = false;
                                        redFlagDetails.IsvisuaAdd = true;
                                        redFlagDetails.DataMonth = "Null";
                                    }
                                }
                                else
                                {
                                    redFlagDetails.IsvisuaEdit = false;
                                    redFlagDetails.IsvisuaAdd = true;
                                    redFlagDetails.DataMonth = "Null";
                                }
                                #endregion
                                ListChildMonthlyData.Add(redFlagDetails);
                            }
                        }
                    }
                        listView.IsVisible = true;
                        if (ListChildMonthlyData.Count == 0)
                        {
                           btnPriviousnext.IsEnabled = false;
                        }
                        else
                        {
                            btnPriviousnext.IsEnabled = true;
                            listView.ItemsSource = null;
                            listView.ItemsSource = ListChildMonthlyData;
                        }
                    }

            }
            catch (Exception ex)
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
            if (previousValue >=0)
            {
                btnPriviousnext.IsEnabled = true;
                previousValue += 5;
                try
                {
                    long id = StaticClass.VillageID;
                    var ListData = App.DAUtil.GetAllFamilyByLocation(id);
                    List<RedFlagDetails> ListChildMonthlyData = new List<RedFlagDetails>();
                    if (ListData != null)
                    {
                        var DataMId = (DataM)ddlDataMonth.SelectedItem;
                        int DataID = DataMId.Datamonthid;
                        for (int l = 0; l < ListData.Count; l++)
                        {
                            var ChildList = App.DAUtil.FindChildId(ListData[l].FamilyId.ToString());
                            for (int i = 0; i < ChildList.Count; i++)
                            {
                                var chilRedFlagData = App.DAUtil.GetListOfRedFlag(ChildList[i].ChildId.ToString(), DataID).Skip(previousValue).Take(5).Where(x => x.AnyRedFlag == true).ToList();
                                if (chilRedFlagData.Count > 0)
                                {
                                    RedFlagDetails redFlagDetails = new RedFlagDetails();
                                    redFlagDetails.ChildName = ChildList[i].ChildName;
                                    redFlagDetails.GrowthId = chilRedFlagData[0].GrowthId;
                                    redFlagDetails.ChildId = chilRedFlagData[0].ChildId;
                                    redFlagDetails.DateMonthId = chilRedFlagData[0].DataMonthId;
                                    var RedflagchildId = App.DAUtil.GetAllRedFlagRegisterById(chilRedFlagData[0].ChildId.ToString()).FirstOrDefault();
                                    if (RedflagchildId != null)
                                    {
                                        redFlagDetails.RedFlagId = RedflagchildId.RedFlagId;
                                    }
                                    #region Child Code
                                    var checkChildata = App.DAUtil.FindChildByData(chilRedFlagData[0].ChildId.ToString()).FirstOrDefault();
                                    redFlagDetails.ChildCode = checkChildata.ChildCode;
                                    #endregion
                                    #region Gender Name
                                    if (chilRedFlagData[0].GenderID != 0)
                                    {
                                        var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                                        for (int k = 0; k < ListofGender.Count; k++)
                                        {
                                            if (ListofGender[k].columnValueId == chilRedFlagData[0].GenderID)
                                            {
                                                redFlagDetails.GenderName = ListofGender[k].columnValue;
                                            }
                                        }
                                    }
                                    #endregion
                                    #region Datamonth formate and button hide and show
                                    if (chilRedFlagData[0].DataMonthId != 0)
                                    {
                                        var dateId = App.DAUtil.GetDataMonthByID(chilRedFlagData[0].DataMonthId);

                                        var ChildRedFlagData = App.DAUtil.GetAllRedFlagRegisterById(chilRedFlagData[0].ChildId.ToString());
                                        if (ChildRedFlagData.Count > 0)
                                        {
                                            if (dateId.Count > 0)
                                                redFlagDetails.DataMonth = dateId[0].Datamonth.ToString("MMM-yyyy");

                                            redFlagDetails.IsvisuaEdit = true;
                                            redFlagDetails.IsvisuaAdd = false;
                                        }
                                        else
                                        {
                                            redFlagDetails.IsvisuaEdit = false;
                                            redFlagDetails.IsvisuaAdd = true;
                                            redFlagDetails.DataMonth = "Null";
                                        }
                                    }
                                    else
                                    {
                                        redFlagDetails.IsvisuaEdit = false;
                                        redFlagDetails.IsvisuaAdd = true;
                                        redFlagDetails.DataMonth = "Null";
                                    }
                                    #endregion
                                    ListChildMonthlyData.Add(redFlagDetails);
                                }
                            }
                        }
                        listView.IsVisible = true;
                        if (ListChildMonthlyData.Count == 0)
                        {
                            btnPriviousnext.IsEnabled = false;
                        }
                        else
                        {
                            btnPriviousnext.IsEnabled = true;
                            listView.ItemsSource = null;
                            listView.ItemsSource = ListChildMonthlyData;
                        }
                    }

                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                btnPrivious.IsEnabled = false;
            }
        }
    }
    public class RedFlagDetails
    {
        
        public Guid GrowthId { get; set; }
        public Guid ChildId { get; set; }
        public Guid RedFlagId { get; set; }
        public int DateMonthId { get; set; }
        public bool AnyRedFlag { get; set; }
        public string DataMonth { get; set; }
        public int GenderID { get; set; }
        public string GenderName { get; set; }
        public string ChildName { get; set; }
        public DateTime DOB { get; set; }
        public int ChildStatus { get; set; }
        public bool IsvisuaAdd { get; set; }
        public bool IsvisuaEdit { get; set; }
        public string ChildCode { get; set; }
       
    }
}