using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAPISolution.Pages;

namespace XamarinAPISolution.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeView : ContentView
    {
        public WelcomeView()
        {
            InitializeComponent();
        }

        private async void Registration(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegistrationPage(new Models.RegDataInput()));
        }

        private async void Login(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage(new Models.LoginDataInput()));
        }
    }
}