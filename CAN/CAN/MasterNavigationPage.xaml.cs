using CAN.Models;
using CAN.ViewModels;
using Plugin.Connectivity;
using Plugin.RestClient;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Services;
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
    public partial class MasterNavigationPage : MasterDetailPage
    {
        public MasterNavigationPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelected;
           check();
        }

        private async void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
          
            var item = e.SelectedItem as MasterNavigationPageMenuItem;
            if (item == null)
                return;

            var page = (Page)Activator.CreateInstance(item.TargetType);
            page.Title = item.Title;
            if (item.Id == 0)
            {
                Detail = new NavigationPage(new ListOfVillage())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
                
            }
            if (item.Id == 1)
            {
               
                
                if (CrossConnectivity.Current.IsConnected)
                {
                    
                    var pages = new loderpage();
                    await Navigation.PushPopupAsync(pages);
                    //txtactive.IsVisible = true;
                    // lblmessage.IsVisible = true;
                    var UserDetails = App.DAUtil.GetUsers().FirstOrDefault();
                    int userId = UserDetails.UserId;
                    UserLoginById userLoginById = new UserLoginById()
                    {
                        userId = userId,
                        Token = UserDetails.Token
                    };
                    LoginData loginData = new LoginData()
                    {
                        UserName = UserDetails.UserName,
                        Password = UserDetails.Password
                    };

                    RestClient<LoginData> restClient = new RestClient<LoginData>();
                    var TokenUpdate = await restClient.UserLogin(loginData);
                    RestClient<UserLoginById> getLogin = new RestClient<UserLoginById>();
                    var GetData = await getLogin.synData(userLoginById);
                    // var GetMasterData = await getLogin.PullNewData(userLoginById);
                    if (StaticClass.ResponceStatus == "Not Found")
                    {
                        await Navigation.PopPopupAsync();
                        DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                        // txtactive.IsRunning = false;
                        //txtactive.IsVisible = false;
                        //lblmessage.IsVisible = false;
                    }
                    else
                    {
                        await Navigation.PopPopupAsync();
                        DependencyService.Get<Toast>().Show("Sync Master Data Success");
                       
                        // txtactive.IsVisible = false;
                        // lblmessage.IsVisible = false;
                        StaticClass.PageName = "VillagePage";
                        Detail = new NavigationPage(new ListOfVillage())
                        {
                            BarBackgroundColor = Color.FromHex("#ffbf00")

                        };
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
                        var pages = new loderpage();
                        await Navigation.PushPopupAsync(pages);
                        // txtactive.IsVisible = true;
                        // lblmessage.IsVisible = true;
                        DateTime checkdateFlag;
                        checkdateFlag = DateTime.Now;
                        //lblmessage.Text = "Data Push.......";
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
                            await Navigation.PopPopupAsync();
                            DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                        }
                        else
                        {
                            long idd = StaticClass.VillageID;
                            if (CheckDate.Count > 0 && CheckDate[0].Flage == true)
                            {
                                checkdateFlag = CheckDate[0].CheckDate;
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

                            RestClient<PushData> getLogin = new RestClient<PushData>();
                            var GetData = await getLogin.PushNewData(pushData);
                            if (StaticClass.ResponceStatus == "Not Found")
                            {
                                await Navigation.PopPopupAsync();
                                DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                               // txtactive.IsVisible = false;
                                //lblmessage.IsVisible = false;
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
                                    await Navigation.PopPopupAsync();
                                    DependencyService.Get<Toast>().Show("Data Push Success");
                                    //txtactive.IsVisible = false;
                                   // lblmessage.IsVisible = false;
                                }
                                else
                                {
                                    await Navigation.PopPopupAsync();
                                    DependencyService.Get<Toast>().Show("Data Push Fail Please try again");
                                    //txtactive.IsVisible = false;
                                    //lblmessage.IsVisible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        DependencyService.Get<Toast>().Show("There is no  internet connection");
                    }
                }
                catch (Exception ex)
                {
                   // txtactive.IsVisible = false;
                    //lblmessage.IsVisible = false;
                    DependencyService.Get<Toast>().Show("Data Push Success");
                    //txtactive.IsVisible = false;
                   // lblmessage.IsVisible = false;
                    // DependencyService.Get<Toast>().Show("Error");
                }
            }
            if (item.Id == 3)
            {
                // StaticClass.PageName = "VillagePage";
                StaticClass.PageName = "MonthlyMonitoring";
                StaticClass.MonthlyMonitoringName = "a";
              //  Application.Current.MainPage = new MasterDetailPage1();
            }
            if (item.Id == 4)
            {
                try
                {
                    StaticClass.LastsyncDateChange = "y";
                    if (CrossConnectivity.Current.IsConnected)
                    {
                        //Application.Current.MainPage = new NavigationPage(new PinPage())
                        //{
                        //    BarBackgroundColor = Color.FromHex("#ffbf00"),
                        //    BarTextColor = Color.White
                        //};
                        Detail = new NavigationPage(new PinPage())
                        {
                            BarBackgroundColor = Color.FromHex("#ffbf00")

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
            //if (StaticClass.PageName == "HomePage")
            //{
            //    StaticClass.PageName = "";
            //    Detail = new NavigationPage(new HeaderTabbed())
            //    {
            //        BarBackgroundColor = Color.FromHex("#ffbf00")

            //    };
            //}
            //else
            //{

            //    Detail = new NavigationPage(new ListOfVillage())
            //    {
            //        BarBackgroundColor = Color.FromHex("#ffbf00")

            //    };
            //}
            //Detail = new NavigationPage(page);
            IsPresented = false;

            MasterPage.ListView.SelectedItem = null;
        }
        private void check()
        {
            if (StaticClass.PageName == "HomePage")
            {
                StaticClass.PageName = "";
                Detail = new NavigationPage(new HeaderTabbed())
                {
                    BarBackgroundColor = Color.FromHex("#ffbf00")

                };
            }
        }
    }
}