using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using CAN.Models;

namespace CAN
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AssetsPage : PopupPage
    {
        List<Ass> listass = new List<Ass>();
        public AssetsPage ()
		{
			InitializeComponent ();
            BindAssets();

        }
        private void BindAssets()
        {
           
            if (StaticClass.FamilyEdit=="True")
            {
                var checkFamilydata = App.DAUtil.FindFamilyId(StaticClass.FamilyId);
                string Assets = checkFamilydata[0].Assets;
                if (Assets != null)
                {
                    var numbers = Assets.Split(',');
                    List<string> Lass = new List<string>();
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        string data = numbers[i];
                        Lass.Add(data);
                    }
                    var ListOfAssets = App.DAUtil.GetColumnValuesBytext(59);
                    for (int i = 0; i < ListOfAssets.Count; i++)
                    {
                        Ass ass = new Ass();
                        ass.Id = ListOfAssets[i].columnValueId;
                        ass.Name = ListOfAssets[i].columnValue;
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
                    var ListOfAssets = App.DAUtil.GetColumnValuesBytext(59);
                    for (int i = 0; i < ListOfAssets.Count; i++)
                    {
                        Ass ass = new Ass();
                        ass.Id = ListOfAssets[i].columnValueId;
                        ass.Name = ListOfAssets[i].columnValue;
                        ass.Flag = "false";
                        listass.Add(ass);
                    }
                }
            }
            else
            {
                if (StaticClass.txtAssetsDetails == null)
                {
                    var ListOfAssets = App.DAUtil.GetColumnValuesBytext(59);
                    for (int i = 0; i < ListOfAssets.Count; i++)
                    {
                        Ass ass = new Ass();
                        ass.Id = ListOfAssets[i].columnValueId;
                        ass.Name = ListOfAssets[i].columnValue;
                        ass.Flag = "false";
                        listass.Add(ass);
                    }
                }
                else
                {
                    var numbers = StaticClass.txtAssetsDetails.Split(',');
                    List<string> Lass = new List<string>();
                    for (int i = 0; i < numbers.Length; i++)
                    {
                        string data = numbers[i];
                        Lass.Add(data);
                    }
                    var ListOfAssets = App.DAUtil.GetColumnValuesBytext(59);
                    for (int i = 0; i < ListOfAssets.Count; i++)
                    {
                        Ass ass = new Ass();
                        ass.Id = ListOfAssets[i].columnValueId;
                        ass.Name = ListOfAssets[i].columnValue;
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
            listView.ItemsSource = listass;
        }
        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Ass villageN = (Ass)listView.SelectedItem;

        }
      
        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (StaticClass.FamilyEdit != "True")
                StaticClass.txtAssetsDetails = null;

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
            StaticClass.txtAssetsDetails = builder.ToString();
            await Navigation.PopPopupAsync();
        }
    }
    public class Ass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Flag { get; set; }
    }
}