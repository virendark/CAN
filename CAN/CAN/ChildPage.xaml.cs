using CAN.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Text.RegularExpressions;
using CAN.Helper;

namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildPage : ContentPage
    {
        private MediaFile _mediaFile;
        private string URL { get; set; }
        bool IsValidate = true;
        public double W4LHZ;
        public double W4AZ;
        public double H4AZ;
        public double BMI;
        public double BMIZ;
        double? w4az;
        double? w4hz;
        public ChildPage()
        {
            InitializeComponent();

            txtDOB2.IsVisible = false;
            txtAWDOB2.IsVisible = false;
            // lblFCName.IsVisible = false;
            // lblBWeight.IsVisible = false;

            // lblBOrder.IsVisible = false;
            bindIllness();
            BindGender();
        
            BindChildStatus();
            BindddlDeliveryTerm();
            BindBirthDeliveryType();
            BindBirthPlace();
            BindChildrecordedWeightforHeight();
            BindChildrecordeds();
            this.Title = StaticClass.LocationName;
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
            txtDOB.MaximumDate = new DateTime(year, month, day);
            txtAWDOB.MaximumDate= new DateTime(year, month, day);
            txtEntryRegistor.MaximumDate= new DateTime(year, month, day);
        }
        private void bindIllness()
        {
            var Check = App.DAUtil.FindFamilyId(StaticClass.PageData);
            string FMilleness;
            if(Check.Count>0)
            {
                if(Check[0].FatherPastDisease!=null)
                {
                    FMilleness = "Father(" + Check[0].FatherPastDisease + ") Mother(" + Check[0].MotherPastDisease + ")";
                    txtAnyLongTermIllnessInFamily.Text = FMilleness;
                }
                else
                {
                    txtAnyLongTermIllnessInFamily.Text = "Mother("+ Check[0].MotherPastDisease+")";
                }
            }
        }
        private void BindChildStatus()
        {
            var ListofCStatus = App.DAUtil.GetColumnValuesBytext(12);
            ddlChildStatus.ItemsSource = ListofCStatus;
            ddlChildStatus.SelectedIndex = 0;
        }
        private void BindChildrecordeds()
        {
            var ListofChildrecorded = App.DAUtil.GetColumnValuesBytext(66);
            ddlChildrecorded.ItemsSource = ListofChildrecorded;
        }
        private void BindChildrecordedWeightforHeight()
        {
            var ListofChildrecordedWeightforHeight = App.DAUtil.GetColumnValuesBytext(67);
            ddlChildrecordedWeightforHeight.ItemsSource = ListofChildrecordedWeightforHeight;
        }
        private void BindBirthPlace()
        {
            var ListofCStatus = App.DAUtil.GetColumnValuesBytext(18);
            ddlBirthPlace.ItemsSource = ListofCStatus;
        }
        private void BindBirthDeliveryType()
        {
            var ListofCStatus = App.DAUtil.GetColumnValuesBytext(47);
            ddlBirthDeliveryType.ItemsSource = ListofCStatus;
        }
        private void BindddlDeliveryTerm()
        {
            var ListofDeliveryTerm = App.DAUtil.GetColumnValuesBytext(56);
            ddlDeliveryTerm.ItemsSource = ListofDeliveryTerm;
        }
        private void BindGender()
        {
            var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
            ddlGender.ItemsSource = ListofGender;
        }
        private void txtFCName_Focused(object sender, FocusEventArgs e)
        {
            lblFCName.IsVisible = true;
            if (txtFCName.Text == null || txtFCName.Text.Length == 0)
            {
                txtFCName.Placeholder = string.Empty;
            }
        }
        private void txtFCName_Unfocused(object sender, FocusEventArgs e)
        {
            if (txtFCName.Text == null || txtFCName.Text.Length == 0)
            {
                txtFCName.Placeholder = "Child Name";
                lblFCName.IsVisible = false;
            }
        }
        private void txtBWeight_Focused(object sender, FocusEventArgs e)
        {
            lblBWeight.IsVisible = true;
            if (txtBWeight.Text == null || txtBWeight.Text.Length == 0)
            {
                txtBWeight.Placeholder = string.Empty;
            }
        }
        private void txtBWeight_Unfocused(object sender, FocusEventArgs e)
        {
            if (txtBWeight.Text == null || txtBWeight.Text.Length == 0)
            {
                txtBWeight.Placeholder = "Birth Weight";
                lblBWeight.IsVisible = false;
            }
        }
        private void txtBOrder_Focused(object sender, FocusEventArgs e)
        {
            lblBOrder.IsVisible = true;
            //if (txtBOrder.Text == null || txtBOrder.Text.Length == 0)
            //{
            //    txtBOrder.Placeholder = string.Empty;
            //}
        }
        private void txtBOrder_Unfocused(object sender, FocusEventArgs e)
        {
            //if (txtBOrder.Text == null || txtBOrder.Text.Length == 0)
            //{
            //    txtBOrder.Placeholder = "Birth Order";
            //    lblBOrder.IsVisible = false;
            //}
            //try
            //{
                
            //    var checkNoOfChild = App.DAUtil.FindFamilyId(StaticClass.PageData).FirstOrDefault();
            //    if (Convert.ToInt32(txtBOrder.Text) <= checkNoOfChild.NumberofChildenAlive)
            //    {

            //    }
            //    else
            //    {
            //        txtBOrder.Text = "";
            //        DependencyService.Get<Toast>().Show("Please Enter currect number of child alive");
            //        IsValidate = false;
            //        txtBOrder.Focus();
            //        return;
            //    }
            //}
            //catch
            //{

            //}

        }
        private void btnSave_Clicked(object sender, EventArgs e)
        {

            Validation();
            try
            {
                if (IsValidate == true)
                {
                    var selectedGender = (ColumnValue)ddlGender.SelectedItem;
                    int GenderName = selectedGender==null?0: selectedGender.columnValueId;
                    var selectedDeliveryTerm = (ColumnValue)ddlDeliveryTerm.SelectedItem;
                    int ? DeliveryTerm = selectedDeliveryTerm == null ? (int?)null : selectedDeliveryTerm.columnValueId;
                    ChildRegister childRegister = new ChildRegister();
                    if (StaticClass.PageButtonText == "Update")
                    {
                        var checkChildData = App.DAUtil.FindChildByData(StaticClass.ChildID.ToString());
                        childRegister.DOE = checkChildData[0].DOE;
                        childRegister.ChildId = checkChildData[0].ChildId; //StaticClass.ChildID;
                        childRegister.FamilyId = checkChildData[0].FamilyId; //StaticClass.PageData;
                        App.DAUtil.DeletChildById(StaticClass.ChildID.ToString());

                    }
                    else
                    {
                        childRegister.ChildId = Guid.NewGuid();
                        childRegister.FamilyId = StaticClass.PageData;
                        childRegister.DOE = DateTime.Now;

                    }

                    childRegister.DeliveryTerm = DeliveryTerm;
                    childRegister.ChildName = txtFCName.Text;
                    childRegister.DOB = txtDOB.Date;

                    childRegister.GenderID = GenderName;
                    if (!String.IsNullOrEmpty(txtBOrder.Text))
                    {
                        childRegister.BirthOrder = Convert.ToInt32(txtBOrder.Text);
                    }
                    else
                    {
                        childRegister.BirthOrder = null;
                    }
                    childRegister.RegisterDate = txtEntryRegistor.Date;

                    childRegister.DOU = DateTime.Now;
                    var selectedchildid = (ColumnValue)ddlChildStatus.SelectedItem;
                    int CStatusID = selectedchildid.columnValueId;
                    childRegister.ChildStatus = CStatusID;
                    childRegister.CreatedBy = 0;
                    childRegister.UpdatedBy = 0;
                    childRegister.BirthLengthHeightInCms = txtBirthLengthHeightInCms.Text==null?(decimal?)null: Convert.ToDecimal(txtBirthLengthHeightInCms.Text);
                    childRegister.BirthWeightInKg = txtBWeight.Text==null?(decimal?)null: Convert.ToDecimal(txtBWeight.Text);
                    var selectedBirthPlace = (ColumnValue)ddlBirthPlace.SelectedItem;
                    int? selectedBirthPlaceId = selectedBirthPlace==null?(int?)null: selectedBirthPlace.columnValueId;
                    childRegister.BirthPlace = selectedBirthPlaceId;
                    var selectedBirthDeliveryType = (ColumnValue)ddlBirthDeliveryType.SelectedItem;
                    int? selectedBirthDeliveryTypeId = selectedBirthDeliveryType==null?(int?)null: selectedBirthDeliveryType.columnValueId;
                    childRegister.BirthDeliveryType = selectedBirthDeliveryTypeId;
                    childRegister.AWCEntryWeightInKG = txtAWCEntryWeightInKG.Text==null?(decimal?)null: Convert.ToDecimal(txtAWCEntryWeightInKG.Text);
                    childRegister.AWCEntryHeightInCms = txtAWCEntryHeightInCms.Text==null?(decimal?)null: Convert.ToDecimal(txtAWCEntryHeightInCms.Text);
                    childRegister.AWCEntryMUAC = txtAWCEntryMUAC.Text==null?(decimal?)null: Convert.ToDecimal(txtAWCEntryMUAC.Text);
                    childRegister.AWCEntryW4AZ = w4az==null?(decimal?)null: Convert.ToDecimal(w4az);
                    childRegister.AWCEntryW4HZ = w4hz==null?(decimal?)null: Convert.ToDecimal(w4hz);
                    childRegister.ChildCode = txtChildCode.Text;
                    childRegister.Photograph = aBC.imgName;
                    childRegister.AnyDisability = txtAnyDisability.Text==null? "Not Applicable": txtAnyDisability.Text;
                    childRegister.AnyIllness = txtAnyIllness.Text;
                    childRegister.AnyLongTermIllnessInFamily = txtAnyLongTermIllnessInFamily.Text;
                    var SelectedChildrecorded = (ColumnValue)ddlChildrecorded.SelectedItem;
                    childRegister.GrowthChartGrade = SelectedChildrecorded == null ? (int?)null : SelectedChildrecorded.columnValueId;
                    var SelectedChildrecordedWeightforHeight = (ColumnValue)ddlChildrecordedWeightforHeight.SelectedItem;
                    childRegister.GradeOfChild = SelectedChildrecordedWeightforHeight == null ? (int?)null : SelectedChildrecordedWeightforHeight.columnValueId;
                    if (txtAWDOB2.IsVisible == true)
                    {
                       // childRegister.AWCEntryDate = txtAWDOB.Date;
                        childRegister.AWCEntryDate = Convert.ToDateTime(txtAWDOB.Date);
                    }
                   else
                    {
                        childRegister.AWCEntryDate = null;
                    }
                    App.DAUtil.SaveChildData(childRegister);
                    Navigation.PushAsync(new ListOfChildPage());
                    // StaticClass.PageName = "ChildPage";
                    //Application.Current.MainPage = new MasterDetailPage1();

                }
            }
            catch
            {

            }
        }
        private void Validation()
           {
            try
            {
                IsValidate = true;
                if (string.IsNullOrEmpty(txtFCName.Text))
                {
                    DependencyService.Get<Toast>().Show("Please Enter Child Name");
                    IsValidate = false;
                    txtFCName.Focus();
                    return;
                }
                if (txtDOB2.IsVisible ==false)
                {
                    DependencyService.Get<Toast>().Show("Please select DOB");
                    IsValidate = false;
                   
                    return;
                }
                if ((ColumnValue)ddlGender.SelectedItem == null)
                {
                    DependencyService.Get<Toast>().Show("Please Select Gender");
                    IsValidate = false;
                    ddlGender.Focus();
                    return;
                }
                //if (string.IsNullOrEmpty(txtBWeight.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter Birth Weight");
                //    IsValidate = false;
                //    txtBWeight.Focus();
                //    return;
                //}
                //if (txtBWeight.TextColor==Color.Red)
                //{
                //    DependencyService.Get<Toast>().Show("Please valid Enter Birth Weight");
                //    IsValidate = false;
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtBirthLengthHeightInCms.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter Birth Length Height");
                //    IsValidate = false;
                //    txtBirthLengthHeightInCms.Focus();
                //    return;
                //}
                //if (txtBirthLengthHeightInCms.TextColor == Color.Red)
                //{
                //    DependencyService.Get<Toast>().Show("Please valid Enter Birth Length Height");
                //    IsValidate = false;
                //    return;
                //}
                //if ((ColumnValue)ddlBirthPlace.SelectedItem == null)
                //{
                //    DependencyService.Get<Toast>().Show("Please select Birth Place");
                //    IsValidate = false;
                //    ddlBirthPlace.Focus();
                //    return;
                //}
                //if ((ColumnValue)ddlBirthDeliveryType.SelectedItem == null)
                //{
                //    DependencyService.Get<Toast>().Show("Please select Birth Delivery Type");
                //    IsValidate = false;
                //    ddlBirthDeliveryType.Focus();
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtAWCEntryWeightInKG.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter  AWC Entry Weight");
                //    IsValidate = false;
                //    txtAWCEntryWeightInKG.Focus();
                //    return;
                //}
                //if (txtAWCEntryWeightInKG.TextColor == Color.Red)
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter valid  AWC Entry Weight");
                //    IsValidate = false;
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtAWCEntryHeightInCms.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter  AWC Entry Height");
                //    IsValidate = false;
                //    txtAWCEntryHeightInCms.Focus();
                //    return;
                //}
                //if (txtAWCEntryHeightInCms.TextColor == Color.Red)
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter valid AWC Entry Height");
                //    IsValidate = false;
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtAWCEntryMUAC.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter AWC Entry MUAC");
                //    IsValidate = false;
                //    txtAWCEntryMUAC.Focus();
                //    return;
                //}
                //if (txtAWCEntryMUAC.TextColor == Color.DarkRed)
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter valid AWC Entry MUAC");
                //    IsValidate = false;
                //    return;
                //}
                //if (string.IsNullOrEmpty(txtBOrder.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter Birth Order");
                //    IsValidate = false;
                //    return;
                //}
                if ((ColumnValue)ddlChildStatus.SelectedItem == null)
                {
                    DependencyService.Get<Toast>().Show("Please select Child Status");
                    IsValidate = false;
                    ddlChildStatus.Focus();
                    return;
                }
               
                //if (string.IsNullOrEmpty(txtAWCEntryW4AZ.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter AWC Entry W4AZ");
                //    IsValidate = false;
                //    return;
                //}
                //if (txtAWCEntryW4AZ.TextColor==Color.Red)
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter valid AWC Entry W4AZ");
                //    IsValidate = false;
                //    return;
                //}
                if (string.IsNullOrEmpty(txtChildCode.Text))
                {
                    DependencyService.Get<Toast>().Show("Please Enter Child Code");
                    IsValidate = false;
                    return;
                }
                //if (string.IsNullOrEmpty(txtAWCEntryW4HZ.Text))
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter AWC Entry W4HZ");
                //    IsValidate = false;
                //    return;
                //}
                //if (txtAWCEntryW4HZ.TextColor==Color.Red)
                //{
                //    DependencyService.Get<Toast>().Show("Please Enter valid AWC Entry W4HZ");
                //    IsValidate = false;
                //    return;
                //}
               
            }
            catch
            {

            }

        }
        private void EditFamily()
        {
            //  FindFamilyId
            var checkChildata = App.DAUtil.FindChildByData(StaticClass.ChildID.ToString());

            if (checkChildata.Count > 0)
            {
                // LableShow();
                try
                {

                    ChildRegister childRegister = new ChildRegister();


                    txtFCName.Text = checkChildata[0].ChildName;
                    txtDOB.Date = checkChildata[0].DOB;
                    txtDOB.IsEnabled = false;
                    txtEntryRegistor.IsEnabled = false;
                    var Childrecorded = App.DAUtil.GetColumnValuesBytext(66);
                    for (int i = 0; i < Childrecorded.Count; i++)
                    {
                        if (Childrecorded[i].columnValueId == checkChildata[0].GrowthChartGrade)
                        {
                            ddlChildrecorded.SelectedIndex = i;
                        }
                    }
                    var ChildrecordedWeightforHeight = App.DAUtil.GetColumnValuesBytext(67);
                    for (int i = 0; i < ChildrecordedWeightforHeight.Count; i++)
                    {
                        if (ChildrecordedWeightforHeight[i].columnValueId == checkChildata[0].GradeOfChild)
                        {
                            ddlChildrecordedWeightforHeight.SelectedIndex = i;
                        }
                    }
                    var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                    for (int i = 0; i < ListofGender.Count; i++)
                    {
                        if (checkChildata[0].GenderID == ListofGender[i].columnValueId)
                        {
                            ddlGender.SelectedIndex = i;
                        }
                    }
                    ddlGender.IsEnabled = false;
                    var ListofDeliveryTerm = App.DAUtil.GetColumnValuesBytext(56);
                    for (int i = 0; i < ListofDeliveryTerm.Count; i++)
                    {
                        if (checkChildata[0].DeliveryTerm == ListofDeliveryTerm[i].columnValueId)
                        {
                            ddlDeliveryTerm.SelectedIndex = i;
                        }
                    }
                    // ddlGender.SelectedIndex = checkChildata[0].GenderID;
                    txtBOrder.Text = checkChildata[0].BirthOrder.HasValue? checkChildata[0].BirthOrder.ToString():null;
                    txtEntryRegistor.Date = checkChildata[0].RegisterDate;
                    var ListofCStatus = App.DAUtil.GetColumnValuesBytext(12);
                    for (int i = 0; i < ListofCStatus.Count; i++)
                    {
                        if (checkChildata[0].ChildStatus == ListofCStatus[i].columnValueId)
                        {
                            ddlChildStatus.SelectedIndex = i;
                        }
                    }
                    var ListBirthDeliveryType = App.DAUtil.GetColumnValuesBytext(47);
                    for (int i = 0; i < ListBirthDeliveryType.Count; i++)
                    {
                        if (checkChildata[0].BirthDeliveryType == ListBirthDeliveryType[i].columnValueId)
                        {
                            ddlBirthDeliveryType.SelectedIndex = i;
                        }
                    }
                    // ddlChildStatus.SelectedIndex = checkChildata[0].ChildStatus;
                    txtBirthLengthHeightInCms.Text = checkChildata[0].BirthLengthHeightInCms.HasValue? checkChildata[0].BirthLengthHeightInCms.ToString():null;
                    txtBWeight.Text = checkChildata[0].BirthWeightInKg.HasValue? checkChildata[0].BirthWeightInKg.ToString():null;
                    var ListofBirthPlace = App.DAUtil.GetColumnValuesBytext(18);
                    for (int i = 0; i < ListofBirthPlace.Count; i++)
                    {
                        if (checkChildata[0].BirthPlace == ListofBirthPlace[i].columnValueId)
                        {
                            ddlBirthPlace.SelectedIndex = i;
                        }
                    }
                    //  ddlBirthPlace.SelectedIndex = checkChildata[0].BirthPlace;
                    var ListofddlBirthDeliveryType = App.DAUtil.GetColumnValuesBytext(18);
                    for (int i = 0; i < ListofddlBirthDeliveryType.Count; i++)
                    {
                        if (checkChildata[0].BirthDeliveryType == ListofddlBirthDeliveryType[i].columnValueId)
                        {
                            ddlBirthDeliveryType.SelectedIndex = i;
                        }
                    }
                   // ddlBirthDeliveryType.SelectedIndex = checkChildata[0].BirthDeliveryType;
                    txtAWCEntryWeightInKG.Text = checkChildata[0].AWCEntryWeightInKG.HasValue? checkChildata[0].AWCEntryWeightInKG.ToString():null;
                    txtAWCEntryHeightInCms.Text = checkChildata[0].AWCEntryHeightInCms.HasValue? checkChildata[0].AWCEntryHeightInCms.ToString():null;
                    txtAWCEntryMUAC.Text = checkChildata[0].AWCEntryMUAC.HasValue? checkChildata[0].AWCEntryMUAC.ToString():null;
                   // txtAWCEntryW4AZ.Text = checkChildata[0].AWCEntryW4AZ.ToString();
                    txtAnyDisability.Text = checkChildata[0].AnyDisability;
                    txtAnyIllness.Text = checkChildata[0].AnyIllness;
                    if(checkChildata[0].AWCEntryDate!=null)
                    {
                        txtAWDOB2.IsVisible = true;
                        txtAWDOB11.IsVisible = false;
                        txtAWDOB.Date = Convert.ToDateTime(checkChildata[0].AWCEntryDate);
                        
                    }
                   else
                    {

                        txtAWDOB11.IsVisible = true;
                        txtAWDOB2.IsVisible = false;
                    }
                    if (checkChildata[0].AWCEntryW4AZ != null)
                    {
                        w4az = Convert.ToDouble(checkChildata[0].AWCEntryW4AZ);
                        if (w4az < -3)
                        {
                            txtAWCEntryW4AZ.Text = "SUW(Severely Under Weight)";
                            txtAWCEntryW4AZ.TextColor = Color.Black;
                            txtAWCEntryW4AZ.BackgroundColor = Color.Red;
                        }
                        else
                        {
                            if (w4az <= -2)
                            {
                                txtAWCEntryW4AZ.Text = "MUW(Moderately Under Weight)";
                                txtAWCEntryW4AZ.TextColor = Color.Black;
                                txtAWCEntryW4AZ.BackgroundColor = Color.Yellow;
                            }
                            else
                            {

                                txtAWCEntryW4AZ.Text = "Normal";
                                txtAWCEntryW4AZ.TextColor = Color.Black;
                                txtAWCEntryW4AZ.BackgroundColor = Color.Green;

                            }
                        }
                    }
                    if (checkChildata[0].AWCEntryW4HZ != null)
                    {
                        w4hz = Math.Round(Convert.ToDouble(checkChildata[0].AWCEntryW4HZ));
                        if (w4hz < -3)
                        {
                            txtAWCEntryW4HZ.Text = "SAM(Severely Acute Malnutrition)";
                            txtAWCEntryW4HZ.TextColor = Color.Black;
                            txtAWCEntryW4HZ.BackgroundColor = Color.Red;
                        }
                        else
                        {
                            if (w4hz <= -2)
                            {
                                txtAWCEntryW4HZ.Text = "MAM(Moderately Acute Malnutrition)";
                                txtAWCEntryW4HZ.TextColor = Color.Black;
                                txtAWCEntryW4HZ.BackgroundColor = Color.Yellow;
                            }
                            else
                            {

                                txtAWCEntryW4HZ.Text = "Normal";
                                txtAWCEntryW4HZ.TextColor = Color.Black;
                                txtAWCEntryW4HZ.BackgroundColor = Color.Green;

                            }
                        }
                    }
                    //  txtAnyLongTermIllnessInFamily.Text = checkChildata[0].AnyLongTermIllnessInFamily;
                    // txtAWCEntryW4HZ.Text = checkChildata[0].AWCEntryW4HZ.ToString();
                    txtChildCode.Text = checkChildata[0].ChildCode.ToString();
                    if (checkChildata[0].Photograph.Length>0)
                    {
                        aBC.imgName = checkChildata[0].Photograph;

                        imgChild.Source = ImageSource.FromStream(() => new MemoryStream(aBC.imgName));
                    }
                    else
                    {
                        imgChild.Source = "AcconIcon.jpg";
                    }
                   
                }
                catch (Exception ex)
                {

                }
            }

        }
        private void TxtBirthLengthHeightInCms_Focused(object sender, FocusEventArgs e)
        {

        }
        private void TxtBirthLengthHeightInCms_Unfocused(object sender, FocusEventArgs e)
        {
            if (Convert.ToDouble(txtBirthLengthHeightInCms.Text) <= 45)
            {
                txtBirthLengthHeightInCms.Text = "45";
            }
            else
            {
                if (Convert.ToDouble(txtBirthLengthHeightInCms.Text) <= 120 && Convert.ToDouble(txtBirthLengthHeightInCms.Text)>45)
                {

                }
                else
                {
                    txtBirthLengthHeightInCms.Text = "120";
                }
            }
        }
        private void TxtAWCEntryWeightInKG_Focused(object sender, FocusEventArgs e)
        {

        }
        private void TxtAWCEntryWeightInKG_Unfocused(object sender, FocusEventArgs e)
        {
           try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if (value >= 2 && value < 26)
                {

                }
                else
                {
                    entry.Text = "";
                }
            }
            catch
            {

            }
        }
        private void TxtAWCEntryHeightInCms_Focused(object sender, FocusEventArgs e)
        {

        }
        private void TxtAWCEntryHeightInCms_Unfocused(object sender, FocusEventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtAWCEntryHeightInCms.Text) <= 45)
                {
                    txtAWCEntryHeightInCms.Text = "45";
                }
                else
                {
                    if (Convert.ToDouble(txtAWCEntryHeightInCms.Text) <= 120 && Convert.ToDouble(txtAWCEntryHeightInCms.Text) > 45)
                    {

                    }
                    else
                    {
                        txtAWCEntryHeightInCms.Text = "120";
                    }
                }
            }
            catch
            {

            }

        }
        private void TxtAWCEntryMUAC_Focused(object sender, FocusEventArgs e)
        {

        }
        private void TxtAWCEntryMUAC_Unfocused(object sender, FocusEventArgs e)
        {

        }
       
        private void TxtAWCEntryW4HZ_Unfocused(object sender, FocusEventArgs e)
        {

        }
        private void TxtChildCode_Focused(object sender, FocusEventArgs e)
        {

        }
        private void TxtChildCode_Unfocused(object sender, FocusEventArgs e)
        {

        }
        private async void BtnCamera_Clicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMedia.Current.Initialize();
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    DisplayAlert("No Camera", ":( No camera available.", "OK");
                    return;
                }

                _mediaFile = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Test",
                    SaveToAlbum = true,
                    CompressionQuality = 50,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.Medium,
                    MaxWidthHeight = 500,
                    DefaultCamera = CameraDevice.Front
                });

                if (_mediaFile == null)
                    return;
                imgChild.Source = ImageSource.FromStream(() => _mediaFile.GetStream());
                
                aBC.imgName = File.ReadAllBytes(_mediaFile.Path);
                //string imreBase64Data = Convert.ToBase64String(aBC.imgName);
                //imgChild.Source =  ImageSource.FromStream(() => new MemoryStream(aBC.imgName));
            }
            catch
            {

            }
        }
        private async void Btngalary_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                _mediaFile = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    CompressionQuality = 50,
                    CustomPhotoSize = 50,
                    PhotoSize = PhotoSize.Medium,
                    MaxWidthHeight = 500
                   

                });
                if (_mediaFile == null)
                    return;
                aBC.imgName = File.ReadAllBytes(_mediaFile.Path);
                imgChild.Source = ImageSource.FromStream(() => _mediaFile.GetStream());

                
                //imgChild.Source = ImageSource.FromStream(() =>
                //{
                //    var stream = _mediaFile.GetStream();
                //    var bytes = new byte[stream.Length];
                //    _mediaFile.Dispose();
                //    aBC.imgName = bytes;
                //    return stream;
                //});
            }
            catch
            {

            }
        }

        private void TxtBWeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if (value >= 0 && value <= 5)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");
                    var match = regex.Match(txtBWeight.Text);
                    if (match.Success)
                    {
                         txtBWeight.Text = match.Value;
                            txtBWeight.TextColor = Color.Black;
                            
                    }
                    else
                    {
                        txtBWeight.TextColor = Color.Red;
                    }
                }
                else
                {
                    txtBWeight.TextColor = Color.Red;
                }
            }
            catch
            {

            }
        }

        private void TxtAWCEntryWeightInKG_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if (value >= 2 && value < 26)
                {

                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");
                    var match = regex.Match(txtAWCEntryWeightInKG.Text);
                    if (match.Success)
                    {
                        try
                        {
                            txtAWCEntryWeightInKG.Text = match.Value;
                            txtAWCEntryWeightInKG.TextColor = Color.Black;
                            var selectedGender = (ColumnValue)ddlGender.SelectedItem;
                            int Gender = selectedGender.columnValueId;
                            CalculationvalueClass calculationvalueClass = new CalculationvalueClass();

                            DateTime dtcurrent = new DateTime();
                            //  dtcurrent = DateTime.Now;
                            if (txtAWDOB2.IsVisible == true)
                            {
                                dtcurrent = txtAWDOB.Date;
                            }
                            else
                            {
                                dtcurrent = txtEntryRegistor.Date;
                            }
                            DateTime dtDob = txtDOB.Date;
                            TimeSpan ts = dtcurrent - dtDob;
                            int Mts = (dtcurrent.Year - dtDob.Year) * 12 + dtcurrent.Month - dtDob.Month;

                            int days = ts.Days;

                            double? W4AZ = calculationvalueClass.W4AZValue(Gender, days, Convert.ToDouble(txtAWCEntryWeightInKG.Text));
                            if (W4AZ != null)
                            { 
                                w4az = Math.Round(W4AZ.Value);
                            if (w4az < -3)
                            {
                                txtAWCEntryW4AZ.Text = "SUW(Severely Under Weight)";
                                txtAWCEntryW4AZ.TextColor = Color.Black;
                                txtAWCEntryW4AZ.BackgroundColor = Color.Red;

                            }
                            else
                            {
                                if (w4az <= -2)
                                {
                                    txtAWCEntryW4AZ.Text = "MUW(Moderately Under Weight)";
                                    txtAWCEntryW4AZ.TextColor = Color.Black;
                                    txtAWCEntryW4AZ.BackgroundColor = Color.Yellow;

                                }
                                else
                                {

                                    txtAWCEntryW4AZ.Text = "Normal";
                                    txtAWCEntryW4AZ.TextColor = Color.Black;
                                        txtAWCEntryW4AZ.BackgroundColor = Color.Green;


                                }
                            }
                            H4AZCalculation();
                        }
                        }
                        catch
                        {

                        }
                    }
                    else
                    {
                        txtAWCEntryWeightInKG.TextColor = Color.Red;
                    }
                }
                else
                {
                    txtAWCEntryWeightInKG.TextColor = Color.Red;
                }
            }
            catch
            {

            }

        }

        private void TxtAWCEntryHeightInCms_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            { 
            Regex regex = new Regex(@"^\d{0,3}(\.\d{1,3})?$");
            var match = regex.Match(txtAWCEntryHeightInCms.Text);
            if (match.Success)
            {
                txtAWCEntryHeightInCms.Text = match.Value;
                txtAWCEntryHeightInCms.TextColor = Color.Black;
                    H4AZCalculation();
            }
            else
            {
                txtAWCEntryHeightInCms.TextColor = Color.Red;
            }
            }
            catch
            {

            }
        }
        private void H4AZCalculation()
        {
            CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
            var selectedGender = (ColumnValue)ddlGender.SelectedItem;
            int Gender = selectedGender.columnValueId;
            if (txtAWCEntryHeightInCms.Text != null)
            {
                try
                {
                    DateTime dtcurrent = txtEntryRegistor.Date;
                    if(txtAWDOB2.IsVisible == true)
                    {
                        dtcurrent = txtAWDOB.Date;
                    }
                    else
                    {
                        dtcurrent = txtEntryRegistor.Date;
                    }
                    // dtcurrent = DateTime.Now;
                    DateTime dtDob = txtDOB.Date;
                    TimeSpan ts = dtcurrent - dtDob;
                    int days = ts.Days;
                    double? W4LHZ = calculationvalueClass.W4LHZValue(Gender, days, Convert.ToDouble(txtAWCEntryHeightInCms.Text), Convert.ToDouble(txtAWCEntryWeightInKG.Text));
                    if (W4LHZ != null)
                    {
                        w4hz = Math.Round(W4LHZ.Value);
                        if (w4hz < -3)
                        {
                            txtAWCEntryW4HZ.Text = "SAM(Severely Acute Malnutrition)";
                            txtAWCEntryW4HZ.TextColor = Color.Black;
                            txtAWCEntryW4HZ.BackgroundColor = Color.Red;
                        }
                        else
                        {
                            if (w4hz <= -2)
                            {
                                txtAWCEntryW4HZ.Text = "MAM(Moderately Acute Malnutrition)";
                                txtAWCEntryW4HZ.TextColor = Color.Black;
                                txtAWCEntryW4HZ.BackgroundColor = Color.Yellow;
                            }
                            else
                            {

                                txtAWCEntryW4HZ.Text = "Normal";
                                txtAWCEntryW4HZ.TextColor = Color.Black;
                                txtAWCEntryW4HZ.BackgroundColor = Color.Green;
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        //private void TxtBirthLengthHeightInCms_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    try
        //    {
        //        Regex regex = new Regex(@"^\d{0,3}(\.\d{1,2})?$");
        //        var match = regex.Match(txtBirthLengthHeightInCms.Text);
        //        if (match.Success)
        //        {
        //            txtBirthLengthHeightInCms.Text = match.Value;
        //            txtBirthLengthHeightInCms.TextColor = Color.Black;
                    
        //        }
        //        else
        //        {
        //            txtBirthLengthHeightInCms.TextColor = Color.Red;
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}

        private void TxtAWCEntryMUAC_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                var entry = (Entry)sender;
                double value = Convert.ToDouble(entry.Text);
                if (value < 11.5)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");
                    var match = regex.Match(txtAWCEntryMUAC.Text);
                    if (match.Success)
                    {
                        txtAWCEntryMUAC.Text = match.Value;
                        txtAWCEntryMUAC.BackgroundColor = Color.Red;
                        txtAWCEntryMUAC.TextColor = Color.Black;
                    }
                    else
                    {

                        txtAWCEntryMUAC.TextColor = Color.DarkRed;

                    }
                }
                if (value > 12.5)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");

                    var match = regex.Match(txtAWCEntryMUAC.Text);
                    if (match.Success)
                    {
                        txtAWCEntryMUAC.Text = match.Value;
                        txtAWCEntryMUAC.BackgroundColor = Color.Green;
                        txtAWCEntryMUAC.TextColor = Color.Black;
                    }
                    else
                    {

                        txtAWCEntryMUAC.TextColor = Color.DarkRed;
                    }
                }
                if (value > 11.4 && value < 12.6)
                {
                    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,3})?$");

                    var match = regex.Match(txtAWCEntryMUAC.Text);
                    if (match.Success)
                    {
                        txtAWCEntryMUAC.Text = match.Value;
                        txtAWCEntryMUAC.BackgroundColor = Color.Yellow;
                        txtAWCEntryMUAC.TextColor = Color.Black;
                    }
                    else
                    {

                        txtAWCEntryMUAC.TextColor = Color.DarkRed;
                    }
                }
            }
            catch
            {

            }
           
            
        }

        //private void TxtAWCEntryW4AZ_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Regex regex = new Regex(@"^\d{0,2}(\.\d{1,2})?$");
        //    var match = regex.Match(txtAWCEntryW4AZ.Text);
        //    if (match.Success)
        //    {
        //        txtAWCEntryW4AZ.Text = match.Value;
        //        txtAWCEntryW4AZ.TextColor = Color.Black;
        //    }
        //    else
        //    {
        //        txtAWCEntryW4AZ.TextColor = Color.Red;
        //    }
            
        //}

        //private void TxtAWCEntryW4HZ_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    Regex regex = new Regex(@"^\d{0,3}(\.\d{1,2})?$");
        //    var match = regex.Match(txtAWCEntryW4HZ.Text);
        //    if (match.Success)
        //    {
        //        txtAWCEntryW4HZ.Text = match.Value;
        //        txtAWCEntryW4HZ.TextColor = Color.Black;
        //    }
        //    else
        //    {
        //        txtAWCEntryW4HZ.TextColor = Color.Red;
        //    }
            
        //}

        private void TxtDOB_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtDOB2.IsVisible = true;
            txtDOB11.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            txtDOB.Focus();
        }

        private void TxtBOrder_TextChanged(object sender, TextChangedEventArgs e)
        {
            //try
            //{
            //    if (Convert.ToInt32(txtBOrder.Text) <= 0)
            //    {
            //        txtBOrder.Text = "";

            //    }
            //}
            //catch
            //{
            //    txtBOrder.Text = "";
            //}
        }

        private void TxtAWDOB_DateSelected(object sender, DateChangedEventArgs e)
        {
            txtAWDOB2.IsVisible = true;
            txtAWDOB11.IsVisible = false;
            CalculationvalueClass calculationvalueClass = new CalculationvalueClass();
            var selectedGender = (ColumnValue)ddlGender.SelectedItem;
            int Gender = selectedGender.columnValueId;
            if (txtAWCEntryHeightInCms.Text != null)
            {
                try
                {
                    DateTime dtcurrent = txtAWDOB.Date;
                    // dtcurrent = DateTime.Now;
                    DateTime dtDob = txtDOB.Date;
                    TimeSpan ts = dtcurrent - dtDob;
                    int days = ts.Days;
                    double? W4LHZ = calculationvalueClass.W4LHZValue(Gender, days, Convert.ToDouble(txtAWCEntryHeightInCms.Text), Convert.ToDouble(txtAWCEntryWeightInKG.Text));
                    if (W4LHZ != null)
                    {
                        w4hz = Math.Round(W4LHZ.Value);
                        if (w4hz < -3)
                        {
                            txtAWCEntryW4HZ.Text = "SAM(Severely Acute Malnutrition)";
                            txtAWCEntryW4HZ.TextColor = Color.Black;
                            txtAWCEntryW4HZ.BackgroundColor = Color.Red;
                        }
                        else
                        {
                            if (w4hz <= -2)
                            {
                                txtAWCEntryW4HZ.Text = "MAM(Moderately Acute Malnutrition)";
                                txtAWCEntryW4HZ.TextColor = Color.Black;
                                txtAWCEntryW4HZ.BackgroundColor = Color.Yellow;
                            }
                            else
                            {

                                txtAWCEntryW4HZ.Text = "Normal";
                                txtAWCEntryW4HZ.TextColor = Color.Black;
                                txtAWCEntryW4HZ.BackgroundColor = Color.Green;
                            }
                        }
                    }
                }
                catch
                {

                }
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            txtAWDOB.Focus();
        }

        private void TxtEntryRegistor_DateSelected(object sender, DateChangedEventArgs e)
        {
            try
            {
                H4AZCalculation();
            }
            catch
            {

            }
        }
        //public double W4AZValue(int Gender, int AgeInDays, double WeightInKG)
        //{
        //    var d = App.DAUtil.GetLMSWAZ(Gender, AgeInDays);
        //    if (d.Count > 0)
        //    {
        //        double L = d[0].L, M = d[0].M, S = d[0].S;

        //        //            l_RetVal = (POWER((p_WeightInKG / l_MVal), l_LVal) - 1) / (l_SVal * l_LVal);
        //        W4AZ = Math.Round((Math.Pow((WeightInKG / M), L) - 1) / (S * L), 2);


        //        if (this.W4AZ < -3)
        //        {
        //            double l_sd3neg = M * Math.Pow((1 + L * S * (-3)), (1 / L));// l_MVal * POWER((1 + l_LVal * l_SVal * (-3)), (1 / l_LVal));
        //            double l_sd23neg = (M * Math.Pow((1 + L * S * (-2)), (1 / L))) - l_sd3neg;
        //            W4AZ = (-3) - ((l_sd3neg - WeightInKG) / l_sd23neg);
        //        }

        //        else if (this.W4AZ > 3)
        //        {
        //            double l_sd3pos = M * Math.Pow((1 + L * S * 3), (1 / L));
        //            double l_sd23pos = l_sd3pos - (M * Math.Pow((1 + L * S * 2), (1 / L)));
        //            W4AZ = 3 + ((WeightInKG - l_sd3pos) / l_sd23pos);
        //        }
        //    }
        //    return W4AZ;
        //}
    }
}
public class aBC
{
    public static byte[] imgName { get; set; }
}