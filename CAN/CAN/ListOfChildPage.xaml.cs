﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CAN.Models;
using CAN.ViewModels;
using System.IO;

namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListOfChildPage : ContentPage
    {
        public ListOfChildPage()
        {
            InitializeComponent();
       
            this.Title = StaticClass.LocationName;
            BindChildList();
        }

        private void BindChildList()
        {
            try
            {
                long id = StaticClass.VillageID;
                //var FamilyData = App.DAUtil.GetAllFamilyByLocation(id);
                var FamilyData = App.DAUtil.FindFamilyId(StaticClass.PageData);
                
                // StaticClass.PageData = FamilyId;
                if (FamilyData.Count > 0)
                {
                    var ChildData = App.DAUtil.FindChildId(StaticClass.PageData.ToString());
                    if(ChildData.Count < FamilyData[0].NumberofChildenAlive)
                    {
                        btnAdd.IsVisible = true;
                    }
                    else
                    {
                        btnAdd.IsVisible = false;
                    }
                    if (ChildData != null)
                    {
                        List<ChildViewModel> listchildViewModels = new List<ChildViewModel>();
                        foreach (ChildRegister child in ChildData)
                        {
                            ChildViewModel childViewModel = new ChildViewModel();
                            childViewModel.ChildId = child.ChildId;
                            childViewModel.ChildName = child.ChildName;
                            childViewModel.BirthWeightInKg = child.BirthWeightInKg.ToString();
                            childViewModel.FamilyId = child.FamilyId;
                            if(child.AWCEntryW4AZ<-3)
                            {
                                childViewModel.W4AZ = "SUW(Severely Under Weight)";
                            }
                         else
                            {
                                if(child.AWCEntryW4AZ<=-2)
                                {
                                    childViewModel.W4AZ = "MUW(Moderately Under Weight)";
                                }
                                else
                                {
                                    //if(child.AWCEntryW4AZ!=0)
                                    //{
                                        childViewModel.W4AZ = "Normal";
                                    //}
                                    //else
                                    //{
                                    //    childViewModel.W4AZ = "Not Calculated";
                                    //}
                                }
                         }

                            if (child.AWCEntryW4HZ < -3)
                            {
                                childViewModel.W4HZ = "SAM(Severely Acute Malnutrition)";
                            }
                            else
                            {
                                if (child.AWCEntryW4HZ <= -2)
                                {
                                    childViewModel.W4HZ = "MAM(Moderately Acute Malnutrition)";
                                }
                                else
                                {
                                //    if (child.AWCEntryW4HZ != 0)
                                //    {
                                        childViewModel.W4HZ = "Normal";
                                    //}
                                    //else
                                    //{
                                    //    childViewModel.W4HZ = "Not Calculated";
                                    //}
                                }
                            }
                            //childViewModel.W4AZ = child.AWCEntryW4AZ.ToString();
                           // childViewModel.W4HZ = child.AWCEntryW4HZ.ToString();
                            //var imageData = Convert.FromBase64String(child.Photograph);
                            if (child.Photograph != null)
                            {
                                if (child.Photograph.Length > 0)
                                {
                                    childViewModel.PhotoData = ImageSource.FromStream(() => new MemoryStream(child.Photograph));
                                }
                                else
                                {
                                    childViewModel.PhotoData = "AcconIcon.jpg";
                                }
                            }
                            else
                            {
                              
                                    childViewModel.PhotoData = "AcconIcon.jpg";
                              
                            }
                            var ListofGender = App.DAUtil.GetColumnValuesBytext(3);
                            for (int i = 0; i < ListofGender.Count; i++)
                            {
                                if (ListofGender[i].columnValueId == child.GenderID)
                                {
                                    childViewModel.Gender = ListofGender[i].columnValue;
                                }
                            }

                            listchildViewModels.Add(childViewModel);
                        }

                        listView.IsVisible = true;
                        listView.ItemsSource = listchildViewModels;
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {


            var item = (Xamarin.Forms.Image)sender;

            var d = item.Source.BindingContext;

            ChildViewModel child = (ChildViewModel)d;
            Guid ChildId = child.ChildId;
            StaticClass.ChildID = ChildId;
            StaticClass.PageButtonText = "Update";
            await Navigation.PushAsync(new ChildPage());
        }

        private async void ImageButton_ItemTapped(object sender, EventArgs e)
        {
            StaticClass.PageButtonText = "Save";
            await Navigation.PushAsync(new ChildPage());
        }
        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            StaticClass.PageName = "HomePage";
            Application.Current.MainPage = new MasterDetailPage1();
        }
    }
}
//public class Chil
//{
//    public Guid ChildId { get; set; }
//    public string ChildName { get; set; }

//    public string AadhaaId { get; set; }

//    public string SchoolName { get; set; }
//    public decimal BirthWeight { get; set; }
//    public int BloodGroup { get; set; }
//    public string AnyExistingDisease { get; set; }
//    public DateTime RegisterDate { get; set; }
//    public DateTime DOB { get; set; }
//}