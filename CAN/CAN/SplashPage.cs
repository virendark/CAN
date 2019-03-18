
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CAN
{
    public class SplashPage : ContentPage
    {
        Image splashImage;
        public SplashPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                Source = "ic_launcher.jpg",
                WidthRequest = 100,
                HeightRequest = 100
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#696969");
            this.Content = sub;
        }
        protected async override void OnAppearing()
        {
            await splashImage.ScaleTo(0.5, 1200, Easing.Linear);
            await splashImage.ScaleTo(0.7, 1500, Easing.Linear);
            await splashImage.ScaleTo(0.9, 1200, Easing.Linear);
            await splashImage.ScaleTo(1, 2000);
            var checkLogin = 0;//App.DAUtil.GetAllUserDetails();
                               //if (checkLogin.Count > 0)
                               //{

            checkOpenPage();




        }
        private void checkOpenPage()
        {
           
            var CheckUserToken = App.DAUtil.GetUsers();
            if(CheckUserToken.Count>0)
            {
                Application.Current.MainPage = new NavigationPage(new PinPage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00"),
                    BarTextColor = Color.White
                };
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00"),
                    BarTextColor = Color.White


                };

            }
        }
    }
}
