using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CAN.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]
namespace CAN.Droid
{
   public  class Toast_Android: Toast
    {
       
        public void Show(string message)
        {
           

            Android.Widget.Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
    }
}