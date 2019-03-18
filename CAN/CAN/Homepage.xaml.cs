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
    public partial class Homepage : ContentPage
    {
        public Homepage()
        {
            InitializeComponent();
            BindList();

        }

        private void btnAddChild_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChildPage());
        }

        private void btnAddFamily_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new FamilyPage());
        }
        private void BindList()
        {
            var FamilyData = App.DAUtil.GetAllFamily();
            var ChildData = App.DAUtil.GetAllChild();
            if (ChildData != null)
            {
                var ListData = (from a in App.DAUtil.GetAllChild()
                                from b in App.DAUtil.GetAllFamily()
                                where a.FamilyId == b.FamilyId
                                select new
                                {
                                    ChildName = a.ChildName,
                                    AadhaaId = "",//a.AadhaaId,
                                    DOB = a.DOB,
                                    SchoolName = "",//a.SchoolName,
                                    FamilyType = b.FamilyType,
                                    FatherName = b.FatherName,
                                    MotherName = b.MotherName,
                                    BirthWeight ="",// a.BirthWeight,
                                    BloodGroup ="",// a.BloodGroup,
                                    RegisterDate = a.DOE
                                    //Gender = a.Gender
                                });
                listView.ItemsSource = ListData;
            }

        }
    }
}