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
        public ListOfFamilyPage()
        {
            InitializeComponent();
            listView.IsVisible = false;
            this.Title = StaticClass.LocationName;
            BindList();

        }

        private void BindList()
        {
           
            long id = StaticClass.VillageID;
            var ListData = App.DAUtil.GetAllFamilyByLocation(id);
           
            if (ListData.Count > 0)
            {

                listView.IsVisible = true;
                listView.ItemsSource = ListData;
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
            StaticClass.PageData = FamilyId;
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
            StaticClass.PageData = FamilyId;
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
    }
}