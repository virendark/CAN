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

[assembly: Xamarin.Forms.Dependency(typeof(AndroidMethods))]
namespace CAN.Droid
{
  public class   AndroidMethods: ExitAppInterface
    {
        public void exitApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }

    }
}