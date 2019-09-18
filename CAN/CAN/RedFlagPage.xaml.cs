using CAN.Helper;
using CAN.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAN
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RedFlagPage : ContentPage
	{
        bool IsValidate = true;
        int Gender = 0;
        DateTime txtDOB;
        int TempAgeInDays = 0;
        double w4az;
        double w4hz;
        public RedFlagPage ()
		{
			InitializeComponent ();
           // BindConsultancyprovidedtoFamily();
            //BindSpecialDietProvided();
            BindWasonMedication();
            BindDatamonths();
          //  BindRefertoHealthCare();
            //BindChildGonetoHealthCenter();
            BindIsEatingSufficiently();
            BindHygeineMaintained();
            //BindAWWVisitWithASHA();
            BindChildSeverelyUnderweightSymptoms();
          //  BindCaseOfReferral();
            BindReferradTo();
            BindChildAdmittedInCTCNRC();
            BindFirstFollowUp();
            BindSecondFollowUp();
          //  BindAdvicedButNotAdmittedReason();
            BindThirdFollowUp();
            BindfourthFollowUp();
            Bindstatus();
            Bindchildill();
            checkFlowUp();
            BindddlIsExtraFeeding();
          //  getChildCode();
            GetAgeInDay();
            //  stack4.IsVisible = false;
            // txtASHAVisitDate1.IsVisible = true;
            //txtASHAVisitDate3.IsVisible = false;
            txtDateOfDischarge11.IsVisible = true;
            txtDateOfDischarge2.IsVisible =false;
            //txtDateOfReferral11.IsVisible = true;
            //txtDateOfReferral2.IsVisible =false;
            txtDateofAdmission11.IsVisible = true;
            txtDateofAdmission2.IsVisible =false;
            BindOutcomeofReferralbyASHA();
            BindddlIsEatingSufficientlybool();
            if (StaticClass.PageButtonText == "Update")
            {
                EditData();
            }
            else
            {
                WeightHeightbindLastRecourd();
            }
            FlagMaxAndMinDate();
        }
        private void FlagMaxAndMinDate()
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day;
            txtDateofAdmission.MaximumDate = new DateTime(year, month, day);
            txtDateOfDischarge.MaximumDate = new DateTime(year, month, day);
            
        }
        public void BindddlIsEatingSufficientlybool()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlIsEatingSufficientlybool.ItemsSource = ListRedflag;
        }
        private void WeightHeightbindLastRecourd()
        {
            var RedFlagRecourd = App.DAUtil.GetAllRedFlagRegisterById(StaticClass.RedFlagChildId.ToString()).LastOrDefault();
            if(RedFlagRecourd!=null)
            {
                txtwaightkg.Text = RedFlagRecourd.WeightInKg.ToString();
                txtLengthHeight.Text = RedFlagRecourd.LengthHeight.ToString();
            }
            else
            {
                txtwaightkg.Text = "";
                txtLengthHeight.Text ="";
            }
        }
        public void Bindchildill()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlChildill.ItemsSource = ListRedflag;
        }
        public void BindfourthFollowUp()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlforthFollowUp.ItemsSource = ListRedflag;
        }
        public void BindddlIsExtraFeeding()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlIsExtraFeeding.ItemsSource = ListRedflag;
        }
        public void BindThirdFollowUp()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlThirdFollowUp.ItemsSource = ListRedflag;
        }
        public void BindOutcomeofReferralbyASHA()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlReferradboolTo.ItemsSource = ListRedflag;
        }
        public void BindSecondFollowUp()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlSecondFollowUp.ItemsSource = ListRedflag;
        }
        public void BindFirstFollowUp()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlFirstFollowUp.ItemsSource = ListRedflag;
        }
        public void BindChildAdmittedInCTCNRC()
        {
            //List<RedFlag> ListRedflag = new List<RedFlag>();
            //RedFlag redFlag = new RedFlag();
            //redFlag.Id = 1;
            //redFlag.Name = "Yes";
            //ListRedflag.Add(redFlag);
            //RedFlag redFlag1 = new RedFlag();
            //redFlag1.Id = 0;
            //redFlag1.Name = "No";
            //ListRedflag.Add(redFlag1);
            var ListChildAdmittedInCTCNRC = App.DAUtil.GetColumnValuesBytext(71);
            ddlChildAdmittedInCTCNRC.ItemsSource = ListChildAdmittedInCTCNRC;
        }
        //public void BindCaseOfReferral()
        //{
        //    List<RedFlag> ListRedflag = new List<RedFlag>();
        //    RedFlag redFlag = new RedFlag();
        //    redFlag.Id = 1;
        //    redFlag.Name = "Yes";
        //    ListRedflag.Add(redFlag);
        //    RedFlag redFlag1 = new RedFlag();
        //    redFlag1.Id = 0;
        //    redFlag1.Name = "No";
        //    ListRedflag.Add(redFlag1);
        //    ddlCaseOfReferral.ItemsSource = ListRedflag;
        //}
        //public void BindAWWVisitWithASHA()
        //    {
        //    List<RedFlag> ListRedflag = new List<RedFlag>();
        //    RedFlag redFlag = new RedFlag();
        //    redFlag.Id = 1;
        //    redFlag.Name = "Yes";
        //    ListRedflag.Add(redFlag);
        //    RedFlag redFlag1 = new RedFlag();
        //    redFlag1.Id = 0;
        //    redFlag1.Name = "No";
        //    ListRedflag.Add(redFlag1);
        //    ddlAWWVisitWithASHA.ItemsSource = ListRedflag;
        //}
        public void BindHygeineMaintained()
        {
            List<RedFlag> ListRedflag = new List<RedFlag>();
            RedFlag redFlag = new RedFlag();
            redFlag.Id = 1;
            redFlag.Name = "Yes";
            ListRedflag.Add(redFlag);
            RedFlag redFlag1 = new RedFlag();
            redFlag1.Id = 0;
            redFlag1.Name = "No";
            ListRedflag.Add(redFlag1);
            ddlHygeineMaintained.ItemsSource = ListRedflag;
        }

        //public void BindChildGonetoHealthCenter()
        //{
        //    List<RedFlag> ListRedflag = new List<RedFlag>();
        //    RedFlag redFlag = new RedFlag();
        //    redFlag.Id = 1;
        //    redFlag.Name = "Yes";
        //    ListRedflag.Add(redFlag);
        //    RedFlag redFlag1 = new RedFlag();
        //    redFlag1.Id = 0;
        //    redFlag1.Name = "No";
        //    ListRedflag.Add(redFlag1);
        //    ddlChildGonetoHealthCenter.ItemsSource = ListRedflag;
        //}
        //private void BindRefertoHealthCare()
        //{
        //    var ListofhealthCenter = App.DAUtil.GetColumnValuesBytext(48);
        //    ddlReferredtohealthCenter.ItemsSource = ListofhealthCenter;
        //}
        private void Bindstatus()
        {
            var Listofstatus = App.DAUtil.GetColumnValuesBytext(69);
            ddlStatus.ItemsSource = Listofstatus;
            //ddlStatus.SelectedIndex = 0;
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
            var MinDate = App.DAUtil.GetDataMonthsFormate().Where(d => d.Datamonthid == StaticClass.DataMonthId).FirstOrDefault();
            int month = MinDate.Datamonth.Month;
            int year = MinDate.Datamonth.Year;
            txtMeasurementDate.MinimumDate = new DateTime(year, month, 1);
            DateTime tempDate = MinDate.Datamonth;
            txtMeasurementDate.MaximumDate = tempDate;
        }
        //public void BindConsultancyprovidedtoFamily()
        //{
        //    List<RedFlag> ListRedflag = new List<RedFlag>();
        //    RedFlag redFlag = new RedFlag();
        //    redFlag.Id = 1;
        //    redFlag.Name = "Yes";
        //    ListRedflag.Add(redFlag);
        //    RedFlag redFlag1 = new RedFlag();
        //    redFlag1.Id = 0;
        //    redFlag1.Name = "No";
        //    ListRedflag.Add(redFlag1);
        //    ddlConsultancyprovidedtoFamily.ItemsSource = ListRedflag;
        //}

        //public void BindSpecialDietProvided()
        //{
        //    List<RedFlag> ListRedflag = new List<RedFlag>();
        //    RedFlag redFlag = new RedFlag();
        //    redFlag.Id = 1;
        //    redFlag.Name = "Yes";
        //    ListRedflag.Add(redFlag);
        //    RedFlag redFlag1 = new RedFlag();
        //    redFlag1.Id = 0;
        //    redFlag1.Name = "No";
        //    ListRedflag.Add(redFlag1);
        //    ddlSpecialDietProvided.ItemsSource = ListRedflag;
        //}
        public void BindWasonMedication()
        {
            var ListofWasonMedication = App.DAUtil.GetColumnValuesBytext(57);
            ddlWasonMedication.ItemsSource = ListofWasonMedication;
        }
        public void BindIsEatingSufficiently()
        {
            var ListofIsEatingSufficiently = App.DAUtil.GetColumnValuesBytext(58);
            ddlIsEatingSufficiently.ItemsSource = ListofIsEatingSufficiently;
        }
        public void BindChildSeverelyUnderweightSymptoms()
        {
           // var ListofIsEatingSufficiently = App.DAUtil.GetColumnValuesBytext(49);
            //ddlChildSeverelyUnderweightSymptoms.ItemsSource = ListofIsEatingSufficiently;
        }
        public void BindReferradTo()
        {
            // var ListofIsEatingSufficiently = App.DAUtil.GetColumnValuesBytext(50);
            var ListofIsEatingSufficiently = App.DAUtil.GetColumnValuesBytext(70);
            ddlReferradTo.ItemsSource = ListofIsEatingSufficiently;
        }
        //public void BindAdvicedButNotAdmittedReason()
        //{
        //    var ListofIsEatingSufficiently = App.DAUtil.GetColumnValuesBytext(51);
        //    ddlAdvicedButNotAdmittedReason.ItemsSource = ListofIsEatingSufficiently;
        //}

        private void DdlReferredtohealthCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
           // var ReferredtohealthCenter = (ColumnValue)ddlReferredtohealthCenter.SelectedItem;
            //string IsReferredtohealthCenter = ReferredtohealthCenter.columnValue;
            //if(IsReferredtohealthCenter=="None"|| IsReferredtohealthCenter=="No")
            //{
            //    stack4.IsVisible = true;
            //}
            //else
            //{
            //    stack4.IsVisible = false;
            //}
        }
        private void Validation()
        {
            var selectedDataMonth = (DataM)ddlDataMonth.SelectedItem;
            int MId = selectedDataMonth == null ? 0 : selectedDataMonth.Datamonthid;
            if (MId == 0)
            {
                IsValidate = false;
                DependencyService.Get<Toast>().Show("Please select Data Month");
                return;
            }
            //if ((ColumnValue)ddlReferredtohealthCenter.SelectedItem == null)
            //{
            //    IsValidate = false;
            //    DependencyService.Get<Toast>().Show("Referred to Health Center");
            //    return;
            //}
            //if ((RedFlag)ddlConsultancyprovidedtoFamily.SelectedItem == null)
            //{
            //    IsValidate = false;txtchildcode
            //    DependencyService.Get<Toast>().Show("Please select Consultancy provided to Family");
            //    return;
            //}

            if (txtwaightkg.TextColor == Color.Red)
            {
                DependencyService.Get<Toast>().Show("Please Enter valid Weight in kg");
                IsValidate = false;
                return;
            }
            if (txtLengthHeight.TextColor == Color.Red)
            {
                DependencyService.Get<Toast>().Show("Please Enter valid Length/Height");
                IsValidate = false;
                return;
            }
            //if (string.IsNullOrEmpty(txtMUACincms.Text))
            //{
            //    DependencyService.Get<Toast>().Show("Please Enter Entry MUAC");
            //    IsValidate = false;
            //    txtMUACincms.Focus();
            //    return;
            //}
            if (txtMUACincms.TextColor == Color.DarkRed)
            {
                DependencyService.Get<Toast>().Show("Please Enter valid  Entry MUAC");
                IsValidate = false;
                return;
            }
            //if (txtMUACincms.TextColor == Color.Red)
            //{
            //    DependencyService.Get<Toast>().Show("Please Enter valid MUAC in cms");
            //    IsValidate = false;
            //    return;
            //}
        }
        private void EditData()
        {
            //getChildCode();
            var checkRedFlagdata = App.DAUtil.GetRedFlagRegisterRegisterById(StaticClass.RedFlagId.ToString());
            if (checkRedFlagdata.Count > 0)
            {
                try
                {
                    var ListOfDatamonth = App.DAUtil.GetDataMonthsFormate().OrderByDescending(x => x.Datamonthid).ToList();
                    for (int i = 0; i < ListOfDatamonth.Count; i++)
                    {
                        DataM dataMonths = new DataM();
                        dataMonths.Datamonthid = ListOfDatamonth[i].Datamonthid;
                        if (ListOfDatamonth[i].Datamonthid == checkRedFlagdata[0].DateMonthId)
                        {
                            ddlDataMonth.SelectedIndex = i;
                        }
                    }
                    //var ReferredtohealthCenter = App.DAUtil.GetColumnValuesBytext(48);
                    //for (int i = 0; i < ReferredtohealthCenter.Count; i++)
                    //{
                    //    if (ReferredtohealthCenter[i].columnValueId == checkRedFlagdata[0].ReferredToHealthCenter)
                    //    {
                    //        ddlReferredtohealthCenter.SelectedIndex = i;
                    //    }
                    //}
                   // ddlChildGonetoHealthCenter.SelectedIndex = checkRedFlagdata[0].ChildGoneToHealthCenter == true ? 0 : 1;
                  //  ddlConsultancyprovidedtoFamily.SelectedIndex = checkRedFlagdata[0].ConsultancyProvidedToFamily == true ? 0 : 1;
                    ddlIsExtraFeeding.SelectedIndex = checkRedFlagdata[0].IsExtraFeeding == true ? 0 : 1;
                    ddlReferradboolTo.SelectedIndex= checkRedFlagdata[0].IsSuggestedReferral == true ? 0 : 1;
                    txtMeasurementDate.Date = checkRedFlagdata[0].MeasurementDate;
                    txtMeasurementDate.IsEnabled = false;
                    txtwaightkg.Text = checkRedFlagdata[0].WeightInKg.ToString();
                    txtLengthHeight.Text = checkRedFlagdata[0].LengthHeight.ToString();
                    txtMUACincms.Text= checkRedFlagdata[0].MUAC.ToString();
                   // ddlSpecialDietProvided.SelectedIndex = checkRedFlagdata[0].SpecialDietProvided == true ? 0 : 1;
                    var WasonMedication = App.DAUtil.GetColumnValuesBytext(57);
                    for (int i = 0; i < WasonMedication.Count; i++)
                    {
                        if (WasonMedication[i].columnValueId == checkRedFlagdata[0].WasOnMedication)
                        {
                            ddlWasonMedication.SelectedIndex = i;
                        }
                    }
                  //  txtObservation.Text = checkRedFlagdata[0].Observation;
                  //  txtDiagnose.Text = checkRedFlagdata[0].Diagnose;
                    var custatus = App.DAUtil.GetColumnValuesBytext(69);
                    for (int i = 0; i < custatus.Count; i++)
                    {
                        if (custatus[i].columnValueId == checkRedFlagdata[0].CurrentStatus)
                        {
                            ddlStatus.SelectedIndex = i;
                        }
                    }
                   // txtCurrentStatus.Text = checkRedFlagdata[0].CurrentStatus;
                   // txtAnyBlockerInLastDiagnose.Text = checkRedFlagdata[0].AnyBlockerInLastDiagnose;
                   //if (checkRedFlagdata[0].ASHAVisitDate==null)
                   // {
                   //     txtASHAVisitDate1.IsVisible = true;
                   //     txtASHAVisitDate3.IsVisible = false;
                   // }
                   // else
                   // {
                   //     txtASHAVisitDate1.IsVisible = false;
                   //     txtASHAVisitDate3.IsVisible = true;
                   //     txtASHAVisitDate.Date =  Convert.ToDateTime(checkRedFlagdata[0].ASHAVisitDate);
                   // }
                    var EatingSufficiently = App.DAUtil.GetColumnValuesBytext(58);
                    for (int i = 0; i < EatingSufficiently.Count; i++)
                    {
                        if (EatingSufficiently[i].columnValueId == checkRedFlagdata[0].IsEatingSufficiently)
                        {
                            ddlIsEatingSufficiently.SelectedIndex = i;
                        }
                    }
                    ddlHygeineMaintained.SelectedIndex = checkRedFlagdata[0].HygeineMaintained == true ? 0 : 1;
                   // ddlAWWVisitWithASHA.SelectedIndex = checkRedFlagdata[0].AWWVisitWithASHA == true ? 0 : 1;
                    //var ChildSeverelyUnderweightSymptoms = App.DAUtil.GetColumnValuesBytext(49);
                    //for (int i = 0; i < ChildSeverelyUnderweightSymptoms.Count; i++)
                    //{
                    //    if (ChildSeverelyUnderweightSymptoms[i].columnValueId == checkRedFlagdata[0].ChildSeverelyUnderweightSymptoms)
                    //    {
                    //        ddlChildSeverelyUnderweightSymptoms.SelectedIndex = i;
                    //    }
                    //}
                    //ddlCaseOfReferral.SelectedIndex = checkRedFlagdata[0].CaseOfReferral == true ? 0 : 1;
                    //if (checkRedFlagdata[0].DateOfReferral == null)
                    //{
                        

                    //    txtDateOfReferral11.IsVisible = true;
                    //    txtDateOfReferral2.IsVisible = false;
                    //}
                    //else
                    //{
                    //    txtDateOfReferral11.IsVisible = false;
                    //    txtDateOfReferral2.IsVisible = true;
                    //    txtDateOfReferral.Date = Convert.ToDateTime(checkRedFlagdata[0].DateOfReferral);
                    //}
                   
                    //var ReferradTo = App.DAUtil.GetColumnValuesBytext(70);
                    //for (int i = 0; i < ReferradTo.Count; i++)
                    //{
                    //    if (ReferradTo[i].columnValueId == checkRedFlagdata[0].OutcomeofReferralbyASHA)
                    //    {
                    //        ddlReferradTo.SelectedIndex = i;
                    //    }
                    //}
                    var ChildAdmittedInCTCNRC = App.DAUtil.GetColumnValuesBytext(71);
                    for (int i = 0; i < ChildAdmittedInCTCNRC.Count; i++)
                    {
                        if (ChildAdmittedInCTCNRC[i].columnValueId == checkRedFlagdata[0].ChildAdmittedInCTCNRC)
                        {
                            ddlChildAdmittedInCTCNRC.SelectedIndex = i;
                        }
                    }
                  //  ddlChildAdmittedInCTCNRC.SelectedIndex = checkRedFlagdata[0].ChildAdmittedInCTCNRC == true ? 0 : 1;
                
                    if (checkRedFlagdata[0].DateofAdmission == null)
                    {
                        txtDateofAdmission11.IsVisible = true;
                        txtDateofAdmission2.IsVisible = false;
                    }
                    else
                    {
                        txtDateofAdmission11.IsVisible = false;
                        txtDateofAdmission2.IsVisible =true;
                        txtDateofAdmission.Date = Convert.ToDateTime(checkRedFlagdata[0].DateofAdmission);
                    }
                    if (checkRedFlagdata[0].DateOfDischarge == null)
                    {

                        txtDateOfDischarge11.IsVisible = true;
                        txtDateOfDischarge2.IsVisible = false;
                    }
                    else
                    {
                        txtDateOfDischarge11.IsVisible = false;
                        txtDateOfDischarge2.IsVisible =true;
                        txtDateOfDischarge.Date = Convert.ToDateTime(checkRedFlagdata[0].DateOfDischarge);
                    }
                    ddlforthFollowUp.SelectedIndex = checkRedFlagdata[0].FourthFollowUp == true ? 0 : 1;
                    ddlFirstFollowUp.SelectedIndex = checkRedFlagdata[0].FirstFollowUp == true ? 0 : 1;
                    ddlSecondFollowUp.SelectedIndex = checkRedFlagdata[0].SecondFollowUp == true ? 0 : 1;
                    ddlThirdFollowUp.SelectedIndex = checkRedFlagdata[0].ThirdFollowUp == true ? 0 : 1;
                    ddlChildill.SelectedIndex = checkRedFlagdata[0].IsCurrentlyIll == true ? 0 : 1;
                    ddlIsEatingSufficientlybool.SelectedIndex = checkRedFlagdata[0].IsEatingSufficientlyBool == true ? 0 : 1;
                    txtRemark.Text = checkRedFlagdata[0].Remark;
                    //var AdvicedButNotAdmittedReason = App.DAUtil.GetColumnValuesBytext(51);
                    //for (int i = 0; i < AdvicedButNotAdmittedReason.Count; i++)
                    //{
                    //    if (AdvicedButNotAdmittedReason[i].columnValueId == checkRedFlagdata[0].AdvicedButNotAdmittedReason)
                    //    {
                    //        ddlAdvicedButNotAdmittedReason.SelectedIndex = i;
                    //    }
                    //}

                }
                catch (Exception ex)
                {

                }


            }
        }
        private void checkFlowUp()
        {
            var GenderId = App.DAUtil.FindChildByData(StaticClass.RedFlagChildId.ToString());
            var checkRedflagdata = App.DAUtil.GetRedFlagRegistercheckFlowup(StaticClass.RedFlagChildId.ToString(), StaticClass.DataMonthId);
            if (GenderId.Count>0)
            {
                Gender = GenderId[0].GenderID;
                txtDOB = GenderId[0].DOB;
            }
            if (checkRedflagdata.Count>0)
            {
                ddlforthFollowUp.IsEnabled = true;
                ddlThirdFollowUp.IsEnabled = true;
                ddlSecondFollowUp.IsEnabled = true;
                ddlFirstFollowUp.IsEnabled = true;
                for (int i=0;i< checkRedflagdata.Count;i++)
                {
                    if (checkRedflagdata[i].FirstFollowUp == true)
                    {
                        ddlFirstFollowUp.IsEnabled = false;
                        ddlFirstFollowUp.SelectedIndex =0;
                    }
                    if (checkRedflagdata[i].SecondFollowUp == true)
                    {
                        ddlSecondFollowUp.IsEnabled = false;
                        ddlSecondFollowUp.SelectedIndex = 0;
                    }
                    if (checkRedflagdata[i].ThirdFollowUp == true)
                    {
                        ddlThirdFollowUp.IsEnabled = false;
                        ddlThirdFollowUp.SelectedIndex = 0;
                    }
                    if (checkRedflagdata[i].FourthFollowUp == true)
                    {
                        ddlforthFollowUp.IsEnabled = false;
                        ddlforthFollowUp.SelectedIndex = 0;
                    }
                }
            }
            
        }
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            IsValidate = true;
            Validation();
            if (IsValidate == true)
            {
                try
                {
                    RedFlagRegister redFlagRegister = new RedFlagRegister();
                    if (StaticClass.PageButtonText == "Update")
                    {
                        var checkRedflagdata = App.DAUtil.GetRedFlagRegisterRegisterById(StaticClass.RedFlagId.ToString());
                        redFlagRegister.RedFlagId = checkRedflagdata[0].RedFlagId;//StaticClass.RedFlagId;
                        redFlagRegister.ChildId = checkRedflagdata[0].ChildId; //StaticClass.RedFlagChildId;
                        redFlagRegister.Doe = checkRedflagdata[0].Doe;// DateTime.Now;
                        App.DAUtil.DeleteRedFlagRegisterRegisterById(StaticClass.RedFlagId.ToString());
                        
                    }
                    else
                    {
                        redFlagRegister.RedFlagId = Guid.NewGuid();
                        redFlagRegister.ChildId = StaticClass.RedFlagChildId;
                        redFlagRegister.Doe = DateTime.Now;
                    }
                   var selectedDataMonth = (DataM)ddlDataMonth.SelectedItem;
                    redFlagRegister.DateMonthId = selectedDataMonth.Datamonthid;
                    //var ReferredtohealthCenter = (ColumnValue)ddlReferredtohealthCenter.SelectedItem;
                    redFlagRegister.ReferredToHealthCenter = null;// ReferredtohealthCenter == null ? 0 : ReferredtohealthCenter.columnValueId;
                    var Childill = (RedFlag)ddlChildill.SelectedItem;
                    string IsChildill = Childill == null ? "No" : Childill.Name;
                    redFlagRegister.IsCurrentlyIll = IsChildill == "Yes" ? true : false;
                    var Extrafee = (RedFlag)ddlIsExtraFeeding.SelectedItem;
                    string isExtrafee = Extrafee == null ? "No" : Extrafee.Name;
                    redFlagRegister.IsExtraFeeding = isExtrafee == "Yes" ? true : false;
                    //  var ChildGonetoHealthCenter = (RedFlag)ddlChildGonetoHealthCenter.SelectedItem;
                    //string IsChildGonetoHealthCenter = ChildGonetoHealthCenter == null ? "No" : ChildGonetoHealthCenter.Name;
                    redFlagRegister.ChildGoneToHealthCenter = null; //IsChildGonetoHealthCenter == "Yes" ? true : false;
                                                                    // var ConsultancyprovidedtoFamily = (RedFlag)ddlConsultancyprovidedtoFamily.SelectedItem;
                                                                    //string IsConsultancyprovidedtoFamily = ConsultancyprovidedtoFamily == null ? "No" : ConsultancyprovidedtoFamily.Name;
                    redFlagRegister.ConsultancyProvidedToFamily = null; //IsConsultancyprovidedtoFamily == "Yes" ? true : false;
                    redFlagRegister.MeasurementDate = txtMeasurementDate.Date;
                    if (!string.IsNullOrEmpty(txtwaightkg.Text))
                    {
                        redFlagRegister.WeightInKg = Convert.ToDecimal(txtwaightkg.Text);
                    }
                    if (!string.IsNullOrEmpty(txtLengthHeight.Text))
                    {
                        redFlagRegister.LengthHeight = Convert.ToDecimal(txtLengthHeight.Text);
                    }
                    if (!string.IsNullOrEmpty(txtMUACincms.Text))
                    {
                        redFlagRegister.MUAC = Convert.ToDecimal(txtMUACincms.Text);
                    }
                    // var SpecialDietProvided = (RedFlag)ddlSpecialDietProvided.SelectedItem;
                    //string IsSpecialDietProvided = SpecialDietProvided == null ? "No" : SpecialDietProvided.Name;
                    redFlagRegister.SpecialDietProvided = null; //IsSpecialDietProvided == "Yes" ? true : false;
                    var WasonMedication = (ColumnValue)ddlWasonMedication.SelectedItem;
                    redFlagRegister.WasOnMedication = WasonMedication == null ? 0 : WasonMedication.columnValueId;
                    redFlagRegister.Observation = null;//txtObservation.Text;
                    redFlagRegister.Diagnose = null;//txtDiagnose.Text;
                    var Status = (ColumnValue)ddlStatus.SelectedItem;
                    redFlagRegister.CurrentStatus = Status == null ? 0 : Status.columnValueId;
                    // redFlagRegister.CurrentStatus = txtCurrentStatus.Text;
                    redFlagRegister.AnyBlockerInLastDiagnose = null; //txtAnyBlockerInLastDiagnose.Text;
                    //if (txtASHAVisitDate3.IsVisible == true)
                    //{
                    //    redFlagRegister.ASHAVisitDate = txtASHAVisitDate.Date;
                    //}
                    //else
                    //{
                    //    redFlagRegister.ASHAVisitDate = null;

                    //}
                    redFlagRegister.ASHAVisitDate = null;
                    var EatingSufficiently = (ColumnValue)ddlIsEatingSufficiently.SelectedItem;
                    redFlagRegister.IsEatingSufficiently = EatingSufficiently == null ? 0 : EatingSufficiently.columnValueId;
                    var HygeineMaintained = (RedFlag)ddlHygeineMaintained.SelectedItem;
                    string IsHygeineMaintained = HygeineMaintained == null ? "No" : HygeineMaintained.Name;
                    redFlagRegister.HygeineMaintained = IsHygeineMaintained == "Yes" ? true : false;

                    var EatingSufficientlybool = (RedFlag)ddlIsEatingSufficientlybool.SelectedItem;
                    string IsEatingSufficientlybool = EatingSufficientlybool == null ? "No" : EatingSufficientlybool.Name;
                    redFlagRegister.IsEatingSufficientlyBool = IsEatingSufficientlybool == "Yes" ? true : false;
                    // var AWWVisitWithASHA = (RedFlag)ddlAWWVisitWithASHA.SelectedItem;
                    // string IsAWWVisitWithASHA = AWWVisitWithASHA == null ? "No" : AWWVisitWithASHA.Name;
                    redFlagRegister.AWWVisitWithASHA = null; //IsAWWVisitWithASHA == "Yes" ? true : false;
                                                             //  var ChildSeverelyUnderweightSymptoms = (ColumnValue)ddlChildSeverelyUnderweightSymptoms.SelectedItem;
                    redFlagRegister.ChildSeverelyUnderweightSymptoms = StaticClass.ChildSeverlysymtoms;//ChildSeverelyUnderweightSymptoms == null ? 0 : ChildSeverelyUnderweightSymptoms.columnValueId;
                    redFlagRegister.CaseOfReferral = null;
                    redFlagRegister.DateOfReferral = null;
                    //var CaseOfReferral = (RedFlag)ddlCaseOfReferral.SelectedItem;
                    //string IsCaseOfReferral = CaseOfReferral == null ? "No" : CaseOfReferral.Name;
                    //redFlagRegister.CaseOfReferral = IsCaseOfReferral == "Yes" ? true : false;
                    //if (txtDateOfReferral2.IsVisible == true)
                    //{
                    //    redFlagRegister.DateOfReferral = txtDateOfReferral.Date;
                    //}
                    //else
                    //{
                    //    redFlagRegister.DateOfReferral = null;

                    //}

                    var ReferradTo = (ColumnValue)ddlReferradTo.SelectedItem;
                    redFlagRegister.ReferradTo = ReferradTo == null ? 0 : ReferradTo.columnValueId;

                    var IsSuggestedReferral = (RedFlag)ddlReferradboolTo.SelectedItem;
                    string SuggestedReferral = IsSuggestedReferral == null ? "No" : IsSuggestedReferral.Name;
                    redFlagRegister.IsSuggestedReferral = SuggestedReferral == "Yes" ? true : false;

                    // redFlagRegister.OutcomeofReferralbyASHA = ReferradTo == null ? 0 : ReferradTo.columnValueId;
                    //redFlagRegister.ReferradTo = 0;
                    // var ChildAdmittedInCTCNRC = (RedFlag)ddlChildAdmittedInCTCNRC.SelectedItem;
                    // string IsChildAdmittedInCTCNRC = ChildAdmittedInCTCNRC == null ? "No" : ChildAdmittedInCTCNRC.Name;
                    var IsChildAdmittedInCTCNRC = (ColumnValue)ddlChildAdmittedInCTCNRC.SelectedItem;
                    redFlagRegister.ChildAdmittedInCTCNRC = IsChildAdmittedInCTCNRC == null ? 0 : IsChildAdmittedInCTCNRC.columnValueId;
                   // redFlagRegister.ChildAdmittedInCTCNRC = IsChildAdmittedInCTCNRC == "Yes" ? true : false;
                    
                    if (txtDateofAdmission2.IsVisible == true)
                    {
                        redFlagRegister.DateofAdmission = txtDateofAdmission.Date;
                    }
                    else
                    {
                        redFlagRegister.DateofAdmission = null;
                    }
                    if (txtDateOfDischarge2.IsVisible == true)
                    {
                        redFlagRegister.DateOfDischarge = txtDateOfDischarge.Date;
                    }
                    else
                    {
                        redFlagRegister.DateOfDischarge = null;

                    }
                 
                    var FirstFollowUp = (RedFlag)ddlFirstFollowUp.SelectedItem;
                    string IsFirstFollowUp = FirstFollowUp == null ? "No" : FirstFollowUp.Name;
                    redFlagRegister.FirstFollowUp = IsFirstFollowUp == "Yes" ? true : false;
                    var SecondFollowUp = (RedFlag)ddlSecondFollowUp.SelectedItem;
                    string IsSecondFollowUp = SecondFollowUp == null ? "No" : SecondFollowUp.Name;
                    redFlagRegister.SecondFollowUp = IsSecondFollowUp == "Yes" ? true : false;
                    var ThirdFollowUp = (RedFlag)ddlThirdFollowUp.SelectedItem;
                    string IsThirdFollowUp = ThirdFollowUp == null ? "No" : ThirdFollowUp.Name;
                    redFlagRegister.ThirdFollowUp = IsThirdFollowUp == "Yes" ? true : false;
                    var fourthFollowUp = (RedFlag)ddlforthFollowUp.SelectedItem;
                    string IsfourthFollowUp = fourthFollowUp == null ? "No" : fourthFollowUp.Name;
                    redFlagRegister.FourthFollowUp = IsfourthFollowUp == "Yes" ? true : false;
                    // var AdvicedButNotAdmittedReason = (ColumnValue)ddlAdvicedButNotAdmittedReason.SelectedItem;
                    redFlagRegister.AdvicedButNotAdmittedReason = null; //AdvicedButNotAdmittedReason == null ? 0 : AdvicedButNotAdmittedReason.columnValueId;
                    redFlagRegister.Remark = txtRemark.Text;


                    redFlagRegister.Dou = DateTime.Now;
                    redFlagRegister.CreatedBy = 0;
                    redFlagRegister.UpdatedBy = 0;
                   
                    App.DAUtil.SaveRedFlagRegister(redFlagRegister);
                    StaticClass.PageName = "HomePage";
                    StaticClass.ChildSeverlysymtoms = null;
                    StaticClass.TabbedIndex = 3;
                    Application.Current.MainPage = new MasterNavigationPage();
                }
                catch (Exception ex)
                {

                }
            }
            }
        private void GetAgeInDay()
        {
            try
            {
                  DateTime dtcurrent = new DateTime();
                    dtcurrent = txtMeasurementDate.Date;
                    // dtcurrent = DateTime.Now;
                    DateTime dtDob = txtDOB;
                TimeSpan ts = dtcurrent - dtDob;
                    int Mts = (dtcurrent.Year - dtDob.Year) * 12 + dtcurrent.Month - dtDob.Month;
                    int Age = ts.Days;
                    TempAgeInDays = Age;
                    txtAgeinmonths.Text = Mts.ToString();
                   
            }
            catch
            {

            }
        }
        private void H4AZCalculation()
        {
            if (txtwaightkg.Text != "" && txtwaightkg.Text != null && txtLengthHeight.Text != "" && txtLengthHeight.Text != null)
            {
                CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
                double? w4hz = calculationvalueClass.W4LHZValue(Gender, TempAgeInDays, Convert.ToDouble(txtLengthHeight.Text), Convert.ToDouble(txtwaightkg.Text));
                if (w4hz != null)
                { 
                    if (w4hz < -3)
                    {
                        txtgradeasperheight.Text = "SAM(Severely Acute Malnutrition)";
                        txtgradeasperheight.TextColor = Color.Black;
                        txtgradeasperheight.BackgroundColor = Color.Red;
                    }
                    else
                    {
                        if (w4hz <= -2)
                        {
                            txtgradeasperheight.Text = "MAM(Moderately Acute Malnutrition)";
                            txtgradeasperheight.TextColor = Color.Black;
                            txtgradeasperheight.BackgroundColor = Color.Yellow;
                        }
                        else
                        {

                            txtgradeasperheight.Text = "Normal";
                            txtgradeasperheight.TextColor = Color.Black;
                            txtgradeasperheight.BackgroundColor = Color.Green;

                        }
                    }
            }
            }
        }
        private void Txtwaightkg_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if (value >= 2 && value < 26)
                {

                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");
                    var match = regex.Match(txtwaightkg.Text);
                    if (match.Success)
                    {
                        try
                        {
                            txtwaightkg.Text = match.Value;
                            txtwaightkg.TextColor = Color.Black;
                            if (txtwaightkg.Text != "" && txtwaightkg.Text != null)
                            {
                              CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
                           double? W4AZ = calculationvalueClass.W4AZValue(Gender, TempAgeInDays, Convert.ToDouble(txtwaightkg.Text));
                                if (W4AZ != null)
                                {
                                    w4az = Math.Round(W4AZ.Value);
                                    if (w4az < -3)
                                    {
                                        txtgradeasperweight.Text = "SUW(Severely Under Weight)";
                                        txtgradeasperweight.TextColor = Color.Black;
                                        txtgradeasperweight.BackgroundColor = Color.Red;
                                    }
                                    else
                                    {
                                        if (w4az <= -2)
                                        {
                                            txtgradeasperweight.Text = "MUW(Moderately Under Weight)";
                                            txtgradeasperweight.TextColor = Color.Black;
                                            txtgradeasperweight.BackgroundColor = Color.Yellow;

                                        }
                                        else
                                        {

                                            txtgradeasperweight.Text = "Normal";
                                            txtgradeasperweight.TextColor = Color.Black;
                                            txtgradeasperweight.BackgroundColor = Color.Green;


                                        }
                                    }
                                    H4AZCalculation();
                                }
                            }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        txtwaightkg.TextColor = Color.Red;
                    }
                }
                else
                {
                    txtwaightkg.TextColor = Color.Red;
                }
            }
            catch
            {

            }
            
        }

        private void TxtLengthHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            
            try
            {
                Regex regex = new Regex(@"^\d{0,3}(\.\d{1,3})?$");
                var match = regex.Match(txtLengthHeight.Text);
                if (match.Success)
                {
                    txtLengthHeight.Text = match.Value;
                    txtLengthHeight.TextColor = Color.Black;
                    H4AZCalculation();
                }
                else
                {
                    txtLengthHeight.TextColor = Color.Red;
                }
            }
            catch
            {

            }
        }

        private void TxtMUACincms_TextChanged(object sender, TextChangedEventArgs e)
        {
           
           
            try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if (value < 11.5)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");
                    var match = regex.Match(txtMUACincms.Text);
                    if (match.Success)
                    {
                        txtMUACincms.Text = match.Value;
                        txtMUACincms.BackgroundColor = Color.Red;
                        txtMUACincms.TextColor = Color.Black;
                    }
                    else
                    {

                        txtMUACincms.TextColor = Color.DarkRed;

                    }
                }
                if (value > 12.5)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");

                    var match = regex.Match(txtMUACincms.Text);
                    if (match.Success)
                    {
                        txtMUACincms.Text = match.Value;
                        txtMUACincms.BackgroundColor = Color.Green;
                        txtMUACincms.TextColor = Color.Black;
                    }
                    else
                    {

                        txtMUACincms.TextColor = Color.DarkRed;
                    }
                }
                if (value > 11.4 && value < 12.6)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");

                    var match = regex.Match(txtMUACincms.Text);
                    if (match.Success)
                    {
                        txtMUACincms.Text = match.Value;
                        txtMUACincms.BackgroundColor = Color.Yellow;
                        txtMUACincms.TextColor = Color.Black;
                    }
                    else
                    {

                        txtMUACincms.TextColor = Color.DarkRed;
                    }
                }
            }
            catch
            {

            }

        }

        //private void TxtASHAVisitDate_DateSelected(object sender, DateChangedEventArgs e)
        //{
           
        //        txtASHAVisitDate1.IsVisible = false;
        //    txtASHAVisitDate3.IsVisible = true;
        //}

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    txtASHAVisitDate.Focus();
        //}

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            txtDateOfDischarge.Focus();

        }

        private void TxtDateOfDischarge_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtDateOfDischarge11.IsVisible = false;
            txtDateOfDischarge2.IsVisible = true;
        }

        //private void TxtDateOfReferral_DateSelected(object sender, DateChangedEventArgs e)
        //{
        //    txtDateOfReferral11.IsVisible = false;
        //    txtDateOfReferral2.IsVisible = true;
        //}

        //private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        //{
        //    txtDateOfReferral.Focus();
        //}

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            txtDateofAdmission.Focus();
        }

        private void TxtDateofAdmission_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtDateofAdmission11.IsVisible = false;
            txtDateofAdmission2.IsVisible = true;
        }
        //private void getChildCode()
        //{
        //    var checkChildata = App.DAUtil.FindChildByData(StaticClass.RedFlagChildId.ToString());
        //    txtchildcode.Text = checkChildata[0].ChildCode;
        //}

        private void TxtMeasurementDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                GetAgeInDay();
            }
            catch
            {

            }
        }

        private void DdlChildAdmittedInCTCNRC_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch
            {

            }
        }

        private async void TxtChildSeverelyUnderweightSymptoms_Clicked(object sender, EventArgs e)
        {
            var page = new ChildSeverelyUnderweightSymptoms();
            await Navigation.PushPopupAsync(page);
        }

        private void TxtAgeinmonths_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TxtLengthHeight_Unfocused(object sender, FocusEventArgs e)
        {
            if (Convert.ToDouble(txtLengthHeight.Text) <= 45)
            {
                txtLengthHeight.Text = "45";
            }
            else
            {
                if (Convert.ToDouble(txtLengthHeight.Text) <= 120 && Convert.ToDouble(txtLengthHeight.Text) > 45)
                {

                }
                else
                {
                    txtLengthHeight.Text = "120";
                }
            }
        }
    }
}