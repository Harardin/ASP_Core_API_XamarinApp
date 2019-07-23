using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAPISolution.CommonFunctions;
using XamarinAPISolution.Models;

namespace XamarinAPISolution.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        private LoginDataInput loginData;
        public LoginPage(LoginDataInput _loginData)
        {
            InitializeComponent();
            BindingContext = loginData = _loginData;
        }

        private async void Login(object sender, EventArgs e)
        {
            ILoginFunction loginFunction = new LoginFunction();
            // Appearence settings
            CreateNewAccountLable.IsVisible = false;
            CreateNewAccountAction.IsVisible = false;
            LoginError.IsVisible = false;
            SignInButton.IsVisible = false;
            LoadingLable.IsVisible = true;
            //

            if (await loginFunction.Login(loginData))
            {
                // Appearence settings
                LoadingLable.IsVisible = false;
                SignInButton.IsVisible = true;

                await Navigation.PushAsync(new WalletPage());
            }
            else
            {
                // Appearence settings
                LoadingLable.IsVisible = false;
                SignInButton.IsVisible = true;
                LoginError.IsVisible = true;
                CreateNewAccountLable.IsVisible = true;
                CreateNewAccountAction.IsVisible = true;
            }
        }

        // Proceed to SugnUp if Account don't excists
        private async void CreateNewAccount(object sender, EventArgs e)
        {
             await Navigation.PushAsync(new RegistrationPage(new Models.RegDataInput()));
        }
    }
}