using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CAN.Models;
using Plugin.RestClient;
using Plugin.Connectivity;
namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PinPage : ContentPage
    {
        public PinPage()
        {
            InitializeComponent();
            lblErrorMessage.IsVisible = false;
        }

        private async void BtnSignIn_Clicked(object sender, EventArgs e)
        {
            try
            {
               StaticClass.MonthlyMonitoringName = "";

                var ChickPin = App.DAUtil.GetUserPin();
                if (ChickPin.Count>0)
                {
                    if (Convert.ToInt32(txtPin.Text) == ChickPin[0].SetPin)
                    {
                        Application.Current.MainPage = new VillagePage();
                    }
                    else
                    {
                        DisplayAlert("", "Wrong Pin Please check", "OK");
                    }
                }
                else
                {
                    if (txtPin.Text != null && txtPin.Text != "")
                    {
                        UserPinTable userPinTable = new UserPinTable();

                        userPinTable.UserId = 1;
                        userPinTable.SetPin = Convert.ToInt32(txtPin.Text);
                        App.DAUtil.SetPin(userPinTable);
                        Application.Current.MainPage = new VillagePage();
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Please Enter Pin");
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
