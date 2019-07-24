using System;
using System.IO;
using Xamarin.Forms;
using Newtonsoft.Json;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json.Linq;
using XamarinAPISolution.Models;
using XamarinAPISolution.Pages;

namespace XamarinAPISolution
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Here will be a check if token excists
            if (File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userToken.json")))
            {
                UserDataModel userModel = new UserDataModel();
                string userJson = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userToken.json"));
                userModel = JsonConvert.DeserializeObject<UserDataModel>(userJson);

                if (userModel.token != "" || userModel.token != null)
                {
                    // Replace with WalletPage (This happens only if Token excists)
                    MainPage = new NavigationPage(new WalletPage());
                }
            }
            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
