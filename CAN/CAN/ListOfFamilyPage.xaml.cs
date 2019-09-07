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
    public partial class ListOfFamilyPage : ContentPage
    {
        int previousValue = 0;
        
        long id;
        public ListOfFamilyPage()
        {
            InitializeComponent();
            //btnPrivious.IsVisible = false;
            //btnPriviousnext.IsVisible = false;
            listView.IsVisible = false;
            this.Title = StaticClass.LocationName;
            BindList();

        }

        private void BindList()
        {
            id = StaticClass.VillageID;
            var ListData = App.DAUtil.GetAllFamilyByLocation(id).Take(5).OrderByDescending(x=>x.FamilyCode).ToList();
           
            if (ListData.Count > 0)
            {
                btnPriviousnext.IsVisible = true;
                listView.IsVisible = true;
                listView.ItemsSource = ListData;
                btnPrivious.IsEnabled = false;
            }

        }


        private async  void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Image)sender;

           var d= item.Source.BindingContext;
            FamilyRegister family = (FamilyRegister)d;
            Guid FamilyId = family.FamilyId;
            //StaticClass.PageData = FamilyId;
            StaticClass.FamilyId = FamilyId;
            StaticClass.PageButtonText = "Update";
            await Navigation.PushAsync(new FamilyPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Image)sender;

            var d = item.Source.BindingContext;
            FamilyRegister family = (FamilyRegister)d;
            Guid FamilyId = family.FamilyId;
            StaticClass.FamilyAliveChildCount = family.NumberofChildenAlive;
            await Navigation.PushAsync(new ListOfChildPage());
        }

        private async void ImageButton_ItemTapped(object sender, EventArgs e)
        {
            StaticClass.PageButtonText = "Save";
            StaticClass.FamilyEdit = "False";
            await Navigation.PushAsync(new FamilyPage());
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Label)sender;
            var d = item.BindingContext;
            FamilyRegister family = (FamilyRegister)d;
           Guid FamilyId = family.FamilyId;
            StaticClass.FamilyAliveChildCount = family.NumberofChildenAlive;
            await Navigation.PushAsync(new ListOfChildPage());
        }

        private async void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            var item = (Xamarin.Forms.Label)sender;

            var d = item.BindingContext;
            FamilyRegister family = (FamilyRegister)d;
            Guid FamilyId = family.FamilyId;
            //StaticClass.PageData = FamilyId;
            StaticClass.FamilyId = FamilyId;
            StaticClass.PageButtonText = "Update";
            await Navigation.PushAsync(new FamilyPage());
        }

        private void BtnPrivious_Clicked(object sender, EventArgs e)
        {
            if (previousValue > 0)
            {
                previousValue -= 5;
                var ListData = App.DAUtil.GetAllFamilyByLocation(id).Skip(previousValue).Take(5).OrderByDescending(x => x.FamilyCode).ToList();
                if (ListData.Count == 0)
                {
                    btnPrivious.IsEnabled = false;
                    btnPriviousnext.IsEnabled = true;
                }
                else
                {
                    listView.ItemsSource = null;
                    listView.ItemsSource = ListData;
                    btnPrivious.IsEnabled = true;
                    btnPriviousnext.IsEnabled = true;
                }
            }
            else
            {
                btnPrivious.IsEnabled = false;
            }
        }

        private void BtnPriviousnext_Clicked(object sender, EventArgs e)
        {
            if (previousValue >= 0)
            {
                btnPrivious.IsEnabled = true;
                previousValue += 5;
                var ListData = App.DAUtil.GetAllFamilyByLocation(id).Skip(previousValue).Take(5).OrderByDescending(x => x.FamilyCode).ToList();
                if (ListData.Count == 0)
                {
                    btnPriviousnext.IsEnabled = false;

                }
                else
                {
                    btnPriviousnext.IsEnabled = true;

                    listView.ItemsSource = null;
                    listView.ItemsSource = ListData;
                }
            }
        }
    }
}