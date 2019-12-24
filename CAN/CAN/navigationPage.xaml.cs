using CAN.Models;
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
using Plugin.RestClient;
using CAN.ViewModels;
using CAN.Models;
using Plugin.Connectivity;
namespace CAN
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class navigationPage : ContentPage
    {
        public ListView ListView;
        public navigationPage()
        {
            InitializeComponent();
            var name = App.DAUtil.GetUsers();
            lblusername.Text = name[0].Name;
            BindingContext = new MasterDetailPage1MasterViewModel();
            ListView = MenuItemsListView;
            lblmessage.IsVisible = false;
            lblmessage.Text = "Data Sync.....";
        }
        class MasterDetailPage1MasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<MasterDetailPage1MenuItem> MenuItems { get; set; }

            public MasterDetailPage1MasterViewModel()
            {
               
                MenuItems = new ObservableCollection<MasterDetailPage1MenuItem>(new[]
                {
                    new MasterDetailPage1MenuItem { Id = 0, Title = "Change Current Village",ImgIcon="ic_allVillage.png" },
                    new MasterDetailPage1MenuItem { Id = 1, Title = "Sync Master Data",ImgIcon="ic_Sync.png" },
                    new MasterDetailPage1MenuItem { Id = 2, Title = "Push Data to Server", ImgIcon="ic_PushData.png" },
                     new MasterDetailPage1MenuItem { Id = 4, Title ="Change The Last Sync Date",ImgIcon="ic_Sync.png" },
                    new MasterDetailPage1MenuItem { Id = 3, Title = "Monthly Monitoring", ImgIcon="ic_monthly.png" },
                   // new MasterDetailPage1MenuItem { Id = 3, Title = "Add Monthly Data" },
                   // new MasterDetailPage1MenuItem { Id = 4, Title = "Add Red Flag Data" },

                    new MasterDetailPage1MenuItem { Id = 5, Title = "LogOut",ImgIcon="ic_Logout.png" },

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

        private async void MenuItemsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
           
            StaticClass.PageName = "";
            var item = e.SelectedItem as MasterDetailPage1MenuItem;
            if (item == null)
                return;
            if (item.Id == 0)
            {
                lblmessage.IsVisible = false;
                lblmessagewait.IsVisible = true;
                txtactive.IsVisible = true;
                await Task.Delay(1000);
                StaticClass.PageName = "VillagePage";
                StaticClass.MonthlyMonitoringName = "";
                Application.Current.MainPage = new MasterDetailPage1();
            }
            if (item.Id == 1)
            {
                if (CrossConnectivity.Current.IsConnected)
                {
                txtactive.IsVisible = true;
                lblmessage.IsVisible = true;
                var UserDetails = App.DAUtil.GetUsers().FirstOrDefault();
                int userId = UserDetails.UserId;
                UserLoginById userLoginById = new UserLoginById()
                {
                    userId= userId,
                    Token= UserDetails.Token
                };
               LoginData loginData = new LoginData()
               {
                    UserName= UserDetails.UserName,
                    Password= UserDetails.Password
                };
                
                RestClient<LoginData> restClient = new RestClient<LoginData>();
                var TokenUpdate =await  restClient.UserLogin(loginData);
                RestClient<UserLoginById> getLogin = new RestClient<UserLoginById>();
                var GetData = await getLogin.synData(userLoginById);
                    // var GetMasterData = await getLogin.PullNewData(userLoginById);
                    if (StaticClass.ResponceStatus == "Not Found")
                    {

                        DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                        txtactive.IsRunning = false;
                        txtactive.IsVisible = false;
                        lblmessage.IsVisible = false;
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("Sync Master Data Success");
                        txtactive.IsVisible = false;
                        lblmessage.IsVisible = false;
                        StaticClass.PageName = "VillagePage";
                        Application.Current.MainPage = new MasterDetailPage1();
                    }
                }
                else
                {
                    DependencyService.Get<Toast>().Show("There is no  internet connection");
                }
            }

            if (item.Id == 2)
            {
                try
                {
                  
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        txtactive.IsVisible = true;
                        lblmessage.IsVisible = true;
                        DateTime checkdateFlag;
                        checkdateFlag = DateTime.Now;
                        lblmessage.Text = "Data Push.......";
                        long id = StaticClass.VillageID;
                        var CheckDate = App.DAUtil.Getpushdatatime(); //Check Time Flag
                        var Userdata = App.DAUtil.GetUsers();
                        PushData pushData = new PushData();
                        pushData.userId = Userdata[0].UserName; // Demo
                        pushData.password = Userdata[0].Password;
                        LoginData loginData = new LoginData()
                        {
                            UserName = Userdata[0].UserName,
                            Password = Userdata[0].Password
                        };
                        RestClient<LoginData> restClient = new RestClient<LoginData>();
                        var TokenUpdate = await restClient.UserLogin(loginData);
                        if (StaticClass.ResponceStatus == "Not Found")
                        {
                            DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                        }
                        else
                        {
                            long idd = StaticClass.VillageID;
                            if (CheckDate.Count > 0)
                            {
                                checkdateFlag = CheckDate[0].CheckDate;
                            }

                            if (CheckDate.Count > 0 && CheckDate[0].Flage == true)
                            {
                                // var ListData = App.DAUtil.GetAllFamilyByLocation(idd);
                                var ListOfFamilyData = App.DAUtil.GetAllFamily().Where(x => x.DOU >= checkdateFlag).ToList();
                                //  var ListOfChildData = App.DAUtil.GetAllChildDatetime(checkdateFlag);
                                var ListOfChildData = App.DAUtil.GetAllChild().Where(x => x.DOU >= checkdateFlag).ToList();
                                // var Listmonthlydata = App.DAUtil.GetGrowthRegistersDateTime(checkdateFlag);
                                var Listmonthlydata = App.DAUtil.GetGrowthRegisters().Where(x => x.Dou >= checkdateFlag).ToList();
                                // var ListofMotherData = App.DAUtil.GetAllGrowthRegisterMotherDateTime(checkdateFlag);
                                var ListofMotherData = App.DAUtil.GetAllGrowthRegisterMother().Where(x => x.DOU >= checkdateFlag).ToList();
                                //  var ListOfredFlagData = App.DAUtil.GetAllRedFlagRegisterDateTime(checkdateFlag);
                                var ListOfredFlagData = App.DAUtil.GetAllRedFlagRegister().Where(x => x.Dou >= checkdateFlag).ToList();
                                pushData.childData = ListOfChildData;
                                pushData.familyData = ListOfFamilyData;
                                pushData.growthData = Listmonthlydata;
                                pushData.growthRegisterMotherData = ListofMotherData;
                                pushData.redFlagData = ListOfredFlagData;
                            }
                            else
                            {
                                var ListOfFamilyData = App.DAUtil.GetAllFamily();
                                var ListOfChildData = App.DAUtil.GetAllChild();
                                DateTime dt = ListOfChildData[0].AWCEntryDate.Value;
                                var Listmonthlydata = App.DAUtil.GetGrowthRegisters();
                                var ListofMotherData = App.DAUtil.GetAllGrowthRegisterMother();
                                var ListOfredFlagData = App.DAUtil.GetAllRedFlagRegister();
                                pushData.childData = ListOfChildData;
                                pushData.familyData = ListOfFamilyData;
                                pushData.growthData = Listmonthlydata;
                                pushData.growthRegisterMotherData = ListofMotherData;
                                pushData.redFlagData = ListOfredFlagData;
                            }
                            //foreach(var fData in pushData.familyData)
                            //{
                            //    if("0001-01-01T00:00:00"== fData.DOE.ToString())
                            //    {
                            //        fData.DOE = DateTime.Now;
                            //    }
                            //}
                            //foreach (var cData in pushData.childData)
                            //{
                            //    if ("0001-01-01T00:00:00" == cData.DOE.ToString())
                            //    {
                            //        cData.DOE = DateTime.Now;
                            //    }
                            //}
                            //foreach (var mData in pushData.growthRegisterMotherData)
                            //{
                            //    if ("0001-01-01T00:00:00" == mData.DOE.ToString())
                            //    {
                            //        mData.DOE = DateTime.Now;
                            //    }
                            //}
                            //foreach (var gData in pushData.growthData)
                            //{
                            //    if ("0001-01-01T00:00:00" == gData.Doe.ToString())
                            //    {
                            //        gData.Doe = DateTime.Now;
                            //    }
                            //}
                            //foreach (var rData in pushData.redFlagData)
                            //{
                            //    if ("0001-01-01T00:00:00" == rData.Doe.ToString())
                            //    {
                            //        rData.Doe = DateTime.Now;
                            //    }
                            //}
                           
                            RestClient<PushData> getLogin = new RestClient<PushData>();
                            var GetData = await getLogin.PushNewData(pushData);
                            if (StaticClass.ResponceStatus == "Not Found")
                            {
                                DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                                txtactive.IsVisible = false;
                                lblmessage.IsVisible = false;
                            }
                            else
                            {
                                if (GetData == true)
                                {
                                    if (CheckDate.Count > 0)
                                        App.DAUtil.deletepushdatatime();
                                    TblPushDataTime tblPushDataTime = new TblPushDataTime();
                                    tblPushDataTime.Flage = true;
                                    tblPushDataTime.CheckDate = DateTime.Now;
                                    App.DAUtil.SavepushDataTime(tblPushDataTime);
                                    DependencyService.Get<Toast>().Show("Data Push Success");
                                    txtactive.IsVisible = false;
                                    lblmessage.IsVisible = false;
                                }
                                else
                                {
                                    DependencyService.Get<Toast>().Show("Data Push Fail Please try again");
                                    txtactive.IsVisible = false;
                                    lblmessage.IsVisible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("There is no  internet connection");
                    }
                    }
                catch(Exception ex)
                {
                    txtactive.IsVisible = false;
                    lblmessage.IsVisible = false;
                    DependencyService.Get<Toast>().Show("Data Push Success");
                    txtactive.IsVisible = false;
                    lblmessage.IsVisible = false;
                    // DependencyService.Get<Toast>().Show("Error");
                }
            }
            if (item.Id == 3)
            {
               // StaticClass.PageName = "VillagePage";
                StaticClass.PageName = "MonthlyMonitoring";
                StaticClass.MonthlyMonitoringName = "a";
                Application.Current.MainPage = new MasterDetailPage1();
            }
            if (item.Id == 4)
            {
                try
                {
                    StaticClass.LastsyncDateChange = "y";
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        Application.Current.MainPage = new NavigationPage(new PinPage())
                        {
                            BarBackgroundColor = Color.FromHex("#ffbf00"),
                            BarTextColor = Color.White
                        };
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("There is no  internet connection");
                    }
                }
                catch (Exception ex)
                {
                    
                    DependencyService.Get<Toast>().Show("Date Push Success");
                    
                }
            }
            if (item.Id == 5)
            {
                var action = await DisplayAlert("Exit", "Are you sure to close", "Yes", "No");
                if (action)
                {

                    DependencyService.Get<ExitAppInterface>().exitApp();
                }
              else
                {
                   
                }
                
            }
        }
    }
}