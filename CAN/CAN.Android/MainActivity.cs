using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.CurrentActivity;
using Plugin.Media;

namespace CAN.Droid
{
    [Activity(Label = "CAN", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CrossCurrentActivity.Current.Init(this, savedInstanceState);
            CrossMedia.Current.Initialize();
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public void exitApp()
        {
            (Xamarin.Forms.Forms.Context as Activity).Finish();
        }
        //public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Android.Content.PM.Permission[] grantResults)
        //{
        //    PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        //}
    }
}