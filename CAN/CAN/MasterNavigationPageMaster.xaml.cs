using CAN.Models;
using Plugin.Connectivity;
using Plugin.RestClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CAN
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterNavigationPageMaster : ContentPage
    {
        public ListView ListView;

        public MasterNavigationPageMaster()
        {
            InitializeComponent();

            BindingContext = new MasterNavigationPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class MasterNavigationPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterNavigationPageMenuItem> MenuItems { get; set; }

            public MasterNavigationPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<MasterNavigationPageMenuItem>(new[]
                {
                    new MasterNavigationPageMenuItem { Id = 0, Title = "Change Current Village",ImgIcon="ic_allVillage.png"},
                    new MasterNavigationPageMenuItem { Id = 1, Title = "Sync Master Data",ImgIcon="ic_Sync.png"},
                    new MasterNavigationPageMenuItem { Id = 2, Title = "Push Data to Server", ImgIcon="ic_PushData.png" },
                    new MasterNavigationPageMenuItem { Id = 4, Title ="Change The Last Sync Date",ImgIcon="ic_Sync.png" },
                    new MasterNavigationPageMenuItem { Id = 3, Title = "Monthly Monitoring", ImgIcon="ic_monthly.png" },
                    new MasterNavigationPageMenuItem { Id = 5, Title = "LogOut",ImgIcon="ic_Logout.png"  },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }

        
    }
}