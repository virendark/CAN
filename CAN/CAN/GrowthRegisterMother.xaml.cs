using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CAN.Models;
using Rg.Plugins.Popup.Extensions;

namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GrowthRegisterMother : ContentPage
    {
        bool IsValidation = true;
        DateTime? lastdDate = new DateTime?();
        DateTime? Expectdate = new DateTime?();
        int DDCheck = 0;
        public GrowthRegisterMother()
        {
            InitializeComponent();
            showandhidePageLoadStacklayout();
            this.Title = StaticClass.LocationName;
            BindDatamonths();
            BindddChildExpected();
            BindddlIsLactating();

            BindddlReceivedDietUnderAay();
            BindddlGetMealAwcUnderAay();
            BindddlReceivedMealFromAwcunderAay();
            BindddlTypeOfMeal();
            BindddlEggReceived();
            BindddlReceivedSnacksUnderAay();
            BindddlReceivedSnacksFromAwc();
            BindddlEatMealRegularlyFromAwc();
            BindddlIsMealEnough();
            BindddlQualityOfAwcfood();
            BindddlResonForNotEatingAwcmeal();
            BindddlIsFirstPregnancy();
            BindddlAwcregister();
            btnSave.Text = StaticClass.PageButtonText;
            txtExpectedDeliveryDate2.IsVisible = false;
            
            txtLastDeliveryDate2.IsVisible = false;
            txtLastDeliveryDate11.IsVisible = true;
            stacShowAndHide.IsVisible = false;
            Bindpreviousdata();
            if (StaticClass.PageButtonText == "Update")
            {
                EditMGrouth();
            }
            else
            {
                lastMGrouth();
            }
            try
            {
                var ListOfDatamonth = App.DAUtil.GetDataMonthsFormate().Where(d => d.Datamonthid == StaticClass.DataMonthId).FirstOrDefault();
                DateTime tempDate = ListOfDatamonth.Datamonth;
                int year = tempDate.Year;
                int month = tempDate.Month;
                txtDateOfMeasurement.MinimumDate = new DateTime(year, month, 1);
                txtDateOfMeasurement.MaximumDate = tempDate;
                DateTime dateTime = txtDateOfMeasurement.Date;
                txtExpectedDeliveryDate.MinimumDate = dateTime;
                txtLastDeliveryDate.MaximumDate = new DateTime(year, month, tempDate.Day);
                txtExpectedDeliveryDate.MaximumDate = new DateTime(year, month += 5, tempDate.Day);
                // txtLastDeliveryDate.MinimumDate = dateTime;
                 //new DateTime(year, month, tempDate.Day);
            }
            catch
            {

            }
            //BindLastData();
        }
        private void Bindpreviousdata()
        {
            var MotherRecord = App.DAUtil.FindGrowthRegisterMother(StaticClass.GrouthFamilyId.ToString());
            if (MotherRecord != null && MotherRecord.Count != 0)
            {
                var MotherData = MotherRecord.LastOrDefault();
                if (MotherData.MotherWeightIn1Trimester != 0)
                {
                    txtMotherWeightIn1Trimesterr.Text = MotherData.MotherWeightIn1Trimester.ToString();

                }
            }
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
        public void BindddlAwcregister()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddlAwcregister.ItemsSource = ListownAgriculturalLandk;
        }
        public void BindddChildExpected()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddChildExpected.ItemsSource = ListownAgriculturalLandk;
        }
        public void BindddlIsLactating()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddlIsLactating.ItemsSource = ListownAgriculturalLandk;
        }
        public void BindddlGetMealAwcUnderAay()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddlGetMealAwcUnderAay.ItemsSource = ListownAgriculturalLandk;
        }
        public void BindddlReceivedDietUnderAay()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddlReceivedDietUnderAay.ItemsSource = ListownAgriculturalLandk;
        }
        public void BindddlReceivedMealFromAwcunderAay()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddlMealFromAwcunderAay.ItemsSource = ListownAgriculturalLandk;
        }
        public void BindddlTypeOfMeal()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(40);
            ddlTypeOfMeal.ItemsSource = ListOfWaterType;
        }
        public void BindddlEggReceived()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(41);
            ddlEggReceived.ItemsSource = ListOfWaterType;
        }
        public void BindddlReceivedSnacksUnderAay()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddlReceivedSnacksUnderAay.ItemsSource = ListownAgriculturalLandk;
        }
        public void BindddlReceivedSnacksFromAwc()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(42);
            ddlReceivedSnacksFromAwc.ItemsSource = ListOfWaterType;

        }
        public void BindddlEatMealRegularlyFromAwc()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(43);
            ddlEatMealRegularlyFromAwc.ItemsSource = ListOfWaterType;
        }
        public void BindddlIsMealEnough()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(44);
            ddlIsMealEnough.ItemsSource = ListOfWaterType;

        }
        public void BindddlQualityOfAwcfood()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(45);
            ddlQualityOfAwcfood.ItemsSource = ListOfWaterType;
        }
        public void BindddlResonForNotEatingAwcmeal()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(46);
            ddlResonForNotEatingAwcmeal.ItemsSource = ListOfWaterType;
        }
        public void BindddlIsFirstPregnancy()
        {
            List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            OwnAgriculturalLand ownnull = new OwnAgriculturalLand();
            ownnull.Id = 2;
            ownnull.Name = "";
            ListownAgriculturalLandk.Add(ownnull);
            ddlIsFirstPregnancy.ItemsSource = ListownAgriculturalLandk;
        }
        private void validation()
        {
            try
            {
                var selectedDataMonth = (DataM)ddlDataMonth.SelectedItem;
                int MId = selectedDataMonth == null ? 0 : selectedDataMonth.Datamonthid;
                if (MId == 0)
                {
                    IsValidation = false;
                    DependencyService.Get<Toast>().Show("Please select Data Month");
                    return;
                }
                //var ChildExpectedid = (OwnAgriculturalLand)ddChildExpected.SelectedItem;
                //if (ChildExpectedid == null)
                //{
                //    IsValidation = false;
                //    DependencyService.Get<Toast>().Show("Please select Child Expected");
                //    return;
                //}
                //var Lactating = (OwnAgriculturalLand)ddlIsLactating.SelectedItem;
                //if (Lactating == null)
                //{
                //    IsValidation = false;
                //    DependencyService.Get<Toast>().Show("Please select Lactating");
                //    return;
                //}
                if (ddlReceivedMealFromAwcunderAay.TextColor == Color.Red)
                {
                    IsValidation = false;
                    DependencyService.Get<Toast>().Show("Please enter received meal from AWC under AAY between 0 to 6 months");
                    return;
                }
            }
            catch
            {

            }

        }
        private void EditMGrouth()
        {
            try
            {
                var checkFamilydata = App.DAUtil.FindGrowthRegisterMotherSingleData(StaticClass.GrouthMotherID.ToString());
            if (checkFamilydata.Count > 0)
            {
                
                    var ListOfDatamonth = App.DAUtil.GetDataMonthsFormate().OrderByDescending(x => x.Datamonthid).ToList();
                    for (int i = 0; i < ListOfDatamonth.Count; i++)
                    {
                        DataM dataMonths = new DataM();
                        dataMonths.Datamonthid = ListOfDatamonth[i].Datamonthid;
                        if (ListOfDatamonth[i].Datamonthid == checkFamilydata[0].DataMonthId)
                        {
                            ddlDataMonth.SelectedIndex = i;
                        }
                    }

                    ddChildExpected.SelectedIndex = checkFamilydata[0].ChildExpected == true ? 0 : 1;
                    ddlReceivedDietUnderAay.SelectedIndex = checkFamilydata[0].ReceivedDietUnderAay == true ? 0 : 1;
                    ddlGetMealAwcUnderAay.SelectedIndex = checkFamilydata[0].GetMealAwcUnderAay == true ? 0 : 1;
                    ddlMealFromAwcunderAay.SelectedIndex = checkFamilydata[0].ReceivedMealFromAwcunderAay == true ? 0 : 1;
                    if (checkFamilydata[0].ExpectedDeliveryDate == null)
                    {
                        txtExpectedDeliveryDate2.IsVisible = false;
                        txtExpectedDeliveryDate11.IsVisible = true;
                    }
                    else
                    {
                        txtExpectedDeliveryDate.Date = Convert.ToDateTime(checkFamilydata[0].ExpectedDeliveryDate);
                        txtExpectedDeliveryDate2.IsVisible = true;
                        txtExpectedDeliveryDate11.IsVisible = false;
                        DDCheck = 1;
                    }
                    if (checkFamilydata[0].LastDeliveryDate == null)
                    {
                        txtLastDeliveryDate2.IsVisible = false;
                        txtLastDeliveryDate11.IsVisible = true;
                    }
                    else
                    {
                        txtLastDeliveryDate.Date = Convert.ToDateTime(checkFamilydata[0].LastDeliveryDate);
                        txtLastDeliveryDate2.IsVisible = true;
                        txtLastDeliveryDate11.IsVisible = false;
                    }
                    // txtLastDeliveryDate.Date = checkFamilydata[0].LastDeliveryDate;
                    ddlAwcregister.SelectedIndex = checkFamilydata[0].AwcregistrationDate == true ? 0 : 1;
                    txtancregister.Text = checkFamilydata[0].AncregistraionDate.ToString();

                    txtMotherWeightInDeliveryTime.Text = checkFamilydata[0].MotherWeightInDeliveryTime.ToString();
                    txtDateOfMeasurement.IsEnabled = false;
                    txtDateOfMeasurement.Date = checkFamilydata[0].DateOfMeasurement;

                    // ddlReceivedDietUnderAay.SelectedIndex = checkFamilydata[0].ReceivedDietUnderAay == true ? 0 : 1;
                    ddlReceivedMealFromAwcunderAay.Text = checkFamilydata[0].HowManyReceivedMealFromAwcunderAay.ToString();
                    ddlReceivedSnacksUnderAay.SelectedIndex = checkFamilydata[0].ReceivedSnacksUnderAay == true ? 0 : 1;
                    // ddlReceivedSnacksFromAwc.SelectedIndex = checkFamilydata[0].ReceivedSnacksFromAwc == true ? 0 : 1;

                    txtMotherWeightIn1Trimesterr.Text = checkFamilydata[0].MotherWeightIn1Trimester.ToString();
                    var TypeOfReceivedSnacksFromAwc = App.DAUtil.GetColumnValuesBytext(42);
                    for (int i = 0; i < TypeOfReceivedSnacksFromAwc.Count; i++)
                    {
                        if (TypeOfReceivedSnacksFromAwc[i].columnValueId == checkFamilydata[0].ReceivedSnacksFromAwc)
                        {
                            ddlReceivedSnacksFromAwc.SelectedIndex = i;
                        }
                    }
                    txtMotherWeightIn1Trimesterr.Text = checkFamilydata[0].MotherWeightIn1Trimester.ToString();
                    var TypeOfMeal = App.DAUtil.GetColumnValuesBytext(40);
                    for (int i = 0; i < TypeOfMeal.Count; i++)
                    {
                        if (TypeOfMeal[i].columnValueId == checkFamilydata[0].TypeOfMeal)
                        {
                            ddlTypeOfMeal.SelectedIndex = i;
                        }
                    }
                    txtMotherWeightIn1Trimesterr.Text = checkFamilydata[0].MotherWeightIn1Trimester.ToString();
                    var typeEatMealRegularlyFromAwc = App.DAUtil.GetColumnValuesBytext(43);
                    for (int i = 0; i < typeEatMealRegularlyFromAwc.Count; i++)
                    {
                        if (typeEatMealRegularlyFromAwc[i].columnValueId == checkFamilydata[0].EatMealRegularlyFromAwc)
                        {
                            ddlEatMealRegularlyFromAwc.SelectedIndex = i;
                        }
                    }


                    var TypeEggReceived = App.DAUtil.GetColumnValuesBytext(41);
                    for (int i = 0; i < TypeEggReceived.Count; i++)
                    {
                        if (TypeEggReceived[i].columnValueId == checkFamilydata[0].EggReceived)
                        {
                            ddlEggReceived.SelectedIndex = i;
                        }
                    }

                    // ddlEggReceived.SelectedIndex = checkFamilydata[0].EggReceived == true ? 0 : 1;
                    ddlIsLactating.SelectedIndex = checkFamilydata[0].IsLactating == true ? 0 : 1;
                    ddlIsFirstPregnancy.SelectedIndex = checkFamilydata[0].IsFirstPregnancy == true ? 0 : 1;
                    var EatMealRegularlyFromAwc = App.DAUtil.GetColumnValuesBytext(43);
                    for (int i = 0; i < EatMealRegularlyFromAwc.Count; i++)
                    {
                        if (EatMealRegularlyFromAwc[i].columnValueId == checkFamilydata[0].EatMealRegularlyFromAwc)
                        {
                            ddlEatMealRegularlyFromAwc.SelectedIndex = i;
                        }
                    }
                    var IsMealEnough = App.DAUtil.GetColumnValuesBytext(44);
                    for (int i = 0; i < IsMealEnough.Count; i++)
                    {
                        if (IsMealEnough[i].columnValueId == checkFamilydata[0].IsMealEnough)
                        {
                            ddlIsMealEnough.SelectedIndex = i;
                        }
                    }
                    var QualityOfAwcfood = App.DAUtil.GetColumnValuesBytext(45);
                    for (int i = 0; i < QualityOfAwcfood.Count; i++)
                    {
                        if (QualityOfAwcfood[i].columnValueId == checkFamilydata[0].QualityOfAwcfood)
                        {
                            ddlQualityOfAwcfood.SelectedIndex = i;
                        }
                    }
                    var ResonForNotEatingAwcmeal = App.DAUtil.GetColumnValuesBytext(46);
                    for (int i = 0; i < ResonForNotEatingAwcmeal.Count; i++)
                    {
                        if (ResonForNotEatingAwcmeal[i].columnValueId == checkFamilydata[0].ResonForNotEatingAwcmeal)
                        {
                            ddlResonForNotEatingAwcmeal.SelectedIndex = i;
                        }
                    }
                    if (!string.IsNullOrEmpty(checkFamilydata[0].MotherWeightInDeliveryTime.ToString()) && !string.IsNullOrEmpty(checkFamilydata[0].MotherWeightIn1Trimester.ToString()))
                    {
                        double DWeight = 0;
                        DWeight = Convert.ToDouble(txtMotherWeightInDeliveryTime.Text);
                        double MWT = 0;
                        MWT = Convert.ToDouble(txtMotherWeightIn1Trimesterr.Text);
                        if (MWT != 0 && DWeight != 0)
                        {
                            txtdifferentweigh.Text = (DWeight - MWT).ToString();
                        }
                    }
                }
              }
            catch (Exception ex)
            {

            }

        }
        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            try
            {
                IsValidation = true;
                validation();
                if (IsValidation)
                {

                    TblGrowthRegisterMother tblGrowthRegisterMother = new TblGrowthRegisterMother();
                    if (btnSave.Text == "Update")
                    {
                        var CheckMotherGroth = App.DAUtil.FindGrowthRegisterMotherSingleData(StaticClass.GrouthMotherID.ToString());
                        tblGrowthRegisterMother.GrowthId = CheckMotherGroth[0].GrowthId; //StaticClass.GrouthMotherID;
                        tblGrowthRegisterMother.FamilyId = CheckMotherGroth[0].FamilyId; //StaticClass.GrouthFamilyId;
                        StaticClass.GrouthFamilyId= CheckMotherGroth[0].FamilyId;
                        tblGrowthRegisterMother.DOE = CheckMotherGroth[0].DOE;// DateTime.Now;
                        App.DAUtil.deleteGrowthRegisterMother(StaticClass.GrouthFamilyId.ToString(), StaticClass.GrouthMotherID.ToString());

                    }
                    else
                    {
                        tblGrowthRegisterMother.GrowthId = Guid.NewGuid();
                        tblGrowthRegisterMother.FamilyId = StaticClass.GrouthFamilyId;
                        tblGrowthRegisterMother.DOE = DateTime.Now;
                    }

                    var selectedDataMonth = (DataM)ddlDataMonth.SelectedItem;
                    tblGrowthRegisterMother.DataMonthId = selectedDataMonth.Datamonthid;
                    var ChildExpectedid = (OwnAgriculturalLand)ddChildExpected.SelectedItem;
                    string IsChildExpected = ChildExpectedid==null?"No": ChildExpectedid.Name;
                    tblGrowthRegisterMother.ChildExpected = IsChildExpected == "Yes" ? true : false;
                    tblGrowthRegisterMother.ExpectedDeliveryDate = Expectdate == null ? (DateTime?)null : txtExpectedDeliveryDate.Date;
                    var SelectedIsLactatingid = (OwnAgriculturalLand)ddlIsLactating.SelectedItem;
                    string IsLactating = SelectedIsLactatingid==null?"No": SelectedIsLactatingid.Name;
                    tblGrowthRegisterMother.IsLactating = IsLactating == "Yes" ? true : false;
                    tblGrowthRegisterMother.LastDeliveryDate = lastdDate == null ? (DateTime?)null : txtLastDeliveryDate.Date;
                    var SelectedddlAwcregister = (OwnAgriculturalLand)ddlAwcregister.SelectedItem;
                    string awcregistrationDate = SelectedddlAwcregister == null ? "No" : SelectedddlAwcregister.Name;
                    tblGrowthRegisterMother.AwcregistrationDate= awcregistrationDate == "Yes" ? true : false;
                    tblGrowthRegisterMother.AncregistraionDate = Convert.ToInt32(txtancregister.Text);
                    tblGrowthRegisterMother.MotherWeightIn1Trimester = Convert.ToDecimal(txtMotherWeightIn1Trimesterr.Text);

                    tblGrowthRegisterMother.MotherWeightInDeliveryTime = Convert.ToDecimal(txtMotherWeightInDeliveryTime.Text);
                    tblGrowthRegisterMother.DateOfMeasurement = txtDateOfMeasurement.Date;
                    var SelectedReceivedDietUnderAay = (OwnAgriculturalLand)ddlReceivedDietUnderAay.SelectedItem;
                    string IsSelectedReceivedDietUnderAay = SelectedReceivedDietUnderAay == null ? "No" : SelectedReceivedDietUnderAay.Name;
                    tblGrowthRegisterMother.ReceivedDietUnderAay = IsSelectedReceivedDietUnderAay == "Yes" ? true : false;
                    var SelectedGetMealAwcUnderAay = (OwnAgriculturalLand)ddlGetMealAwcUnderAay.SelectedItem;
                    string IsSelectedGetMealAwcUnderAay = SelectedGetMealAwcUnderAay == null ? "No" : SelectedGetMealAwcUnderAay.Name;
                    tblGrowthRegisterMother.GetMealAwcUnderAay = IsSelectedGetMealAwcUnderAay == "Yes" ? true : false;

                    var SelectedReceivedMealFromAwcunderAay = (OwnAgriculturalLand)ddlMealFromAwcunderAay.SelectedItem;
                    string IsSelectedReceivedMealFromAwcunderAay = SelectedReceivedMealFromAwcunderAay == null ? "No" : SelectedReceivedMealFromAwcunderAay.Name;
                    tblGrowthRegisterMother.ReceivedMealFromAwcunderAay = IsSelectedReceivedMealFromAwcunderAay == "Yes" ? true : false;
                    tblGrowthRegisterMother.HowManyReceivedMealFromAwcunderAay = Convert.ToInt32(ddlReceivedMealFromAwcunderAay.Text);
                    var SelectedReceivedSnacksUnderAay = (OwnAgriculturalLand)ddlReceivedSnacksUnderAay.SelectedItem;
                    string IsSelectedReceivedSnacksUnderAay = SelectedReceivedSnacksUnderAay == null ? "No" : SelectedReceivedSnacksUnderAay.Name;
                    tblGrowthRegisterMother.ReceivedSnacksUnderAay = IsSelectedReceivedSnacksUnderAay == "Yes" ? true : false;
                    var SelectedReceivedSnacksFromAwc = (ColumnValue)ddlReceivedSnacksFromAwc.SelectedItem;
                    tblGrowthRegisterMother.ReceivedSnacksFromAwc = SelectedReceivedSnacksFromAwc == null ? 0 : SelectedReceivedSnacksFromAwc.columnValueId;
                    var selectedTypeOfMeal = (ColumnValue)ddlTypeOfMeal.SelectedItem;
                    tblGrowthRegisterMother.TypeOfMeal = selectedTypeOfMeal == null ? 0 : selectedTypeOfMeal.columnValueId;
                    //  var selectedEggReceived = (ColumnValue)ddlEggReceived.SelectedItem;
                    // tblGrowthRegisterMother.EggReceived = selectedEggReceived.columnValueId;
                    var selectedEggReceived = (ColumnValue)ddlEggReceived.SelectedItem;
                    tblGrowthRegisterMother.EggReceived = selectedEggReceived == null ? 0 : selectedEggReceived.columnValueId;
                    var selectedEatMealRegularlyFromAwc = (ColumnValue)ddlEatMealRegularlyFromAwc.SelectedItem;
                    tblGrowthRegisterMother.EatMealRegularlyFromAwc = selectedEatMealRegularlyFromAwc == null ? 0 : selectedEatMealRegularlyFromAwc.columnValueId;
                    var selectedIsMealEnough = (ColumnValue)ddlIsMealEnough.SelectedItem;
                    tblGrowthRegisterMother.IsMealEnough = selectedIsMealEnough == null ? 0 : selectedIsMealEnough.columnValueId;
                    var selectedQualityOfAwcfood = (ColumnValue)ddlQualityOfAwcfood.SelectedItem;
                    tblGrowthRegisterMother.QualityOfAwcfood = selectedQualityOfAwcfood == null ? 0 : selectedQualityOfAwcfood.columnValueId;
                    var selectedResonForNotEatingAwcmeal = (ColumnValue)ddlResonForNotEatingAwcmeal.SelectedItem;
                    tblGrowthRegisterMother.ResonForNotEatingAwcmeal = selectedResonForNotEatingAwcmeal == null ? 0 : selectedResonForNotEatingAwcmeal.columnValueId;
                    var SelectedIsFirstPregnancy = (OwnAgriculturalLand)ddlIsFirstPregnancy.SelectedItem;
                    string SelectFirstPregnancy = SelectedIsFirstPregnancy == null ? "No" : SelectedIsFirstPregnancy.Name;
                    tblGrowthRegisterMother.IsFirstPregnancy = SelectFirstPregnancy == "Yes" ? true : false;
                    tblGrowthRegisterMother.DOU = DateTime.Now;
                    tblGrowthRegisterMother.ANCCheckups = StaticClass.ANCCheckups;
                    tblGrowthRegisterMother.ANMMarkHighRiskScreening = StaticClass.ANMMarkHighRiskScreening;
                    tblGrowthRegisterMother.HighRiskMotherHistory = StaticClass.HighRiskMother;
                    tblGrowthRegisterMother.TotalPregnancyMonths = txtTotalPregnancyMonths.Text;

                    tblGrowthRegisterMother.CreatedBy = 0;
                    tblGrowthRegisterMother.UpdatedBy = 0;

                    App.DAUtil.SaveGrowthRegisterMother(tblGrowthRegisterMother);
                    StaticClass.ANCCheckups = null;
                    StaticClass.ANMMarkHighRiskScreening = null;
                    StaticClass.HighRiskMother = null;
                    StaticClass.PageName = "HomePage";
                    StaticClass.TabbedIndex = 1;
                    Application.Current.MainPage = new MasterNavigationPage();

                }
                else
                {

                }
            }
            catch (Exception ex)
            {

            }
        }
        private void showandhidePageLoadStacklayout()
        {
            //  stck4.IsVisible = false;
            //stack8.IsVisible = false;
            //stack41.IsVisible = false;
            //stac4truwithin6month.IsVisible = false;
            //Stack46.IsVisible = false;
            //stack53.IsVisible = false;

        }
        private void DdChildExpected_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ChildExpectedid = (OwnAgriculturalLand)ddChildExpected.SelectedItem;
            string IsChildExpected = ChildExpectedid.Name;
            if (IsChildExpected == "Yes")
            {
                stacShowAndHide.IsVisible = true;
            }
            else
            {

                stacShowAndHide.IsVisible = false;
                ddlIsFirstPregnancy.SelectedIndex = 2;
            }


        }

        private void DdlIsLactating_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ChildExpectedid = (OwnAgriculturalLand)ddChildExpected.SelectedItem;
            if (ChildExpectedid == null)
            {
                DependencyService.Get<Toast>().Show("Please select Child Expected");
                ddlIsLactating.SelectedItem = null;
            }
            else
            {
                //   var IsLactating = (OwnAgriculturalLand)ddlIsLactating.SelectedItem;

                // string Lactating = IsLactating.Name;

            }
        }

        private void DdlIsGettingTreatment_SelectedIndexChanged(object sender, EventArgs e)
        {
            // var ChildExpectedid = (OwnAgriculturalLand)ddChildExpected.SelectedItem;
            // var Lactating = (OwnAgriculturalLand)ddlIsLactating.SelectedItem;



        }


        //private void DdlReceivedMealFromAwcunderAay_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var ReceivedMealFromAwcunderAay = (OwnAgriculturalLand)ddlReceivedMealFromAwcunderAay.SelectedItem;
        //   string ReceivedMealFromAwc = ReceivedMealFromAwcunderAay.Name;

        //}

        private async void TxtHighRiskMotherHistory_Clicked(object sender, EventArgs e)
        {
            var page = new HighRiskMotherHistoryPage();
            await Navigation.PushPopupAsync(page);
        }

        private async void TxtANCCheckups_Clicked(object sender, EventArgs e)
        {
            var page = new ANCCheckupsPage();
            await Navigation.PushPopupAsync(page);
        }

        private async void TxtANMMarkHighRiskScreening_Clicked(object sender, EventArgs e)
        {
            var page = new ANMMarkHighRiskScreeningPage();
            await Navigation.PushPopupAsync(page);
        }



        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            txtExpectedDeliveryDate.Focus();
        }

        private void TxtExpectedDeliveryDate_DateSelected(object sender, DateChangedEventArgs e)
        {
           
               
                txtExpectedDeliveryDate2.IsVisible = true;
                txtExpectedDeliveryDate11.IsVisible = false;
                Expectdate = txtExpectedDeliveryDate.Date;
                if (DDCheck != 0)
                {
                    DaysCalculation();
                }
                DDCheck = 0;

        }
        private void DaysCalculation()
        {
            try
            {
                DateTime date_of_submission = Convert.ToDateTime(DateTime.Now.ToString("MM/dd/yyyy"));
                DateTime _effective_date = Convert.ToDateTime(txtExpectedDeliveryDate.Date);
                int difference = _effective_date.Month - date_of_submission.Month;

                txtTotalPregnancyMonths.Text = (9 - difference).ToString();
            }
            catch
            {

            }
        }
       

        private void DdlReceivedMealFromAwcunderAay_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(ddlReceivedMealFromAwcunderAay.Text) <= 6 || Convert.ToInt32(ddlReceivedMealFromAwcunderAay.Text) == 0)
                {
                    ddlReceivedMealFromAwcunderAay.TextColor = Color.Black;
                }
                else
                {
                    ddlReceivedMealFromAwcunderAay.TextColor = Color.Red;
                }
            }
            catch
            {
                ddlReceivedMealFromAwcunderAay.TextColor = Color.Black;
            }
        }

        private void TxtMotherWeightInDeliveryTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double DWeight = 0;
                DWeight = Convert.ToDouble(txtMotherWeightInDeliveryTime.Text);
                double MWT = 0;
                MWT = Convert.ToDouble(txtMotherWeightIn1Trimesterr.Text);
                if (MWT != 0 && DWeight != 0)
                {
                    txtdifferentweigh.Text = (DWeight - MWT).ToString();
                }
            }
            catch
            {

            }
        }

        private void TxtTotalPregnancyMonths_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtTotalPregnancyMonths.Text))
            {
                txtExpectedDeliveryDate11.IsVisible = true;
                txtExpectedDeliveryDate2.IsVisible = false;

            }
            else
            {
                try
                {


                    string txtNoOfchild = txtTotalPregnancyMonths.Text.Replace(".", "");
                    txtTotalPregnancyMonths.Text = txtNoOfchild;
                    DateTime today = DateTime.Now;
                    int moOfMonth = Convert.ToInt32(txtTotalPregnancyMonths.Text);
                    DateTime totalMonths = today.AddMonths(9 - moOfMonth);
                    txtExpectedDeliveryDate11.IsVisible = false;
                    txtExpectedDeliveryDate2.IsVisible = true;

                    txtExpectedDeliveryDate.Date = totalMonths;
                }
                catch
                {

                }
            }
        }

        private void TxtDateOfMeasurement_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                if (txtExpectedDeliveryDate2.IsVisible == false)
                {
                    DateTime dateTime = txtDateOfMeasurement.Date;
                    txtExpectedDeliveryDate.MinimumDate = dateTime;
                    int month = dateTime.Month;
                    txtExpectedDeliveryDate.MaximumDate = new DateTime(dateTime.Year, month += 5, dateTime.Day);
                    txtLastDeliveryDate.MaximumDate = DateTime.Now;
                    txtExpectedDeliveryDate2.IsVisible = false;
                    txtExpectedDeliveryDate11.IsVisible = true;
                   //txtLastDeliveryDate11.IsVisible = true;
                    //txtLastDeliveryDate2.IsVisible = false;
                }
                else
                {
                    DateTime dateTime = txtDateOfMeasurement.Date;
                    txtExpectedDeliveryDate.MinimumDate = dateTime;
                    int month = dateTime.Month;
                    txtExpectedDeliveryDate.MaximumDate = new DateTime(dateTime.Year, month += 5, dateTime.Day);
                    txtExpectedDeliveryDate2.IsVisible = true;
                    txtExpectedDeliveryDate11.IsVisible = false;
                    //  txtLastDeliveryDate.MinimumDate = dateTime;
                    txtLastDeliveryDate.MaximumDate = DateTime.Now; //new DateTime(dateTime.Year, month, dateTime.Day);
                    //txtLastDeliveryDate11.IsVisible = false;
                    //txtLastDeliveryDate2.IsVisible = true;
                }
            }
            catch
            {

            }

        }
        private void lastMGrouth()
        {
            try
            {
                var checkFamilydata = App.DAUtil.FindGrowthRegisterMother(StaticClass.GrouthFamilyId.ToString()).LastOrDefault();
                if (checkFamilydata != null)
                {
                    try
                    {
                        ddChildExpected.SelectedIndex = checkFamilydata.ChildExpected == true ? 0 : 1;
                        ddlIsFirstPregnancy.SelectedIndex = checkFamilydata.IsFirstPregnancy == true ? 0 : 1;
                        ddlMealFromAwcunderAay.SelectedIndex = checkFamilydata.ReceivedMealFromAwcunderAay == true ? 0 : 1;
                        txtancregister.Text = checkFamilydata.AncregistraionDate.ToString();
                        ddlGetMealAwcUnderAay.SelectedIndex = checkFamilydata.GetMealAwcUnderAay == true ? 0 : 1;
                        if (checkFamilydata.ExpectedDeliveryDate == null)
                        {
                            txtExpectedDeliveryDate2.IsVisible = false;
                            txtExpectedDeliveryDate11.IsVisible = true;
                        }
                        else
                        {
                            txtExpectedDeliveryDate.Date = Convert.ToDateTime(checkFamilydata.ExpectedDeliveryDate);
                            txtExpectedDeliveryDate2.IsVisible = true;
                            txtExpectedDeliveryDate11.IsVisible = false;
                        }
                        if (checkFamilydata.LastDeliveryDate == null)
                        {
                            txtLastDeliveryDate2.IsVisible = false;
                            txtLastDeliveryDate11.IsVisible = true;
                        }
                        else
                        {
                            txtLastDeliveryDate.Date = Convert.ToDateTime(checkFamilydata.LastDeliveryDate);
                            txtLastDeliveryDate2.IsVisible = true;
                            txtLastDeliveryDate11.IsVisible = false;
                        }
                        txtMotherWeightIn1Trimesterr.Text = checkFamilydata.MotherWeightIn1Trimester.ToString();
                        var TypeOfReceivedSnacksFromAwc = App.DAUtil.GetColumnValuesBytext(42);
                        for (int i = 0; i < TypeOfReceivedSnacksFromAwc.Count; i++)
                        {
                            if (TypeOfReceivedSnacksFromAwc[i].columnValueId == checkFamilydata.ReceivedSnacksFromAwc)
                            {
                                ddlReceivedSnacksFromAwc.SelectedIndex = i;
                            }
                        }
                        txtMotherWeightIn1Trimesterr.Text = checkFamilydata.MotherWeightIn1Trimester.ToString();
                        var TypeOfMeal = App.DAUtil.GetColumnValuesBytext(40);
                        for (int i = 0; i < TypeOfMeal.Count; i++)
                        {
                            if (TypeOfMeal[i].columnValueId == checkFamilydata.TypeOfMeal)
                            {
                                ddlTypeOfMeal.SelectedIndex = i;
                            }
                        }
                        txtMotherWeightIn1Trimesterr.Text = checkFamilydata.MotherWeightIn1Trimester.ToString();
                        var typeEatMealRegularlyFromAwc = App.DAUtil.GetColumnValuesBytext(43);
                        for (int i = 0; i < typeEatMealRegularlyFromAwc.Count; i++)
                        {
                            if (typeEatMealRegularlyFromAwc[i].columnValueId == checkFamilydata.EatMealRegularlyFromAwc)
                            {
                                ddlEatMealRegularlyFromAwc.SelectedIndex = i;
                            }
                        }


                        //  txtLastDeliveryDate.Date = checkFamilydata[0].LastDeliveryDate;
                        ddlAwcregister.SelectedIndex = checkFamilydata.AwcregistrationDate == true ? 0 : 1;
                        ddlIsLactating.SelectedIndex = checkFamilydata.IsLactating == true ? 0 : 1;

                        if (!string.IsNullOrEmpty(checkFamilydata.MotherWeightInDeliveryTime.ToString()) && !string.IsNullOrEmpty(checkFamilydata.MotherWeightIn1Trimester.ToString()))
                        {
                            double DWeight = 0;
                            DWeight = Convert.ToDouble(txtMotherWeightInDeliveryTime.Text);
                            double MWT = 0;
                            MWT = Convert.ToDouble(txtMotherWeightIn1Trimesterr.Text);
                            if (MWT != 0 && DWeight != 0)
                            {
                                txtdifferentweigh.Text = (DWeight - MWT).ToString();
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }


                }
            }
            catch
            {

            }
            }

        private void TxtLastDeliveryDate_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                
                    lastdDate = txtLastDeliveryDate.Date;
                
            }
            catch(Exception ex)
            {

            }
            }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            txtLastDeliveryDate2.IsVisible = true;
            txtLastDeliveryDate11.IsVisible = false;
            txtLastDeliveryDate.Focus();
        }
    }
}