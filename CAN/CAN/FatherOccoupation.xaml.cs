using CAN.Models;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
	public partial class FatherOccoupation : PopupPage
    {
        List<Ass> listass = new List<Ass>();
        public FatherOccoupation ()
		{
			InitializeComponent ();
            BindAssets();

        }

        private void BindAssets()
        {

            if (StaticClass.PageButtonText == "Update")
            {
                // var checkFamilydata = App.DAUtil.FindGrowthRegisterMotherSingleData(StaticClass.GrouthMotherID.ToString());
                var checkFamilydata = App.DAUtil.FindFamilyId(StaticClass.FamilyId);
                if (checkFamilydata.Count > 0)
                {
                    string Assets = checkFamilydata[0].FatherOccupation;
                    if (Assets != null)
                    {
                        var numbers = Assets.Split(',');
                        List<string> Lass = new List<string>();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            string data = numbers[i];
                            Lass.Add(data);
                        }
                        var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(64);
                        for (int i = 0; i < ListOfMotherEducation.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfMotherEducation[i].columnValueId;
                            ass.Name = ListOfMotherEducation[i].columnValue;
                            var check = Lass.FirstOrDefault(x => x.Contains(ass.Id.ToString()));
                            if (check != null)
                            {
                                ass.Flag = "true";
                            }
                            else
                            {
                                ass.Flag = "false";
                            }
                            listass.Add(ass);
                        }
                    }
                    else
                    {

                        var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(64);
                        for (int i = 0; i < ListOfMotherEducation.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfMotherEducation[i].columnValueId;
                            ass.Name = ListOfMotherEducation[i].columnValue;
                            ass.Flag = "false";
                            listass.Add(ass);
                        }
                    }
                }
            }
            else
            {
                //    var checkFamilydata = App.DAUtil.FindGrowthRegisterMother(StaticClass.GrouthFamilyId.ToString()).LastOrDefault();
                //    if (checkFamilydata != null)
                //    {
                //        string Assets = checkFamilydata.ANCCheckups;
                //        if (Assets != null)
                //        {
                //            var numbers = Assets.Split(',');
                //            List<string> Lass = new List<string>();
                //            for (int i = 0; i < numbers.Length; i++)
                //            {
                //                string data = numbers[i];
                //                Lass.Add(data);
                //            }
                //            var ListOfANCCheckup = App.DAUtil.GetColumnValuesBytext(62);
                //            for (int i = 0; i < ListOfANCCheckup.Count; i++)
                //            {
                //                Ass ass = new Ass();
                //                ass.Id = ListOfANCCheckup[i].columnValueId;
                //                ass.Name = ListOfANCCheckup[i].columnValue;
                //                var check = Lass.FirstOrDefault(x => x.Contains(ass.Id.ToString()));
                //                if (check != null)
                //                {
                //                    ass.Flag = "true";
                //                }
                //                else
                //                {
                //                    ass.Flag = "false";
                //                }
                //                listass.Add(ass);
                //            }
                //        }
                //        else
                //        {

                //            var ListOfListOfANCCheckup = App.DAUtil.GetColumnValuesBytext(62);
                //            for (int i = 0; i < ListOfListOfANCCheckup.Count; i++)
                //            {
                //                Ass ass = new Ass();
                //                ass.Id = ListOfListOfANCCheckup[i].columnValueId;
                //                ass.Name = ListOfListOfANCCheckup[i].columnValue;
                //                ass.Flag = "false";
                //                listass.Add(ass);
                //            }
                //        }
                //    }
                //    else
                //    {
                if (StaticClass.FatherOccupation == null)
                {
                    var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(64);
                    for (int i = 0; i < ListOfMotherEducation.Count; i++)
                    {
                        Ass ass = new Ass();
                        ass.Id = ListOfMotherEducation[i].columnValueId;
                        ass.Name = ListOfMotherEducation[i].columnValue;
                        ass.Flag = "false";
                        listass.Add(ass);
                    }
                }
                else
                {
                    var numbers = StaticClass.FatherOccupation.Split(',');
                    List<string> Lass = new List<string>();
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        string data = numbers[i];
                        Lass.Add(data);
                    }
                    var ListOfMotherEducation = App.DAUtil.GetColumnValuesBytext(64);
                    for (int i = 0; i < ListOfMotherEducation.Count; i++)
                    {
                        Ass ass = new Ass();
                        ass.Id = ListOfMotherEducation[i].columnValueId;
                        ass.Name = ListOfMotherEducation[i].columnValue;
                        var check = Lass.FirstOrDefault(x => x.Contains(ass.Id.ToString()));
                        if (check != null)
                        {
                            ass.Flag = "true";
                        }
                        else
                        {
                            ass.Flag = "false";
                        }
                        listass.Add(ass);
                    }
                }
                //}
            }

            listView.ItemsSource = listass;
        }


        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ass villageN = (Ass)listView.SelectedItem;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (StaticClass.PageButtonText != "Update")
                StaticClass.FatherOccupation = null;

            await Navigation.PopPopupAsync();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {

            bool f = true;
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < listass.Count; i++)
            {
                if (listass[i].Flag == "True")
                {
                    if (f == true)
                    {
                        f = false;
                        builder.Append("'" + listass[i].Id + "'");
                    }
                    else
                    {
                        builder.Append(",'" + listass[i].Id + "'");
                    }
                }
            }
            StaticClass.FatherOccupation = builder.ToString();
            await Navigation.PopPopupAsync();
        }
    }
}