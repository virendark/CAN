using CAN.Models;
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
    public partial class MasterDetailPage1 : MasterDetailPage
    {
        public MasterDetailPage1()
        {
            InitializeComponent();
            //MasterPage.ListView.ItemSelected += ListView_ItemSelected;
            pageNavigation();
        }

        public void pageNavigation()
        {
            if (StaticClass.PageName == "MonthlyMonitoring")
            {
                Detail = new NavigationPage(new ListOfMonthlyMonitoring())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
            if (StaticClass.PageName == "FamilyPage")
            {
                Detail = new NavigationPage(new ListOfFamilyPage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
            if (StaticClass.PageName == "ChildPage")
            {
                Detail = new NavigationPage(new ListOfChildPage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
            if (StaticClass.PageName == "MonthlyData")
            {
                Detail = new NavigationPage(new ListOfMonthlydataPage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
            if (StaticClass.PageName == "RedFlag")
            {
                Detail = new NavigationPage(new ListOfRedFlagPage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
            if (StaticClass.PageName == "HomePage")
            {
                Detail = new NavigationPage(new HeaderTabbed())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
            //if (StaticClass.PageName == "Home")
            //{
            //    Detail = new NavigationPage(new HeaderTabbed1())
            //    {
            //        BarBackgroundColor = Color.FromHex("#ffbf00")

            //    };
            //}
            if (StaticClass.PageName == "VillagePage")
            {
                Detail = new NavigationPage(new VillagePage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
        }
        //private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        //{
        //    StaticClass.PageName = "";
        //    var item = e.SelectedItem as MasterDetailPage1MenuItem;
        //    if (item == null)
        //        return;
        //    if (item.Id == 0)
        //    {
        //        //Application.Current.MainPage = new VillagePage();
        //        Detail = new NavigationPage(new ListOfVillage())
        //        {
        //            BarBackgroundColor = Color.FromHex("#ffbf00")

        //        };
        //        IsPresented = false;

        //        MasterPage.ListView.SelectedItem = null;
        //    }
        //    //if (item.Id == 1)
        //    //{


        //    //    var page = (Page)Activator.CreateInstance(item.TargetType);
        //    //    page.Title = item.Title;

        //    //    Detail = new NavigationPage(new ListOfFamilyPage())
        //    //    {
        //    //        BarBackgroundColor = Color.FromHex("#ffbf00")

        //    //    };
        //    //    IsPresented = false;

        //    //    MasterPage.ListView.SelectedItem = null;
        //    //}
        //    //if (item.Id == 2)
        //    //{


        //    //    var page = (Page)Activator.CreateInstance(item.TargetType);
        //    //    page.Title = item.Title;

        //    //    Detail = new NavigationPage(new ListOfChildPage())
        //    //    {
        //    //        BarBackgroundColor = Color.FromHex("#ffbf00")

        //    //    };
        //    //    IsPresented = false;

        //    //    MasterPage.ListView.SelectedItem = null;
        //    //}
        //    //if (item.Id == 3)
        //    //{


        //    //    var page = (Page)Activator.CreateInstance(item.TargetType);
        //    //    page.Title = item.Title;

        //    //    Detail = new NavigationPage(new ListOfMonthlydataPage())
        //    //    {
        //    //        BarBackgroundColor = Color.FromHex("#ffbf00")

        //    //    };
        //    //    IsPresented = false;

        //    //    MasterPage.ListView.SelectedItem = null;
        //    //}
        //    //if (item.Id == 4)
        //    //{


        //    //    var page = (Page)Activator.CreateInstance(item.TargetType);
        //    //    page.Title = item.Title;

        //    //    Detail = new NavigationPage(new ListOfRedFlagPage())
        //    //    {
        //    //        BarBackgroundColor = Color.FromHex("#ffbf00")

        //    //    };
        //    //    IsPresented = false;

        //    //    MasterPage.ListView.SelectedItem = null;
        //    //}
        //}
    }
}