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
	public partial class HighRiskMotherHistoryPage : PopupPage
    {
        List<Ass> listass = new List<Ass>();
        public HighRiskMotherHistoryPage ()
		{
			InitializeComponent ();
            BindAssets();

        }
        private void BindAssets()
        {

            if (StaticClass.PageButtonText == "Update")
            {
                var checkFamilydata = App.DAUtil.FindGrowthRegisterMotherSingleData(StaticClass.GrouthMotherID.ToString());
                if (checkFamilydata.Count > 0)
                {
                    string Assets = checkFamilydata[0].HighRiskMotherHistory;
                    if (Assets != null)
                    {
                        var numbers = Assets.Split(',');
                        List<string> Lass = new List<string>();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            string data = numbers[i];
                            Lass.Add(data);
                        }
                        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
                        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfHighRiskMother[i].columnValueId;
                            ass.Name = ListOfHighRiskMother[i].columnValue;
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
                        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
                        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfHighRiskMother[i].columnValueId;
                            ass.Name = ListOfHighRiskMother[i].columnValue;
                            ass.Flag = "false";
                            listass.Add(ass);
                        }
                    }
                }
            }
            else
            {
                var checkFamilydata = App.DAUtil.FindGrowthRegisterMother(StaticClass.GrouthFamilyId.ToString()).LastOrDefault();
                if (checkFamilydata!=null)
                {
                    string Assets = checkFamilydata.HighRiskMotherHistory;
                    if (Assets != null)
                    {
                        var numbers = Assets.Split(',');
                        List<string> Lass = new List<string>();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            string data = numbers[i];
                            Lass.Add(data);
                        }
                        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
                        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfHighRiskMother[i].columnValueId;
                            ass.Name = ListOfHighRiskMother[i].columnValue;
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
                        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
                        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfHighRiskMother[i].columnValueId;
                            ass.Name = ListOfHighRiskMother[i].columnValue;
                            ass.Flag = "false";
                            listass.Add(ass);
                        }
                    }
                }
                else
                {
                    if (StaticClass.HighRiskMother == null)
                    {
                        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
                        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfHighRiskMother[i].columnValueId;
                            ass.Name = ListOfHighRiskMother[i].columnValue;
                            ass.Flag = "false";
                            listass.Add(ass);
                        }
                    }
                    else
                    {
                        var numbers = StaticClass.HighRiskMother.Split(',');
                        List<string> Lass = new List<string>();
                        for (int i = 0; i < numbers.Length; i++)
                        {
                            string data = numbers[i];
                            Lass.Add(data);
                        }
                        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
                        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
                        {
                            Ass ass = new Ass();
                            ass.Id = ListOfHighRiskMother[i].columnValueId;
                            ass.Name = ListOfHighRiskMother[i].columnValue;
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
            #region add
            //else
            //{
            //    if (StaticClass.HighRiskMother == null)
            //    {
            //        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
            //        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
            //        {
            //            Ass ass = new Ass();
            //            ass.Id = ListOfHighRiskMother[i].columnValueId;
            //            ass.Name = ListOfHighRiskMother[i].columnValue;
            //            ass.Flag = "false";
            //            listass.Add(ass);
            //        }
            //    }
            //    else
            //    {
            //        var numbers = StaticClass.HighRiskMother.Split(',');
            //        List<string> Lass = new List<string>();
            //        for (int i = 0; i < numbers.Length; i++)
            //        {
            //            string data = numbers[i];
            //            Lass.Add(data);
            //        }
            //        var ListOfHighRiskMother = App.DAUtil.GetColumnValuesBytext(61);
            //        for (int i = 0; i < ListOfHighRiskMother.Count; i++)
            //        {
            //            Ass ass = new Ass();
            //            ass.Id = ListOfHighRiskMother[i].columnValueId;
            //            ass.Name = ListOfHighRiskMother[i].columnValue;
            //            var check = Lass.FirstOrDefault(x => x.Contains(ass.Id.ToString()));
            //            if (check != null)
            //            {
            //                ass.Flag = "true";
            //            }
            //            else
            //            {
            //                ass.Flag = "false";
            //            }
            //            listass.Add(ass);
            //        }
            //    }
            //}
            #endregion
            listView.ItemsSource = listass;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ass villageN = (Ass)listView.SelectedItem;
        }
        
        private  async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (StaticClass.PageButtonText != "Update")
                StaticClass.HighRiskMother = null;

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
            StaticClass.HighRiskMother = builder.ToString();
            await Navigation.PopPopupAsync();
        }
    }
}