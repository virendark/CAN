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
	public partial class ListOfChildInRedFlag : ContentPage
	{
		public ListOfChildInRedFlag ()
		{
			InitializeComponent ();
            BindDatamonths();
            BindList();
            this.Title = StaticClass.LocationName;
        }
        public void BindDatamonths()
        {
            List<DataM> listdataMonths = new List<DataM>();

            var ListOfDatamonth = App.DAUtil.GetDataMonthsFormate().OrderByDescending(x => x.Datamonthid).ToList();
            for (int i = 0; i < ListOfDatamonth.Count; i++)
            {
                DataM dataMonths = new DataM();
                dataMonths.Datamonthid = ListOfDatamonth[i].Datamonthid;
                string formatted = ListOfDatamonth[i].Datamonth.ToString("MMM-yyyy");
                dataMonths.Datamonth = formatted;
                listdataMonths.Add(dataMonths);
            }

            ddlDataMonth.ItemsSource = listdataMonths;
            for (int j = 0; j < ListOfDatamonth.Count; j++)
            {
                if (StaticClass.DataMonthId == ListOfDatamonth[j].Datamonthid)
                {
                    ddlDataMonth.SelectedIndex = j;
                }
            }
        }
        private void BindList()
        {
            //if (StaticClass.RedFlagButton == "false")
            //{
            //    btnAdd.IsVisible = false;
            //}
                var DataMId = (DataM)ddlDataMonth.SelectedItem;
            int DataID = DataMId.Datamonthid;
           
            var ChildList = App.DAUtil.GetListOfChildInRedFlagData(StaticClass.RedFlagChildId.ToString(), DataID);
            List<ListOfChildInRedFlagDetails> RedflagList = new List<ListOfChildInRedFlagDetails>(); 
            for (int j = 0; j < ChildList.Count; j++)
             {
                var ChildDetails = App.DAUtil.FindChildByData(StaticClass.RedFlagChildId.ToString()).FirstOrDefault();
                ListOfChildInRedFlagDetails listOfChildInRedFlagDetails = new ListOfChildInRedFlagDetails();
                listOfChildInRedFlagDetails.ChildName = ChildDetails==null?null: ChildDetails.ChildName;
                listOfChildInRedFlagDetails.ChildId = ChildList[j].ChildId;
                listOfChildInRedFlagDetails.DateMonthId = ChildList[j].DateMonthId;
                listOfChildInRedFlagDetails.RedFlagId = ChildList[j].RedFlagId;
                listOfChildInRedFlagDetails.ChildCode = ChildDetails==null?null: ChildDetails.ChildCode;
                if (ChildList[j].OutcomeofReferralbyASHA != 0)
                {
                    var ReferradTo = App.DAUtil.GetColumnValuesBytext(70);
                    for (int i = 0; i < ReferradTo.Count; i++)
                    {
                        if (ReferradTo[i].columnValueId == ChildList[j].OutcomeofReferralbyASHA)
                        {
                            listOfChildInRedFlagDetails.OutcomeofReferralbyASHAName = ReferradTo[i].columnValue;
                        }
                      
                    }
                }
                if (ChildDetails != null)
                {
                    if (ChildDetails.GenderID != 0)
                    {
                        var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                        for (int k = 0; k < ListofGender.Count; k++)
                        {
                            if (ListofGender[k].columnValueId == ChildDetails.GenderID)
                            {
                                listOfChildInRedFlagDetails.GenderName = ListofGender[k].columnValue;
                            }
                        }
                    }
                }
                        if (ChildList[j].DateMonthId != 0)
                        {
                            var dateId = App.DAUtil.GetDataMonthByID(ChildList[j].DateMonthId);

                    listOfChildInRedFlagDetails.DataMonth = dateId[0].Datamonth.ToString("MMM-yyyy");
                    if (StaticClass.RedFlagButton == "true")
                    {
                        listOfChildInRedFlagDetails.IsvisuaEdit = true;
                    }
                    else

                    {
                        listOfChildInRedFlagDetails.IsvisuaEdit = false;
                    }
                        }
                       
                        RedflagList.Add(listOfChildInRedFlagDetails);

                    }
                
              listView.IsVisible = true;
              listView.ItemsSource = RedflagList;
        }
        private void ImageButton_ItemTapped(object sender, EventArgs e)
        {
            StaticClass.PageButtonText = "Save";
            Navigation.PushAsync(new RedFlagPage());
        }

        private void DdlDataMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindList();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Image)sender;
            var d = item.Source.BindingContext;
            ListOfChildInRedFlagDetails Child = (ListOfChildInRedFlagDetails)d;
            StaticClass.RedFlagChildId = Child.ChildId;
            StaticClass.RedFlagId = Child.RedFlagId;
            StaticClass.PageButtonText = "Update";
            Navigation.PushAsync(new RedFlagPage());
            
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;
            ListOfChildInRedFlagDetails Child = (ListOfChildInRedFlagDetails)d;
            StaticClass.RedFlagChildId = Child.ChildId;
            StaticClass.RedFlagId = Child.RedFlagId;
            StaticClass.PageButtonText = "Update";
            Navigation.PushAsync(new RedFlagPage());
        }
    }
    public class ListOfChildInRedFlagDetails
    {
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
        public int  OutcomeofReferralbyASHA { get; set; }
        public string OutcomeofReferralbyASHAName { get; set; }

    }
}