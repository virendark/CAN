using CAN.Models;
using Rg.Plugins.Popup.Extensions;
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
    public partial class FamilyPage : ContentPage
    {
        bool IsValidation = true;
        bool IsShowDOB = false;
        List<OwnAgriculturalLand> ListownAgriculturalLandk = new List<OwnAgriculturalLand>();
        List<MotherWork> ListIsMotherWork = new List<MotherWork>();
        List<ElectricityAvailable> ListOfElectricityAvailable = new List<ElectricityAvailable>();
        List<CookingGasAvailable> ListOfCookingGasAvailable = new List<CookingGasAvailable>();
        public FamilyPage()
        {
            InitializeComponent();
            // LabelHide();
            txtMDOBP.IsVisible = false;
            staccastName.IsVisible = false;
            stacmothergooutforwork.IsVisible = true;
            stacmigrate.IsVisible = false;
            this.Title = StaticClass.LocationName;
            BindFamilyType();
            BingWaterType();
            BingSourceWater();
            IsMotherWorkBind();
            //  BindLocation();
            BindRationCard();
            BindAgriculture();
            BindHouse();
            BindCast();
            BindReligion();
            BindMotherEducation();
            BindFatherEducation();
            BindElectricityAvailable();
            //  BindCookingGasAvailable();
            BindWorkType();
            BindWorkHours();
            BindTolite();
            BindStatus();
            BindddlFamilyMigrateToEarn();
            BindddlFamilyMigrateFor();
            BindFatherOccupation();
            BindMotherOccupation();
            //   BindLocation();
            stacmotherwork.IsVisible = false;
            lblFType.IsVisible = true;
            lblFEducation.IsVisible = true;
            ddlLocation.Text = StaticClass.LocationName;
            if (StaticClass.PageButtonText != null)
            {
                btnSave.Text = StaticClass.PageButtonText;
                if (StaticClass.PageButtonText == "Update")
                {
                    EditFamily();

                }
            }
            FlagMaxAndMinDate();
        }
        private void FlagMaxAndMinDate()
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;
            int month = dateTime.Month;
            int day = dateTime.Day;
            txtMDOB.MaximumDate = new DateTime(year, month, day);

            txtdateofEntry.MaximumDate = new DateTime(year, month, day);
        }
        public void BingWaterType()
        {
            var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(10);


            ddlWaterType.ItemsSource = ListOfWaterType;

        }
        public void BingSourceWater()
        {
            var ListOfSourceWater = App.DAUtil.GetColumnValuesBytext(60);


            ddlSourceWater.ItemsSource = ListOfSourceWater;

        }
        public void BindRationCard()
        {
            var Rationcard = App.DAUtil.GetColumnValuesBytext(2);

            ddlTofRation.ItemsSource = Rationcard;

        }
        public void BindAgriculture()
        {

            OwnAgriculturalLand ownAgriculturalLand = new OwnAgriculturalLand();
            ownAgriculturalLand.Id = 1;
            ownAgriculturalLand.Name = "Yes";
            ListownAgriculturalLandk.Add(ownAgriculturalLand);
            OwnAgriculturalLand own = new OwnAgriculturalLand();
            own.Id = 0;
            own.Name = "No";
            ListownAgriculturalLandk.Add(own);
            ddlAgriculturalLand.ItemsSource = ListownAgriculturalLandk;
        }
        public void IsMotherWorkBind()
        {
            var ListOfIsmotherworking = App.DAUtil.GetColumnValuesBytext(65);
            ddlIsMotherWork.ItemsSource = ListOfIsmotherworking;
        }
        private void BindHouse()
        {
            var ListOfHouse = App.DAUtil.GetColumnValuesBytext(6);
            ddlHouseType.ItemsSource = ListOfHouse;
        }
        private void BindElectricityAvailable()
        {
            ElectricityAvailable electricityAvailable = new ElectricityAvailable();
            electricityAvailable.Id = 1;
            electricityAvailable.Name = "Available";
            ListOfElectricityAvailable.Add(electricityAvailable);
            ElectricityAvailable electricityAvailable1 = new ElectricityAvailable();
            electricityAvailable1.Id = 0;
            electricityAvailable1.Name = "Not Available";
            ListOfElectricityAvailable.Add(electricityAvailable1);
            ddlElictricityAvailable.ItemsSource = ListOfElectricityAvailable;
        }
        //private void BindCookingGasAvailable()
        //{

        //    CookingGasAvailable cookingGasAvailable = new CookingGasAvailable();
        //    cookingGasAvailable.Id = 1;
        //    cookingGasAvailable.Name = "Yes";
        //    ListOfCookingGasAvailable.Add(cookingGasAvailable);
        //    CookingGasAvailable cookingGasAvailable1 = new CookingGasAvailable();
        //    cookingGasAvailable1.Id = 0;
        //    cookingGasAvailable1.Name = "No";
        //    ListOfCookingGasAvailable.Add(cookingGasAvailable1);
        //    ddlCookingGasAvailable.ItemsSource = ListOfCookingGasAvailable;
        //}
        public void BindFamilyType()
        {
            var all = App.DAUtil.GetColumnValues();
            var ListOfFamilyType = App.DAUtil.GetColumnValuesBytext(7);
            ddlFType.ItemsSource = ListOfFamilyType;

        }
        public void BindStatus()
        {

            var ListStatus = App.DAUtil.GetColumnValuesBytext(55);
            ddlStatus.ItemsSource = ListStatus;
            ddlStatus.SelectedIndex = 0;

        }
        public void BindWorkType()
        {
            var all = App.DAUtil.GetColumnValues();
            var ListOfFamilyType = App.DAUtil.GetColumnValuesBytext(13);
            ddlMotherWorkType.ItemsSource = ListOfFamilyType;

        }
        public void BindWorkHours()
        {
            var all = App.DAUtil.GetColumnValues();
            var ListOfFamilyType = App.DAUtil.GetColumnValuesBytext(14);
            ddlMotherWorkHours.ItemsSource = ListOfFamilyType;

        }
        public void BindTolite()
        {
            var all = App.DAUtil.GetColumnValues();
            var ListOfFamilyType = App.DAUtil.GetColumnValuesBytext(15);
            ddlToiletFacility.ItemsSource = ListOfFamilyType;

        }
        public void BindddlFamilyMigrateToEarn()
        {
            var all = App.DAUtil.GetColumnValues();
            var ListOfFamilyType = App.DAUtil.GetColumnValuesBytext(16);
            ddlFamilyMigrateToEarn.ItemsSource = ListOfFamilyType;

        }
        public void BindddlFamilyMigrateFor()
        {
            var all = App.DAUtil.GetColumnValues();
            var ListOfFamilyType = App.DAUtil.GetColumnValuesBytext(17);
            ddlMigrationFor.ItemsSource = ListOfFamilyType;

        }
        public void BindFatherEducation()
        {
            var ListOfFatherEducation = App.DAUtil.GetColumnValuesBytext(11);
            ddFEducation.ItemsSource = ListOfFatherEducation;

        }
        public void BindMotherEducation()
        {
            var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(11);
            ddlMEducation.ItemsSource = ListOfMotherEducation;

        }
        public void BindMotherOccupation()
        {
            //var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(64);
            //ddlMOccupation.ItemsSource = ListOfMotherEducation;

        }
        public void BindFatherOccupation()
        {
            //var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(64);
            //ddlFOccupation.ItemsSource = ListOfMotherEducation;

        }
        public void BindCast()
        {
            var ListOfCast = App.DAUtil.GetColumnValuesBytext(8);
            ddlcast.ItemsSource = ListOfCast;

        }
        public void BindReligion()
        {
            var ListOfReligiont = App.DAUtil.GetColumnValuesBytext(9);
            ddlReligion.ItemsSource = ListOfReligiont;
        }


        private void btnSave_Clicked(object sender, EventArgs e)
        {
            Validation();
            if (IsValidation == true)

            {
                try
                {
                    var selectedFamilyType = (ColumnValue)ddlFType.SelectedItem;
                    int? familyType = selectedFamilyType == null ? (int?)null : selectedFamilyType.columnValueId;
                    var selectedMotherWork = (ColumnValue)ddlIsMotherWork.SelectedItem;
                    int? MotherWorks = selectedMotherWork == null ? (int?)null : selectedMotherWork.columnValueId;
                    var selectedAgriculturalLand = (OwnAgriculturalLand)ddlAgriculturalLand.SelectedItem;
                    string AgriculturalLand = selectedAgriculturalLand == null ? null : selectedAgriculturalLand.Name;
                    var selectedHouse = (ColumnValue)ddlHouseType.SelectedItem;
                    int? HouseId = selectedHouse == null ? (int?)null : selectedHouse.columnValueId;
                    var selectedElectricity = (ElectricityAvailable)ddlElictricityAvailable.SelectedItem;
                    string selectedElectricityName = selectedElectricity == null ? null : selectedElectricity.Name;
                    // var selectedGas = (CookingGasAvailable)ddlCookingGasAvailable.SelectedItem;
                    //string selectedGasName = selectedGas.Name;
                   // long selectedLocationId = StaticClass.VillageID;//selectedLocation.Id;
                    FamilyRegister familyRegister = new FamilyRegister();
                    if (btnSave.Text == "Update")
                    {
                        var checkFamilydata = App.DAUtil.FindFamilyId(StaticClass.FamilyId);
                        familyRegister.FamilyId = checkFamilydata[0].FamilyId; //StaticClass.FamilyId;
                        familyRegister.DOE = checkFamilydata[0].DOE;
                        App.DAUtil.DeleteFamilyById(StaticClass.FamilyId);
                    }
                    else
                    {
                        familyRegister.FamilyId = Guid.NewGuid();
                        familyRegister.DOE = DateTime.Now;
                        StaticClass.FId = familyRegister.FamilyId;
                    }


                    familyRegister.FamilyType = familyType;
                    familyRegister.FatherName = txtFName.Text;


                    var selecteFEducation = (ColumnValue)ddFEducation.SelectedItem;
                    int? selecteFEducationID = selecteFEducation == null ? (int?)null : selecteFEducation.columnValueId;
                    familyRegister.FatherEducation = selecteFEducationID;
                    //var selectedFOccupation = (ColumnValue)ddlFOccupation.SelectedItem;
                    familyRegister.FatherOccupation = StaticClass.FatherOccupation;  //selectedFOccupation == null ? (int?)null : selectedFOccupation.columnValueId;
                    familyRegister.FatherPastDisease = txtFPDisease.Text;
                    familyRegister.MotherName = txtMName.Text;
                    //familyRegister.MotherDOB = txtMDOB.Date;
                    if (txtMDOBP.IsVisible == true)
                    {
                        familyRegister.MotherDOB = txtMDOB.Date;

                    }
                    else
                    {
                        familyRegister.MotherDOB = null;

                    }
                    var selecteMEducation = (ColumnValue)ddlMEducation.SelectedItem;
                    int? selecteMEducationID = selecteMEducation == null ? (int?)null : selecteMEducation.columnValueId;
                    familyRegister.MotherEducation = selecteMEducationID;
                    //var selectedMOccupation = (ColumnValue)ddlMOccupation.SelectedItem;
                    familyRegister.MotherOccupation = StaticClass.MotherOccupation;// selectedMOccupation == null ?(int?)null : selectedMOccupation.columnValueId;
                    familyRegister.MotherPastDisease = txtMPDisease.Text;
                    familyRegister.NumberofChildenAlive = Convert.ToInt32(txtNoOfChild.Text);
                    if (txtNoOfchildDied.Text != null)
                    {
                        familyRegister.NumberofChildenDied = Convert.ToInt32(txtNoOfchildDied.Text);
                    }
                    else
                    {
                        familyRegister.NumberofChildenDied = null;
                    }
                    var selectecast = (ColumnValue)ddlcast.SelectedItem;
                    // int selectecastID = selectecast.columnValueId;
                    familyRegister.CasteTribe = selectecast == null ? (int?)null : selectecast.columnValueId;
                    var SourceWater = (ColumnValue)ddlSourceWater.SelectedItem;
                    familyRegister.SourceDrinkingWater = SourceWater == null ? (int?)null : SourceWater.columnValueId;
                    familyRegister.CasteTribeName = txtcasttriblename.Text;
                    var selectedReligion = (ColumnValue)ddlReligion.SelectedItem;
                    int? selectedReligionID = selectedReligion == null ? (int?)null : selectedReligion.columnValueId;
                    familyRegister.Religion = selectedReligionID;
                    var selectedRationcard = (ColumnValue)ddlTofRation.SelectedItem;
                    // int RationType = selectedRationcard.columnValueId;
                    familyRegister.RationCardType = selectedRationcard == null ? (int?)null : selectedRationcard.columnValueId;
                    familyRegister.IsMotherWorking = MotherWorks;

                    familyRegister.OwnAgriculturalLand = AgriculturalLand == "Yes" ? true : false;
                    familyRegister.HouseType = HouseId;
                    var selectedWater = (ColumnValue)ddlWaterType.SelectedItem;
                    // int WaterTypeId = selectedWater.columnValueId;
                    familyRegister.WaterSupplyType = selectedWater == null ? (int?)null : selectedWater.columnValueId;
                    familyRegister.Assets = StaticClass.txtAssetsDetails;
                    familyRegister.ElectricityAvailable = selectedElectricityName == "Available" ? true : false;
                    //familyRegister.CookingGasAvailable = selectedGasName == "Yes" ? true : false;
                    familyRegister.DOU = DateTime.Now;
                    familyRegister.RegisterDate = txtdateofEntry.Date;
                    familyRegister.CreatedBy = 0;
                    familyRegister.UpdatedBy = 0;
                    //  var selecteLocatio = (ColumnValue)ddlLocation.SelectedItem;
                    // int selecteLocationID = selecteLocatio.columnValueId;
                   // familyRegister.LocationId = StaticClass.VillageID;
                    var selecteMotherWorkType = (ColumnValue)ddlMotherWorkType.SelectedItem;
                    // int selecteMotherWorkTypeId = selecteMotherWorkType.columnValueId;
                    familyRegister.MotherWorkType = selecteMotherWorkType == null ? (int?)null : selecteMotherWorkType.columnValueId;
                    var selecteMotherWorkHours = (ColumnValue)ddlMotherWorkHours.SelectedItem;
                    // int selecteMotherWorkHoursId = selecteMotherWorkHours.columnValueId;
                    familyRegister.MotherWorkHours = selecteMotherWorkHours == null ? (int?)null : selecteMotherWorkHours.columnValueId;
                    var selecteToiletFacility = (ColumnValue)ddlToiletFacility.SelectedItem;
                    //int selecteToiletFacilityId = selecteToiletFacility.columnValueId;
                    familyRegister.ToiletFacility = selecteToiletFacility == null ? (int?)null : selecteToiletFacility.columnValueId;
                    var selecteFamilyMigrateToEarn = (ColumnValue)ddlFamilyMigrateToEarn.SelectedItem;
                    //int selecteFamilyMigrateToEarnId = selecteFamilyMigrateToEarn.columnValueId;
                    familyRegister.FamilyMigrateToEarn = selecteFamilyMigrateToEarn == null ? (int?)null : selecteFamilyMigrateToEarn.columnValueId;
                    var selecteMigrationFor = (ColumnValue)ddlMigrationFor.SelectedItem;
                    // int selecteMigrationForId = selecteMigrationFor.columnValueId;
                    familyRegister.MigrationFor = selecteMigrationFor == null ? (int?)null : selecteMigrationFor.columnValueId;
                    familyRegister.MigrationMonthsPerYear = txtMigrationMonthsPerYear.Text == null ? (int?)null : Convert.ToInt32(txtMigrationMonthsPerYear.Text);
                    familyRegister.NewFamilyCode = Convert.ToInt32(txtFamilyCode.Text);
                    familyRegister.LocationId = StaticClass.VillageID;
                    var selecteStatus = (ColumnValue)ddlStatus.SelectedItem;

                    //  int selecteStatusId = selecteStatus.columnValueId;
                    familyRegister.status = selecteStatus == null ? 0 : selecteStatus.columnValueId;
                    // familyRegister.MotherWorkType = selecteMotherWorkTypeId;
                    App.DAUtil.SaveFamilyData(familyRegister);
                    StaticClass.txtAssetsDetails = null;
                    StaticClass.FamilyEdit = "False";
                    StaticClass.TabbedIndex = 0;
                    StaticClass.MotherOccupation = null;
                    EmptyText();
                    StaticClass.PageName = "HomePage";
                    Application.Current.MainPage = new MasterNavigationPage();
                }
                catch (Exception ex)
                {

                }
            }
        }
        private void Validation()
        {
            IsValidation = true;
            //if ((ColumnValue)ddlFType.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select family type");
            //    IsValidation = false;
            //    ddlFType.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtNoOfChild.Text))
            //{
            //    DependencyService.Get<Toast>().Show("Please enter no of children");
            //    IsValidation = false;
            //    txtNoOfChild.Focus();
            //    return;

            //}
            if (string.IsNullOrEmpty(txtFName.Text))
            {
                DependencyService.Get<Toast>().Show("Please enter father's name");
                IsValidation = false;
                txtFName.Focus();
                return;
            }
            //if ((ColumnValue)ddFEducation.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select father's education");
            //    IsValidation = false;
            //    ddFEducation.Focus();
            //    return;
            //}
            //if ((ColumnValue)ddlFOccupation.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select father's occupation");
            //    IsValidation = false;
            //    ddlFOccupation.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtMName.Text))
            //{

            //    DependencyService.Get<Toast>().Show("Please enter mother's name");
            //    IsValidation = false;
            //    txtMName.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtMotherAge.Text))
            //{

            //    DependencyService.Get<Toast>().Show("Please enter mother's Age");
            //    IsValidation = false;
            //    txtMotherAge.Focus();
            //    return;
            //}
            //if ((ColumnValue)ddlMEducation.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select mother's education");
            //    IsValidation = false;
            //    ddlMEducation.Focus();
            //    return;
            //}
            //if ((ColumnValue)ddlMOccupation.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select mother's occupation");
            //    IsValidation = false;
            //    ddlMOccupation.Focus();
            //    return;
            //}
            //if (string.IsNullOrEmpty(txtMPDisease.Text))
            //{
            //    DependencyService.Get<Toast>().Show("Please enter mother's past disease");
            //    IsValidation = false;
            //    txtMPDisease.Focus();
            //    return;
            ////}
            //if (stacmothergooutforwork.IsVisible == true)
            //{
            //    if ((ColumnValue)ddlIsMotherWork.SelectedItem == null)
            //    {
            //        DependencyService.Get<Toast>().Show(" Please select is mother working");
            //        IsValidation = false;
            //        ddlIsMotherWork.Focus();
            //        return;
            //    }
            //}
            ////if ((ColumnValue)ddlMotherWorkType.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select mother work type");
            //    IsValidation = false;
            //    return;
            //}

            //if ((ColumnValue)ddlcast.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select Cast/Tribe");
            //    IsValidation = false;
            //    ddlcast.Focus();
            //    return;
            //}
            //if ((ColumnValue)ddlReligion.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select religion");
            //    IsValidation = false;
            //    ddlReligion.Focus();
            //    return;
            //}
            //if ((ColumnValue)ddlTofRation.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select ration card type");
            //    IsValidation = false;
            //    return;
            //}
            //if ((OwnAgriculturalLand)ddlAgriculturalLand.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select Own agricultural land");
            //    IsValidation = false;
            //    ddlAgriculturalLand.Focus();
            //    return;
            //}
            //if ((ColumnValue)ddlHouseType.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select house type");
            //    IsValidation = false;
            //    ddlHouseType.Focus();
            //    return;
            //}

            //if ((ElectricityAvailable)ddlElictricityAvailable.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select electricity supply available");
            //    IsValidation = false;
            //    ddlElictricityAvailable.Focus();
            //    return;
            //}
            //if ((CookingGasAvailable)ddlCookingGasAvailable.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select cooking gas available");
            //    IsValidation = false;
            //    return;
            //}

            //if ((ColumnValue)ddlMotherWorkHours.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select mother work hours");
            //    IsValidation = false;
            //    return;
            //}
            //if ((ColumnValue)ddlToiletFacility.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select toilet facility");
            //    IsValidation = false;
            //    return;
            //}
            //if (stacmigrate.IsVisible == true)
            //{
            //    if ((ColumnValue)ddlFamilyMigrateToEarn.SelectedItem == null)
            //    {
            //        DependencyService.Get<Toast>().Show("Please select family migrate to earn");
            //        IsValidation = false;
            //        ddlFamilyMigrateToEarn.Focus();
            //        return;
            //    }
            //    if (string.IsNullOrEmpty(txtMigrationMonthsPerYear.Text))
            //    {

            //        DependencyService.Get<Toast>().Show("Please enter migration months per year");
            //        IsValidation = false;
            //        txtMigrationMonthsPerYear.Focus();
            //        return;
            //    }
            //    if (txtMigrationMonthsPerYear.TextColor == Color.Red)
            //    {
            //        DependencyService.Get<Toast>().Show("Please enter valid migration months per year in month");
            //        IsValidation = false;
            //        return;
            //    }
            //}
            //if ((ColumnValue)ddlMigrationFor.SelectedItem == null)
            //{
            //    DependencyService.Get<Toast>().Show("Please select migration for");
            //    IsValidation = false;
            //    return;
            //}
            if (string.IsNullOrEmpty(txtFamilyCode.Text))
            {

                DependencyService.Get<Toast>().Show("Please enter Family Code");
                IsValidation = false;
                txtFamilyCode.Focus();
                return;
            }
            if ((ColumnValue)ddlStatus.SelectedItem == null)
            {
                DependencyService.Get<Toast>().Show("Please select Status");
                IsValidation = false;
                ddlStatus.Focus();
                return;
            }

            //if (string.IsNullOrEmpty(txtNoOfchildDied.Text))
            //{
            //    DependencyService.Get<Toast>().Show("Please enter No Of Children Died");
            //    IsValidation = false;
            //    return;

            //    //IsValidation = false;
            //    //txtNoOfchildDied.Focus();
            //    //return;
            //}





        }
        private void EmptyText()
        {

            //  lblFType.IsVisible = false;
            lblFName.IsVisible = false;


            lblFOccupation.IsVisible = false;
            lblFPDisease.IsVisible = false;
            lblMName.IsVisible = false;

            // lblMEducation.IsVisible = true;
            lblMOccupation.IsVisible = false;
            lblMPDisease.IsVisible = false;
            lblNoOfChild.IsVisible = false;
            lblNoOfchildDied.IsVisible = false;
            // lblcast.IsVisible = false;
            // lblReligion.IsVisible = false;
            //lblAssetsDetails.IsVisible = false;

            //txtFType.Text = "";
            txtFName.Text = "";

            // ddlFEducation.Text = "";
            //txtFOccupation.Text = "";
            txtFPDisease.Text = "";
            txtMName.Text = "";

            // txtMEducation.Text = "";
            // txtMOccupation.Text = "";
            txtMPDisease.Text = "";
            txtNoOfChild.Text = "";
            txtNoOfchildDied.Text = "";
            // txtcast.Text = "";
            //txtReligion.Text = "";

            txtAssetsDetails.Text = "";


        }
        private void EditFamily()
        {
            //  FindFamilyId
            StaticClass.FamilyEdit = "True";
            var checkFamilydata = App.DAUtil.FindFamilyId(StaticClass.FamilyId);

            if (checkFamilydata.Count > 0)
            {
                LableShow();
                try
                {
                    var ListOfFamilyType = App.DAUtil.GetColumnValuesBytext(7);
                    for (int i = 0; i < ListOfFamilyType.Count; i++)
                    {
                        if (checkFamilydata[0].FamilyType == ListOfFamilyType[i].columnValueId)
                        {
                            ddlFType.SelectedIndex = i;
                        }
                    }

                    var ListStatus = App.DAUtil.GetColumnValuesBytext(55);
                    for (int i = 0; i < ListStatus.Count; i++)
                    {
                        if (checkFamilydata[0].status == ListStatus[i].columnValueId)
                        {
                            ddlStatus.SelectedIndex = i;
                        }
                    }
                    var IsMotherWorking = App.DAUtil.GetColumnValuesBytext(65);
                    for (int i = 0; i < IsMotherWorking.Count; i++)
                    {
                        if (checkFamilydata[0].IsMotherWorking == IsMotherWorking[i].columnValueId)
                        {
                            ddlIsMotherWork.SelectedIndex = i;
                        }
                    }


                    var Rationcard = App.DAUtil.GetColumnValuesBytext(2);
                    for (int i = 0; i < Rationcard.Count; i++)
                    {
                        if (checkFamilydata[0].RationCardType == Rationcard[i].columnValueId)
                        {
                            ddlTofRation.SelectedIndex = i;
                        }
                    }
                    //  ddlTofRation.SelectedIndex = checkFamilydata[0].RationCardType;
                    ddlAgriculturalLand.SelectedIndex = checkFamilydata[0].OwnAgriculturalLand == true ? 0 : 1;
                    var ListOfHouse = App.DAUtil.GetColumnValuesBytext(6);
                    for (int i = 0; i < ListOfHouse.Count; i++)
                    {
                        if (checkFamilydata[0].HouseType == ListOfHouse[i].columnValueId)
                        {
                            ddlHouseType.SelectedIndex = i;
                        }
                    }
                    //  ddlHouseType.SelectedIndex = checkFamilydata[0].HouseType;
                    var ListOfWaterType = App.DAUtil.GetColumnValuesBytext(10);
                    for (int i = 0; i < ListOfWaterType.Count; i++)
                    {
                        if (checkFamilydata[0].WaterSupplyType == ListOfWaterType[i].columnValueId)
                        {
                            ddlWaterType.SelectedIndex = i;
                        }
                    }
                    // ddlWaterType.SelectedIndex = checkFamilydata[0].WaterSupplyType;
                    ddlElictricityAvailable.SelectedIndex = checkFamilydata[0].ElectricityAvailable == true ? 0 : 1;
                    //  ddlCookingGasAvailable.SelectedIndex = checkFamilydata[0].CookingGasAvailable==true ? 0 : 1;
                    long selectedLocationId = StaticClass.VillageID;


                    txtFName.Text = checkFamilydata[0].FatherName;

                    var ListOfFatherEducation = App.DAUtil.GetColumnValuesBytext(11);
                    for (int i = 0; i < ListOfFatherEducation.Count; i++)
                    {
                        if (checkFamilydata[0].FatherEducation == ListOfFatherEducation[i].columnValueId)
                        {
                            ddFEducation.SelectedIndex = i;
                        }
                    }
                    //var ListOfFatherOccupation = App.DAUtil.GetColumnValuesBytext(64);
                    //for (int i = 0; i < ListOfFatherOccupation.Count; i++)
                    //{
                    //    if (checkFamilydata[0].FatherOccupation == ListOfFatherOccupation[i].columnValueId)
                    //    {
                    //        ddlFOccupation.SelectedIndex = i;
                    //    }
                    //}
                    //var ListOfMotherOccupation = App.DAUtil.GetColumnValuesBytext(64);
                    //for (int i = 0; i < ListOfMotherOccupation.Count; i++)
                    //{
                    //    if (checkFamilydata[0].MotherOccupation == ListOfMotherOccupation[i].columnValueId)
                    //    {
                    //        ddlMOccupation.SelectedIndex = i;
                    //    }
                    //}
                    var ListOfSourceWater = App.DAUtil.GetColumnValuesBytext(60);
                    for (int i = 0; i < ListOfSourceWater.Count; i++)
                    {
                        if (checkFamilydata[0].SourceDrinkingWater == ListOfSourceWater[i].columnValueId)
                        {
                            ddlSourceWater.SelectedIndex = i;
                        }
                    }
                    //ddFEducation.SelectedIndex = checkFamilydata[0].FatherEducation;
                    txtcasttriblename.Text = checkFamilydata[0].CasteTribeName;
                    // txtFOccupation.Text = checkFamilydata[0].FatherOccupation;
                    txtFPDisease.Text = checkFamilydata[0].FatherPastDisease;
                    txtMName.Text = checkFamilydata[0].MotherName;

                    if (checkFamilydata[0].MotherDOB == null)
                    {
                        txtMDOB1.IsVisible = true;
                        txtMDOBP.IsVisible = false;
                    }
                    else
                    {
                        txtMDOB1.IsVisible = false;
                        txtMDOBP.IsVisible = true;

                        txtMDOB.Date = Convert.ToDateTime(checkFamilydata[0].MotherDOB);
                    }
                    var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(11);
                    for (int i = 0; i < ListOfMotherEducation.Count; i++)
                    {
                        if (checkFamilydata[0].MotherEducation == ListOfMotherEducation[i].columnValueId)
                        {
                            ddlMEducation.SelectedIndex = i;
                        }
                    }
                    // ddlMEducation.SelectedIndex = checkFamilydata[0].MotherEducation;

                    //  txtMOccupation.Text = checkFamilydata[0].MotherOccupation;
                    txtMPDisease.Text = checkFamilydata[0].MotherPastDisease;
                    txtNoOfChild.Text = checkFamilydata[0].NumberofChildenAlive.ToString();
                    txtNoOfchildDied.Text = checkFamilydata[0].NumberofChildenDied.HasValue ? checkFamilydata[0].NumberofChildenDied.ToString() : null;
                    var ListOfCast = App.DAUtil.GetColumnValuesBytext(8);
                    for (int i = 0; i < ListOfCast.Count; i++)
                    {
                        if (checkFamilydata[0].CasteTribe == ListOfCast[i].columnValueId)
                        {
                            ddlcast.SelectedIndex = i;
                        }
                    }
                    // ddlcast.SelectedIndex = checkFamilydata[0].CasteTribe;
                    var ListOfReligiont = App.DAUtil.GetColumnValuesBytext(9);
                    for (int i = 0; i < ListOfReligiont.Count; i++)
                    {
                        if (checkFamilydata[0].Religion == ListOfReligiont[i].columnValueId)
                        {
                            ddlReligion.SelectedIndex = i;
                        }
                    }
                    // ddlReligion.SelectedIndex = checkFamilydata[0].Religion;
                   //  txtAssetsDetails.Text = checkFamilydata[0].Assets;
                    txtdateofEntry.IsEnabled = false;
                    txtdateofEntry.Date = checkFamilydata[0].RegisterDate;
                    // familyRegister.LocationId = selectedLocationId;
                    // txtdateofEntry.Date = checkFamilydata[0].RegisterDate;
                    // txtdateofEntry.Date = checkFamilydata[0].DOE;
                    // ddlLocation.SelectedIndex = checkFamilydata[0].LocationId;  pending work
                    var ListOfFamily = App.DAUtil.GetColumnValuesBytext(13);
                    for (int i = 0; i < ListOfFamily.Count; i++)
                    {
                        if (checkFamilydata[0].MotherWorkType == ListOfFamily[i].columnValueId)
                        {
                            ddlMotherWorkType.SelectedIndex = i;
                        }
                    }
                    // ddlMotherWorkType.SelectedIndex = checkFamilydata[0].MotherWorkType;
                    var MotherWorkhours = App.DAUtil.GetColumnValuesBytext(14);
                    for (int i = 0; i < MotherWorkhours.Count; i++)
                    {
                        if (checkFamilydata[0].MotherWorkHours == MotherWorkhours[i].columnValueId)
                        {
                            ddlMotherWorkHours.SelectedIndex = i;
                        }
                    }
                    // ddlMotherWorkHours.SelectedIndex = checkFamilydata[0].MotherWorkHours;
                    var ListOfFacility = App.DAUtil.GetColumnValuesBytext(15);
                    for (int i = 0; i < ListOfFacility.Count; i++)
                    {
                        if (checkFamilydata[0].ToiletFacility == ListOfFacility[i].columnValueId)
                        {
                            ddlToiletFacility.SelectedIndex = i;
                        }
                    }
                    // ddlToiletFacility.SelectedIndex = checkFamilydata[0].ToiletFacility;
                    var ListOfMigrateToEarn = App.DAUtil.GetColumnValuesBytext(16);
                    for (int i = 0; i < ListOfMigrateToEarn.Count; i++)
                    {
                        if (checkFamilydata[0].FamilyMigrateToEarn == ListOfMigrateToEarn[i].columnValueId)
                        {
                            ddlFamilyMigrateToEarn.SelectedIndex = i;
                        }
                    }

                    //  ddlFamilyMigrateToEarn.SelectedIndex = checkFamilydata[0].FamilyMigrateToEarn;
                    var ListOfMigrationFor = App.DAUtil.GetColumnValuesBytext(17);
                    for (int i = 0; i < ListOfMigrationFor.Count; i++)
                    {
                        if (checkFamilydata[0].MigrationFor == ListOfMigrationFor[i].columnValueId)
                        {
                            ddlMigrationFor.SelectedIndex = i;
                        }
                    }
                    //  ddlMigrationFor.SelectedIndex = checkFamilydata[0].MigrationFor;
                    if (checkFamilydata[0].MigrationMonthsPerYear == 0)
                    {
                        txtMigrationMonthsPerYear.Text = "";
                    }
                    else
                    {
                        txtMigrationMonthsPerYear.Text = checkFamilydata[0].MigrationMonthsPerYear.HasValue ? checkFamilydata[0].MigrationMonthsPerYear.ToString() : null;
                        txtFamilyCode.Text = checkFamilydata[0].NewFamilyCode.ToString();
                    }
                }
                catch (Exception ex)
                {

                }
            }

        }
        private void LableShow()
        {

            lblFType.IsVisible = true;
            lblFName.IsVisible = true;

            lblFEducation.IsVisible = true;
            lblFOccupation.IsVisible = true;
            lblFPDisease.IsVisible = true;
            lblMName.IsVisible = true;

            lblMEducation.IsVisible = true;
            lblMOccupation.IsVisible = true;
            lblMPDisease.IsVisible = true;
            lblNoOfChild.IsVisible = true;
            lblNoOfchildDied.IsVisible = true;
            lblcast.IsVisible = true;
            lblReligion.IsVisible = true;
            // lblAssetsDetails.IsVisible = true;

        }
        private void LabelHide()
        {


            lblFType.IsVisible = false;
            lblFName.IsVisible = false;

            lblFEducation.IsVisible = false;
            lblFOccupation.IsVisible = false;
            lblFPDisease.IsVisible = false;
            lblMName.IsVisible = false;

            lblMEducation.IsVisible = true;
            lblMOccupation.IsVisible = false;
            lblMPDisease.IsVisible = false;
            lblNoOfChild.IsVisible = false;
            lblNoOfchildDied.IsVisible = false;
            lblcast.IsVisible = true;
            lblReligion.IsVisible = true;
            //  lblAssetsDetails.IsVisible = false;

        }



        private void TxtFamilyCode_Focused(object sender, FocusEventArgs e)
        {

        }

        private void TxtFamilyCode_Unfocused(object sender, FocusEventArgs e)
        {

        }

        private async void TxtAssetsDetails_Clicked(object sender, EventArgs e)
        {
            var page = new AssetsPage();
            await Navigation.PushPopupAsync(page);
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            txtMDOB.Focus();
            //txtMDOB1.IsVisible = false;
        }

        private void TxtMDOB_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtMDOB1.IsVisible = false;
            txtMDOBP.IsVisible = true;
            IsShowDOB = false;
            DateTime currentYear = DateTime.Now;
            int MDob = txtMDOB.Date.Year;
            int yearNumber = currentYear.Year - MDob;
            txtMotherAge.Text = yearNumber.ToString();
         


        }

        private void Ddlcast_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedWater = (ColumnValue)ddlcast.SelectedItem;
            if (selectedWater.columnValueId != 124)
            {
                staccastName.IsVisible = true;
            }
            else
            {
                staccastName.IsVisible = false;
            }
        }

        private void TxtMotherAge_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrEmpty(txtMotherAge.Text))
            {
                txtMDOB1.IsVisible = true;
                txtMDOBP.IsVisible = false;
            }
            else
            {
                if (btnSave.Text == "Update")
                {
                    if (IsShowDOB == false)
                    {
                        txtMDOB1.IsVisible = false;
                        txtMDOBP.IsVisible = true;
                        txtMDOB.Date = txtMDOB.Date;
                        IsShowDOB = true;
                    }
                    else
                    {
                        txtMDOB1.IsVisible = false;
                        txtMDOBP.IsVisible = true;
                        DateTime currentYear = DateTime.Now;

                        int yearNumber = currentYear.Year - Convert.ToInt32(txtMotherAge.Text);
                        DateTime DOB = new DateTime(yearNumber, 1, 1);
                        txtMDOB.Date = DOB;
                    }
                }
                else
                {
                    txtMDOB1.IsVisible = false;
                    txtMDOBP.IsVisible = true;
                    DateTime currentYear = DateTime.Now;

                    int yearNumber = currentYear.Year - Convert.ToInt32(txtMotherAge.Text);
                    DateTime DOB = new DateTime(yearNumber, 1, 1);
                    txtMDOB.Date = DOB;

                }
               
            }

        }

        private void DdlIsMotherWork_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedMotherWork = (ColumnValue)ddlIsMotherWork.SelectedItem;
            string MotherWorks = selectedMotherWork.columnValue;
            if (MotherWorks == "No")
            {
                stacmotherwork.IsVisible = false;
            }
            else
            {
                stacmotherwork.IsVisible = true;
            }
        }

        private void TxtMigrationMonthsPerYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string txtnoofchild = txtMigrationMonthsPerYear.Text.Replace(".", "");
                txtMigrationMonthsPerYear.Text = txtnoofchild;

                // var entry = (Entry)sender;
                int value = Convert.ToInt32(txtMigrationMonthsPerYear.Text);
                if (value >= 0 && value <= 12)
                {
                    string entryText = txtMigrationMonthsPerYear.Text;
                    txtMigrationMonthsPerYear.Text = entryText;
                    txtMigrationMonthsPerYear.TextColor = Color.Black;
                }
                else
                {
                    txtMigrationMonthsPerYear.TextColor = Color.Red;
                }
            }
            catch
            {

            }
        }

        private void TxtNoOfChild_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string txtnoofchild = txtNoOfChild.Text.Replace(".", "");
                txtNoOfChild.Text = txtnoofchild;
            }
            catch
            {

            }
        }

        private void TxtNoOfchildDied_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string txtNoOfchild = txtNoOfchildDied.Text.Replace(".", "");
                txtNoOfchildDied.Text = txtNoOfchild;
            }
            catch
            {

            }
        }

        //private void DdlMOccupation_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    var selectedMotherWork = (ColumnValue)ddlMOccupation.SelectedItem;
        //    string MotherWorks = selectedMotherWork.columnValue;
        //    if (MotherWorks == "Not working")
        //    {
        //        stacmothergooutforwork.IsVisible = false;
        //    }
        //    else
        //    {
        //        stacmothergooutforwork.IsVisible = true;
        //    }
        //}

        private void DdlFamilyMigrateToEarn_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedFamilyMigrateToEarn = (ColumnValue)ddlFamilyMigrateToEarn.SelectedItem;
            string MigrateToEarn = selectedFamilyMigrateToEarn.columnValue;
            if (MigrateToEarn == "Don't migrate")
            {
                stacmigrate.IsVisible = false;
            }
            else
            {
                stacmigrate.IsVisible = true;
            }

        }

        private async void DdlMOccupation_Clicked(object sender, EventArgs e)
        {
            var page = new MotherOccoupation();
            await Navigation.PushPopupAsync(page);
        }

        private async void DdlFOccupation_Clicked(object sender, EventArgs e)
        {
            var page = new FatherOccoupation();
            await Navigation.PushPopupAsync(page);
        }
    }
}