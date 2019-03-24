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
	public partial class MonthlyRegisterPage : ContentPage
	{
        bool IsValidate = true;
        int TempAgeInDays = 0;
        int Gender = 0;
        public MonthlyRegisterPage ()
		{
			InitializeComponent ();
          //  BindRedFlag();
          //  BingschoolType();
            Bindtakeration();
            
            BindDatamonths();
            BindGender();
            BindIsBreastfeeding();
            BindddlHealthCheckUpDone();
           // BindAnyIllness();
            BindReasonAnthropometryNotTaken();
            BindCurrentlyAttends();
            BindddlReceiveAAY();
            BindReceiveCookedFood();
            //  stack22.IsVisible = false;
            //stack567.IsVisible = false;
          //  BindImmunisationcard();
            BindAdmittedToAWC();
            BindddlHaschildill();
            btnSave.Text = StaticClass.PageButtonText;
            if(StaticClass.PageButtonText== "Update")
            {
                EditData();
            }
            stackthr.IsVisible = false;
            stackcoockedfood.IsVisible =false;
            ShowAndHideStackLayou();
            BindIllness();
           
        }
       
        private void ShowAndHideStackLayou()
        {
            try
            {
                if (txtAgeInDays.Text != null)
                {
                    var entry = txtAgeInDays.Text;
                    int value = Convert.ToInt32(entry);
                    if (value >= 5.99 && value <= 36)
                    {
                        stackthr.IsVisible = true;
                    }
                }
                if (txtAgeInDays.Text != null)
                {
                    var entry = txtAgeInDays.Text;
                    int value = Convert.ToInt32(entry);
                    if (value >= 35.99 && value <= 72)
                    {
                        stackcoockedfood.IsVisible = true;
                    }
                }
            }
            catch
            {

            }
        }
        private void BindIllness()
        {
            var checkChildata = App.DAUtil.FindChildByData(StaticClass.GrouthChildID.ToString());
            if(checkChildata.Count>0)
            {
                txtAnyDisability.Text = checkChildata[0].AnyDisability;
                ddlAnyIllness.Text = checkChildata[0].AnyIllness;
                DateTime dtcurrent = new DateTime();
                dtcurrent= txtMeasurementdate.Date;
               // dtcurrent = DateTime.Now;
                DateTime dtDob = checkChildata[0].DOB;
                    TimeSpan ts = dtcurrent - dtDob;
                int Mts = (dtcurrent.Year - dtDob.Year) * 12 + dtcurrent.Month - dtDob.Month;
                 int Age = ts.Days;
                TempAgeInDays= Age;
                txtAgeInDays.Text = Mts.ToString();
                if(Mts>=6 && Mts<=72)
                {
                    stack27.IsVisible = true;
                }
                else
                {
                    stack27.IsVisible = false;
                }
                
            }
            var selectedGender = (ColumnValue)ddlGender.SelectedItem;
             Gender = selectedGender.columnValueId;
        }
        private void BindGender()
        {
            var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
            ddlGender.ItemsSource = ListofGender;
            for (int i = 0; i < ListofGender.Count; i++)
            {
                if (ListofGender[i].columnValueId ==StaticClass.GenderId )
                {
                    ddlGender.SelectedIndex = i;
                }
            }

        }
        //private void BindImmunisationcard()
        //{
        //    var ListofImmunisationcard = App.DAUtil.GetColumnValuesBytext(68);
        //    ddlImmunisationcard.ItemsSource = ListofImmunisationcard;
        //}
        private void BindddlReceiveAAY()
        {
            var ListofReceiveAAY = App.DAUtil.GetColumnValuesBytext(54);
            ddlReceiveAAY.ItemsSource = ListofReceiveAAY;
        }
        
        private void BindCurrentlyAttends()
        {
            var ListofCurrentlyAttends = App.DAUtil.GetColumnValuesBytext(53);
            ddlCurrentlyAttends.ItemsSource = ListofCurrentlyAttends;
        }
        
        private void BindReasonAnthropometryNotTaken()
        {
            var ListofNotTaken = App.DAUtil.GetColumnValuesBytext(52);
            ddlReasonAnthropometryNotTaken.ItemsSource = ListofNotTaken;
        }
        
        public void BindDatamonths()
        {
            List<DataM> listdataMonths = new List<DataM>();

            var ListOfDatamonth = App.DAUtil.GetDataMonthsFormate();
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
        
        public void BindReceiveCookedFood()
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
            ddlReceiveCookedFood.ItemsSource = ListRedflag;
        }
        public void BindddlHaschildill()
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
            ddlHaschildill.ItemsSource = ListRedflag;
        }
            public void BindAdmittedToAWC()
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
            ddlAdmittedToAWC.ItemsSource = ListRedflag;
        }
        //public void BindAnyIllness()
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
        //    ddlAnyIllness.ItemsSource = ListRedflag;
        //}
        public void BindddlHealthCheckUpDone()
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
            ddlHealthCheckUpDone.ItemsSource = ListRedflag;
        }
        public void BindIsBreastfeeding()
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
            ddlIsBreastfeeding.ItemsSource = ListRedflag;
        }
        //public void BindRedFlag()
        //{
        //    List<RedFlag> ListRedflag = new List<RedFlag>();
        //    RedFlag redFlag = new RedFlag();
        //    redFlag.Id = 1;
        //    redFlag.Name = "Yes";
        //   ListRedflag.Add(redFlag);
        //    RedFlag redFlag1 = new RedFlag();
        //    redFlag1.Id = 0;
        //    redFlag1.Name = "No";
        //    ListRedflag.Add(redFlag1);
        //    ddlAnyRedFlag.ItemsSource = ListRedflag;
        //}
        //public void BingschoolType()
        //{
        //    List<SchoolType> ListOfSchoolType = new List<SchoolType>();
        //    SchoolType schoolType = new SchoolType();
        //    schoolType.Id = 1;
        //    schoolType.Name = "Day Creche";
        //    ListOfSchoolType.Add(schoolType);
        //    SchoolType schoolType1 = new SchoolType();
        //    schoolType1.Id = 2;
        //    schoolType1.Name = "Aanganwadi";
        //    ListOfSchoolType.Add(schoolType1);
        //    SchoolType schoolType2 = new SchoolType();
        //    schoolType2.Id = 3;
        //    schoolType2.Name = "Government School";
        //    ListOfSchoolType.Add(schoolType2);
        //    SchoolType schoolType3 = new SchoolType();
        //    schoolType3.Id = 4;
        //    schoolType3.Name = "Private School";
        //    ListOfSchoolType.Add(schoolType3);
        //    ddlCurrentlyadmitted.ItemsSource = ListOfSchoolType;

        //}
        public void Bindtakeration()
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
            ddlreceivedration.ItemsSource = ListRedflag;
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            IsValidate = true;
             Validation();
            if (IsValidate == true)
            {
                try
                {
                   GrowthRegister growthRegister = new GrowthRegister();
                    if (StaticClass.PageButtonText == "Update")
                    {
                        var CheckMonthlyData = App.DAUtil.GetGrowthRegisterById(StaticClass.GrouthMonthlyId.ToString());
                        growthRegister.GrowthId = CheckMonthlyData[0].GrowthId; //StaticClass.GrouthMonthlyId;
                        growthRegister.ChildId = CheckMonthlyData[0].ChildId; //StaticClass.GrouthChildID;
                        growthRegister.Doe = CheckMonthlyData[0].Doe;
                        App.DAUtil.DeleteGrowthRegisterById(StaticClass.GrouthMonthlyId.ToString());
                        
                    }
                    else
                    {
                        growthRegister.GrowthId = Guid.NewGuid();
                        growthRegister.ChildId = StaticClass.GrouthChildID;
                        growthRegister.Doe = DateTime.Now;
                    }
                   
                    var selectedDataMonth = (DataM)ddlDataMonth.SelectedItem;
                    growthRegister.DataMonthId = selectedDataMonth.Datamonthid;
                    growthRegister.MeasurementDate = txtMeasurementdate.Date;
                    growthRegister.WeightInKg = Convert.ToDecimal(txtwaightkg.Text);
                    growthRegister.LengthHeight = Convert.ToDecimal(txtLHinCMS.Text);
                    growthRegister.MUAC = Convert.ToDecimal(txtMUACincms.Text);
                    
                    growthRegister.Dou = DateTime.Now;
                    growthRegister.CreatedBy = 0;
                    growthRegister.UpdatedBy = 0;
                    var selectedGenderId = (ColumnValue)ddlGender.SelectedItem;
                   
                    growthRegister.GenderID = selectedGenderId == null ? 0 : selectedGenderId.columnValueId;
                    var selectedreceivedration = (RedFlag)ddlreceivedration.SelectedItem;
                    string IsReceiveRation= selectedreceivedration == null ? "No" : selectedreceivedration.Name;
                    growthRegister.ReceiveTakeHomeRation = IsReceiveRation == "Yes" ? true : false;
                    var selectedddlHaschildill = (RedFlag)ddlHaschildill.SelectedItem;
                    string IsddlHaschildill = selectedddlHaschildill == null ? "No" : selectedddlHaschildill.Name;
                    growthRegister.ChildIllPreviousMonth = IsddlHaschildill == "Yes" ? true : false;

                    growthRegister.ImmunisationCard = StaticClass.Immunisation;


                    growthRegister.ReceiveTakeHomeRation = IsReceiveRation == "Yes" ? true : false;
                    var AdmittedToAWC = (RedFlag)ddlAdmittedToAWC.SelectedItem;
                    string IsAdmittedToAWC = AdmittedToAWC == null ? "No" : AdmittedToAWC.Name;
                   // string IsAdmittedToAWC = AdmittedToAWC.Name;
                    growthRegister.AdmittedToAWC = IsAdmittedToAWC == "Yes" ? true : false;
                    // var AnyRedFlag = (RedFlag)ddlAnyRedFlag.SelectedItem;
                    // string IsAnyRedFlagC = AnyRedFlag == null ? "No" : AnyRedFlag.Name;
                    // string IsAdmittedToAWC = AdmittedToAWC.Name;
                    // growthRegister.AnyRedFlag = IsAnyRedFlagC == "Yes" ? true : false;

                    growthRegister.AgeInDays =Convert.ToInt32(txtAgeInDays.Text);
                    var Breastfeeding = (RedFlag)ddlIsBreastfeeding.SelectedItem;
                    string IsBreastfeeding = Breastfeeding == null ? "No" : Breastfeeding.Name;
                    //string IsBreastfeeding = Breastfeeding.Name;
                    growthRegister.IsBreastfeeding = IsBreastfeeding == "Yes" ? true : false;
                    var HealthCheckUpDone = (RedFlag)ddlHealthCheckUpDone.SelectedItem;
                    string IsHealthCheckUpDone = HealthCheckUpDone == null ? "No" : HealthCheckUpDone.Name;
                   // string IsHealthCheckUpDone = HealthCheckUpDone.Name;
                    growthRegister.HealthCheckUpDone = IsHealthCheckUpDone == "Yes" ? true : false;
                    growthRegister.AnyDisability = txtAnyDisability.Text;
                    // var SelectedAnyIllness = (RedFlag)ddlAnyIllness.SelectedItem;
                    //string IsAnyIllness = SelectedAnyIllness == null ? "No" : SelectedAnyIllness.Name;
                    // string IsAnyIllness = SelectedAnyIllness.Name;
                    growthRegister.AnyIllness = ddlAnyIllness.Text;
                    growthRegister.NumberofDaysIll = Convert.ToInt32(txtNumberofDaysIll.Text);
                    growthRegister.TypeOfIllness = txtTypeOfIllness.Text;
                    var selectedReasonAnthropometryNotTaken = (ColumnValue)ddlReasonAnthropometryNotTaken.SelectedItem;
                    growthRegister.ReasonAnthropometryNotTaken = selectedReasonAnthropometryNotTaken == null ? 0 : selectedReasonAnthropometryNotTaken.columnValueId;
                    var selectedddlCurrentlyAttends = (ColumnValue)ddlCurrentlyAttends.SelectedItem;
                    growthRegister.CurrentlyAttends = selectedddlCurrentlyAttends == null ? 0 : selectedddlCurrentlyAttends.columnValueId;
                    var selectedReceiveCookedFood = (RedFlag)ddlReceiveCookedFood.SelectedItem;
                    string IsReceiveCookedFood = selectedReceiveCookedFood == null ? "No" : selectedReceiveCookedFood.Name;
                 //   string IsReceiveCookedFood = selectedReceiveCookedFood.Name;
                    growthRegister.ReceiveCookedFood = IsReceiveCookedFood == "Yes" ? true : false;
                    var selectedReceiveAAY = (ColumnValue)ddlReceiveAAY.SelectedItem;
                    growthRegister.ReceiveAAY = selectedReceiveAAY == null ? 0 : selectedReceiveAAY.columnValueId;
                    growthRegister.Remark = txtremark.Text;
                   var ChildData = App.DAUtil.FindSingleChildDetails(StaticClass.GrouthChildID.ToString());
                    int tempnofdays = TempAgeInDays; //(int)(txtMeasurementdate.Date.Subtract(ChildData[0].DOB).TotalDays);
                    ZScores zScores = new ZScores(ChildData[0].GenderID, Convert.ToInt16(tempnofdays), Convert.ToDouble(txtwaightkg.Text), Convert.ToDouble(txtLHinCMS.Text));
                    CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
                    growthRegister.W4LHZ = calculationvalueClass.W4LHZValue(ChildData[0].GenderID, tempnofdays, Convert.ToDouble(txtLHinCMS.Text), Convert.ToDouble(txtwaightkg.Text));
                    growthRegister.W4AZ = calculationvalueClass.W4AZValue(ChildData[0].GenderID, tempnofdays, Convert.ToDouble(txtwaightkg.Text));
                    growthRegister.H4AZ = calculationvalueClass.H4AZValue(ChildData[0].GenderID,tempnofdays,Convert.ToDouble(txtLHinCMS.Text));
                    growthRegister.BMI = calculationvalueClass.BMIValue(Convert.ToDouble(txtwaightkg.Text),Convert.ToDouble(txtLHinCMS.Text));
                    growthRegister.BMIZ = calculationvalueClass.BMIZValue(ChildData[0].GenderID,tempnofdays, Convert.ToDouble(txtwaightkg.Text), Convert.ToDouble(txtLHinCMS.Text));
                  if(growthRegister.W4LHZ <= -2 || growthRegister.W4AZ <= -2 || growthRegister.H4AZ <= -2 || growthRegister.BMI <= -2 || growthRegister.BMIZ <= -2)
                    {
                        growthRegister.AnyRedFlag =true;
                        growthRegister.IsZScoreRedFlag = true;
                    }
                  else
                    {
                        growthRegister.IsZScoreRedFlag = false;
                        //growthRegister.AnyRedFlag = false;
                    }
                   // growthRegister.Muac = Convert.ToDecimal(txtMUACincms.Text);
                    //txtremark.Text;
                    App.DAUtil.SaveGrowthRegister(growthRegister);
                    StaticClass.PageName = "HomePage";
                    Application.Current.MainPage = new MasterDetailPage1();
                }
                catch(Exception ex)
                {

                }
            }
        }

        private void EditData()
        {
            var checkMonthlydata = App.DAUtil.GetGrowthRegisterById(StaticClass.GrouthMonthlyId.ToString());
            if (checkMonthlydata.Count > 0)
            {
                try
                {
                    var ListOfDatamonth = App.DAUtil.GetDataMonthsFormate();
                    for (int i = 0; i < ListOfDatamonth.Count; i++)
                    {
                        DataM dataMonths = new DataM();
                        dataMonths.Datamonthid = ListOfDatamonth[i].Datamonthid;
                        if (ListOfDatamonth[i].Datamonthid == checkMonthlydata[0].DataMonthId)
                        {
                            ddlDataMonth.SelectedIndex = i;
                        }
                    }
                    txtMeasurementdate.Date = checkMonthlydata[0].MeasurementDate.Date;
                    txtwaightkg.Text = checkMonthlydata[0].WeightInKg.ToString()==null?"": checkMonthlydata[0].WeightInKg.ToString();
                    txtLHinCMS.Text = checkMonthlydata[0].LengthHeight.ToString()==null?"": checkMonthlydata[0].LengthHeight.ToString();
                    txtMUACincms.Text = checkMonthlydata[0].MUAC.ToString()==null?"": checkMonthlydata[0].MUAC.ToString();
                 
                    var GenderId = App.DAUtil.GetColumnValuesBytext(3);
                    for (int i = 0; i < GenderId.Count; i++)
                    {
                        if (GenderId[i].columnValueId == checkMonthlydata[0].GenderID)
                        {
                            ddlGender.SelectedIndex = i;
                        }
                    }
                    ddlHaschildill.SelectedIndex = checkMonthlydata[0].ChildIllPreviousMonth == true ? 0 : 1;
                    ddlreceivedration.SelectedIndex = checkMonthlydata[0].ReceiveTakeHomeRation == true ? 0 : 1;
                   ddlAdmittedToAWC.SelectedIndex = checkMonthlydata[0].AdmittedToAWC == true ? 0 : 1;
                     txtAgeInDays.Text = checkMonthlydata[0].AgeInDays.ToString()==null?"": checkMonthlydata[0].AgeInDays.ToString();
                    ddlIsBreastfeeding.SelectedIndex = checkMonthlydata[0].IsBreastfeeding == true ? 0 : 1;
                    ddlHealthCheckUpDone.SelectedIndex = checkMonthlydata[0].HealthCheckUpDone == true ? 0 : 1;
                  //  txtAnyDisability.Text = checkMonthlydata[0].AnyDisability.ToString();
                   // ddlAnyIllness.SelectedIndex = checkMonthlydata[0].AnyIllness == true ? 0 : 1;
                   //if (ddlAnyIllness.SelectedIndex == 0)
                   // {
                   //     if (txtwaightkg.Text == null && txtwaightkg.Text == "")
                   //     {
                   //         stack567.IsVisible = true;
                   //     }
                   //     if (txtLHinCMS.Text == null && txtLHinCMS.Text == "")
                   //     {
                   //         stack567.IsVisible = true;
                   //     }
                   //     if (txtMUACincms.Text == null && txtMUACincms.Text == "")
                   //     {
                   //         stack567.IsVisible = true;
                   //     }
                   //     stack22.IsVisible = true;
                   // }
                   // else
                   // {
                   //     if (txtwaightkg.Text != null && txtwaightkg.Text != "")
                   //     {
                   //         stack567.IsVisible = false;
                   //     }
                   //     if (txtLHinCMS.Text != null && txtLHinCMS.Text != "")
                   //     {
                   //         stack567.IsVisible = false;
                   //     }
                   //     if (txtMUACincms.Text != null && txtMUACincms.Text != "")
                   //     {
                   //         stack567.IsVisible = false;
                   //     }
                   //     stack22.IsVisible = false;
                   // }
                    txtNumberofDaysIll.Text = checkMonthlydata[0].NumberofDaysIll.ToString()==null?"": checkMonthlydata[0].NumberofDaysIll.ToString();

                    // ddlAnyRedFlag.SelectedIndex = checkMonthlydata[0].AnyRedFlag == true ? 0 : 1;
                    //var ListImmunisationCard = App.DAUtil.GetColumnValuesBytext(68);
                    //for (int i = 0; i < ListImmunisationCard.Count; i++)
                    //{
                    //    if (ListImmunisationCard[i].columnValueId == checkMonthlydata[0].ImmunisationCard)
                    //    {
                    //        ddlImmunisationcard.SelectedIndex = i;
                    //    }
                    //}
                    var ReasonAnthropometryNotTaken = App.DAUtil.GetColumnValuesBytext(52);
                    for (int i = 0; i < ReasonAnthropometryNotTaken.Count; i++)
                    {
                        if (ReasonAnthropometryNotTaken[i].columnValueId == checkMonthlydata[0].ReasonAnthropometryNotTaken)
                        {
                            ddlReasonAnthropometryNotTaken.SelectedIndex = i;
                        }
                    }
                    var CurrentlyAttends = App.DAUtil.GetColumnValuesBytext(53);
                    for (int i = 0; i < CurrentlyAttends.Count; i++)
                    {
                        if (CurrentlyAttends[i].columnValueId == checkMonthlydata[0].CurrentlyAttends)
                        {
                            ddlCurrentlyAttends.SelectedIndex = i;
                        }
                    }
                    ddlReceiveCookedFood.SelectedIndex = checkMonthlydata[0].ReceiveCookedFood == true ? 0 : 1;
                    //if (ddlReceiveCookedFood.SelectedIndex ==0)
                    //{
                    //    stack27.IsVisible = true;
                    //}
                    //else
                    //{
                    //    stack27.IsVisible = false;
                    //}
                    var ReceiveAAY = App.DAUtil.GetColumnValuesBytext(54);
                    for (int i = 0; i < ReceiveAAY.Count; i++)
                    {
                        if (ReceiveAAY[i].columnValueId == checkMonthlydata[0].ReceiveAAY)
                        {
                            ddlReceiveAAY.SelectedIndex = i;
                        }
                    }
                    txtremark.Text = checkMonthlydata[0].Remark.ToString()==null?"": checkMonthlydata[0].Remark.ToString();
                    txtTypeOfIllness.Text = checkMonthlydata[0].TypeOfIllness.ToString() == null ? "" : checkMonthlydata[0].TypeOfIllness.ToString();
                    
                }
                catch (Exception ex)
                {

                }


            }
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
            //if ((RedFlag)ddlAnyRedFlag.SelectedItem == null)
            //{
            //    IsValidate = false;
            //    DependencyService.Get<Toast>().Show("Please select Any Red Flag");
            //    return;
            //}
            if (txtwaightkg.TextColor == Color.Red)
            {
                DependencyService.Get<Toast>().Show("Please Enter valid Weight in kg");
                IsValidate = false;
                return;
            }
            if (txtLHinCMS.TextColor == Color.Red)
            {
                DependencyService.Get<Toast>().Show("Please Enter valid Length/Height in cms");
                IsValidate = false;
                return;
            }
            if (txtMUACincms.TextColor == Color.DarkRed)
            {
                DependencyService.Get<Toast>().Show("Please Enter valid MUAC in cms");
                IsValidate = false;
                return;
            }
            //if ((RedFlag)ddlAnyIllness.SelectedItem == null)
            //{
            //    IsValidate = false;
            //    DependencyService.Get<Toast>().Show("Please select Any Illness");
            //    return;
            //}
        }

        //private void DdlAnyIllness_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var AnyIllness = (RedFlag)ddlAnyIllness.SelectedItem;
        //    string IsAnyIllness = AnyIllness.Name;
        //    if (IsAnyIllness == "Yes")
        //    {
        //        if(txtwaightkg.Text==null&& txtwaightkg.Text=="")
        //        {
        //            stack567.IsVisible = true;
        //        }
        //        if (txtLHinCMS.Text == null && txtLHinCMS.Text == "")
        //        {
        //            stack567.IsVisible = true;
        //        }
        //        if (txtMUACincms.Text == null && txtMUACincms.Text == "")
        //        {
        //            stack567.IsVisible = true;
        //        }
        //        stack22.IsVisible = true;
        //    }
        //    else
        //    {
        //        if (txtwaightkg.Text != null && txtwaightkg.Text != "")
        //        {
        //            stack567.IsVisible = false;
        //        }
        //        if (txtLHinCMS.Text != null && txtLHinCMS.Text != "")
        //        {
        //            stack567.IsVisible = false;
        //        }
        //        if (txtMUACincms.Text != null && txtMUACincms.Text != "")
        //        {
        //            stack567.IsVisible = false;
        //        }
        //        stack22.IsVisible = false;
        //    }
        //}
        private void DdlReceiveCookedFood_SelectedIndexChanged(object sender, EventArgs e)
        {
            //var ReceiveCookedFood = (RedFlag)ddlReceiveCookedFood.SelectedItem;
            //string IsReceiveCookedFood = ReceiveCookedFood.Name;
            //if (IsReceiveCookedFood == "Yes")
            //{
            //  stack27.IsVisible = true;
            //}
            //else
            //{
            //    stack27.IsVisible = false;
            //}
        }

        private void Txtwaightkg_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if (value >= 0 && value <25)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");
                    var match = regex.Match(txtwaightkg.Text);
                    if (match.Success)
                    {
                        txtwaightkg.Text = match.Value;
                        txtwaightkg.TextColor = Color.Black;
                        if (txtwaightkg.Text != "" && txtwaightkg.Text != null)
                        {
                            CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
                            double W4A = calculationvalueClass.W4AZValue(Gender, TempAgeInDays, Convert.ToDouble(txtwaightkg.Text));
                            if (W4A < -3) 
                            {
                                txtw4az.Text = "SUW(Severely Under Weight)";
                            }
                            else
                            {
                                if (W4A <= -2)
                                {
                                    txtw4az.Text = "MUW(Moderately Under Weight)";
                                }
                                else
                                {
                                   
                                    txtw4az.Text = "Normal";
                                   
                                }
                            }
                            H4AZCalculation();
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

        private void TxtLHinCMS_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Regex regex = new Regex(@"^\d{0,3}(\.\d{1,2})?$");
                var match = regex.Match(txtLHinCMS.Text);
                if (match.Success)
                {
                    txtLHinCMS.Text = match.Value;
                    txtLHinCMS.TextColor = Color.Black;
                    if (txtwaightkg.Text != "" && txtwaightkg.Text != null && txtLHinCMS.Text != "" && txtLHinCMS.Text != null)
                    {
                        CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
                        double w4hz = calculationvalueClass.W4LHZValue(Gender, TempAgeInDays, Convert.ToDouble(txtLHinCMS.Text), Convert.ToDouble(txtwaightkg.Text));

                        if (w4hz < -3)
                        {
                            txth4az.Text = "SAM(Severely Acute Malnutrition)";
                        }
                        else
                        {
                            if (w4hz <= -2)
                            {
                                txth4az.Text = "MAM(Moderately Acute Malnutrition)";
                            }
                            else
                            {
                              
                                txth4az.Text = "Normal";
                               
                            }
                        }
                    }
                }
                else
                {
                    txtLHinCMS.TextColor = Color.Red;
                }
               
            }
            catch
            {

            }
        }

        private void H4AZCalculation()
        {
            if (txtwaightkg.Text != "" && txtwaightkg.Text != null && txtLHinCMS.Text != "" && txtLHinCMS.Text != null)
            {
                CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
                double w4hz = calculationvalueClass.W4LHZValue(Gender, TempAgeInDays, Convert.ToDouble(txtLHinCMS.Text), Convert.ToDouble(txtwaightkg.Text));
               if (w4hz < -3)
                {
                    txth4az.Text = "SAM(Severely Acute Malnutrition)";
                }
                else
                {
                    if (w4hz <= -2)
                    {
                        txth4az.Text = "MAM(Moderately Acute Malnutrition)";
                    }
                    else
                    {

                        txth4az.Text = "Normal";

                    }
                }
            }
        }

        private void TxtMUACincms_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if ( value < 11.5)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");
                    var match = regex.Match(txtMUACincms.Text);
                    if (match.Success)
                    {
                        txtMUACincms.Text = match.Value;
                        txtMUACincms.TextColor = Color.Red;
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
                        txtMUACincms.TextColor = Color.Green;
                    }
                    else
                    {

                        txtMUACincms.TextColor = Color.DarkRed;
                    }
                }
                if (value>11.4 && value < 12.6)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");

                    var match = regex.Match(txtMUACincms.Text);
                    if (match.Success)
                    {
                        txtMUACincms.Text = match.Value;
                        txtMUACincms.TextColor = Color.Yellow;
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

        private void DdlHaschildill_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void DdlImmunisationcard_Clicked(object sender, EventArgs e)
        {
            var page = new ImmunisationCardpage();
            await Navigation.PushPopupAsync(page);
        }

        private void TxtMeasurementdate_DateSelected(object sender, DateChangedEventArgs e)
        {
            BindIllness();
        }
    }
}