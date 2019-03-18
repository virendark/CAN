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
	public partial class ANMMarkHighRiskScreeningPage : PopupPage
    {
        List<Ass> listass = new List<Ass>();
        public ANMMarkHighRiskScreeningPage ()
		{
			InitializeComponent ();
            BindAssets();

        }
        private void BindAssets()
        {

            var MotherRecord = App.DAUtil.FindGrowthRegisterMother(StaticClass.GrouthFamilyId.ToString());
           
            if (MotherRecord != null && MotherRecord.Count!=0)
            {
                 var MotherData = MotherRecord.LastOrDefault();
                var checkFamilydata = App.DAUtil.FindGrowthRegisterMotherSingleData(MotherData.GrowthId.ToString());
                if (checkFamilydata.Count > 0)
                {
                    string Assets = checkFamilydata[0].ANMMarkHighRiskScreening;
                    if (Assets != null)
                    {
                        var numbers = Assets.Split(',');
                        List<string> Lass = new List<string>();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            string data = numbers[i];
                            Lass.Add(data);
                        }
                        var ListOfANMMarkHighRiskScreening = App.DAUtil.GetColumnValuesBytext(63);
                        for (int i = 0; i < ListOfANMMarkHighRiskScreening.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfANMMarkHighRiskScreening[i].columnValueId;
                            ass.Name = ListOfANMMarkHighRiskScreening[i].columnValue;
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
                        var ListOfANMMarkHighRiskScreening = App.DAUtil.GetColumnValuesBytext(63);
                        for (int i = 0; i < ListOfANMMarkHighRiskScreening.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfANMMarkHighRiskScreening[i].columnValueId;
                            ass.Name = ListOfANMMarkHighRiskScreening[i].columnValue;
                            ass.Flag = "false";
                            listass.Add(ass);
                        }
                    }
                }
            }
            else
            {
                if (StaticClass.PageButtonText == "Update")
                {
                    var checkFamilydata = App.DAUtil.FindGrowthRegisterMotherSingleData(StaticClass.GrouthMotherID.ToString());
                    if (checkFamilydata.Count > 0)
                    {
                        string Assets = checkFamilydata[0].ANMMarkHighRiskScreening;
                        if (Assets != null)
                        {
                            var numbers = Assets.Split(',');
                            List<string> Lass = new List<string>();
                            for (int i = 0; i < numbers.Length; i++)
                            {
                                string data = numbers[i];
                                Lass.Add(data);
                            }
                            var ListOfANMMarkHighRiskScreening = App.DAUtil.GetColumnValuesBytext(63);
                            for (int i = 0; i < ListOfANMMarkHighRiskScreening.Count; i++)
                            {
                                Ass ass = new Ass();
                                ass.Id = ListOfANMMarkHighRiskScreening[i].columnValueId;
                                ass.Name = ListOfANMMarkHighRiskScreening[i].columnValue;
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
                            var ListOfANMMarkHighRiskScreening = App.DAUtil.GetColumnValuesBytext(63);
                            for (int i = 0; i < ListOfANMMarkHighRiskScreening.Count; i++)
                            {
                                Ass ass = new Ass();
                                ass.Id = ListOfANMMarkHighRiskScreening[i].columnValueId;
                                ass.Name = ListOfANMMarkHighRiskScreening[i].columnValue;
                                ass.Flag = "false";
                                listass.Add(ass);
                            }
                        }
                    }
                }
                else
                {
                    if (StaticClass.ANMMarkHighRiskScreening == null)
                    {
                        var ListOfANMMarkHighRiskScreening = App.DAUtil.GetColumnValuesBytext(63);
                        for (int i = 0; i < ListOfANMMarkHighRiskScreening.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfANMMarkHighRiskScreening[i].columnValueId;
                            ass.Name = ListOfANMMarkHighRiskScreening[i].columnValue;
                            ass.Flag = "false";
                            listass.Add(ass);
                        }
                    }
                    else
                    {
                        var numbers = StaticClass.ANMMarkHighRiskScreening.Split(',');
                        List<string> Lass = new List<string>();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            string data = numbers[i];
                            Lass.Add(data);
                        }
                        var ListOfANMMarkHighRiskScreening = App.DAUtil.GetColumnValuesBytext(63);
                        for (int i = 0; i < ListOfANMMarkHighRiskScreening.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfANMMarkHighRiskScreening[i].columnValueId;
                            ass.Name = ListOfANMMarkHighRiskScreening[i].columnValue;
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
                }
            }
               
            
            listView.ItemsSource = listass;
        }
       
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ass villageN = (Ass)listView.SelectedItem;
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if(StaticClass.PageButtonText != "Update")
            StaticClass.ANMMarkHighRiskScreening = null;

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
            StaticClass.ANMMarkHighRiskScreening = builder.ToString();
            await Navigation.PopPopupAsync();
        }
    }
}