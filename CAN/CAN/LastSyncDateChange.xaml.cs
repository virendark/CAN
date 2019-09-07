using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CAN.ViewModels;
using CAN.Models;
namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LastSyncDateChange : ContentPage
    {
        public LastSyncDateChange()
        {
            InitializeComponent();
            this.BackgroundColor = Color.FromHex("#696969");
            txtsyncdata.MaximumDate = DateTime.Now;
            var checkdate = App.DAUtil.Getpushdatatime();
            StaticClass.LastsyncDateChange = "";
        }

        private void Btnsave_Clicked(object sender, EventArgs e)
        {
            App.DAUtil.deletepushdatatime();
            TblPushDataTime tblPushDataTime = new TblPushDataTime();
            tblPushDataTime.Flage = true;
            tblPushDataTime.CheckDate = txtsyncdata.Date;// DateTime.Now;
            App.DAUtil.SavepushDataTime(tblPushDataTime);
            StaticClass.LastsyncDateChange = "";
            Application.Current.MainPage = new VillagePage();

        }
    }
}