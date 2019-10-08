using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CAN.Models;
using Plugin.RestClient;
using Plugin.Connectivity;
namespace CAN
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            lblErrorMessage.IsVisible = false;
            txtInductor.IsRunning = false;
            lblmessage.IsVisible = false;
        }
        private async void btnsubmit_Click(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (txtUserId.Text != "" && txtUserId.Text != null && txtPassword.Text != "" && txtPassword.Text != null)
                {
                    btnSignIn.TextColor = Color.Chocolate;
                    txtInductor.IsVisible = true;
                    lblmessage.IsVisible = true;
                    LoginData ld = new LoginData();
                    ld.UserName = txtUserId.Text;
                    ld.Password = txtPassword.Text;
                    StaticClass.UserPassword = txtPassword.Text;
                    RestClient<LoginData> getLoginData = new RestClient<LoginData>();
                    txtInductor.IsRunning = true;
                    var GetLoginDatas = await getLoginData.UserLogin(ld);
                    if (GetLoginDatas == true)
                    {
                        var UserDetails = App.DAUtil.GetUsers().FirstOrDefault(); ;
                        int userId = UserDetails.UserId;

                        UserLoginById userLoginById = new UserLoginById();
                        userLoginById.userId = userId;
                        userLoginById.Token = UserDetails.Token;
                        RestClient<UserLoginById> getLogin = new RestClient<UserLoginById>();
                        var GetData = await getLogin.synData(userLoginById);
                        var GetMasterData = await getLogin.PullNewData(userLoginById);
                        if (GetMasterData == true)
                        {
                            if (StaticClass.ResponceStatus == "Not Found")
                            {
                                
                                DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                                txtInductor.IsRunning = false;
                                txtInductor.IsVisible = false;
                                lblmessage.IsVisible = false;
                            }
                            else
                            {
                                Application.Current.MainPage = new NavigationPage(new PinPage())
                                {
                                    BarBackgroundColor = Color.FromHex("ffbf00"),
                                    BarTextColor = Color.White

                                };
                            }
                        }
                        else
                        {
                            
                            DependencyService.Get<Toast>().Show("Time out Please try");
                            txtInductor.IsRunning = false;
                            txtInductor.IsVisible = false;
                            lblmessage.IsVisible = false;
                        }
                    }
                    else
                    {

                        if (StaticClass.ResponceStatus == "Not Found")
                        {
                            DependencyService.Get<Toast>().Show("The server is not responding. Please try again later.");
                            txtInductor.IsVisible = false;
                            lblmessage.IsVisible = false;
                        }
                        else
                        {
                            DependencyService.Get<Toast>().Show("Incorrect UserName or Password.");
                            txtInductor.IsVisible = false;
                            lblmessage.IsVisible = false;
                        }
                    }
                }
                else
                {
                    DependencyService.Get<Toast>().Show("Please Check UserName and Password");
                }
            }
            else
            {
                DependencyService.Get<Toast>().Show("There is no  internet connection");
            }
        }


        
    }
}
public  class LoginData
{
    public string UserName { get; set; }
    public string Password { get; set; }
}
public class UserLoginById
{
    public int userId { get; set; }
    public string Token { get; set; }
}