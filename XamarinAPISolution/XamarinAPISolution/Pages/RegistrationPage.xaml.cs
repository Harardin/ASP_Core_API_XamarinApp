using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAPISolution.Models;
using XamarinAPISolution.CommonFunctions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAPISolution.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        private RegDataInput regDataInput;
        public RegistrationPage(RegDataInput _regDataInput)
        {
            InitializeComponent();
            BindingContext = regDataInput = _regDataInput;
        }

        private async void RegistrationClick(object sender, EventArgs e)
        {
            IRegistrationFunction registration = new RegistrationFunction();
            // View Settings
            ErrorMessage.IsVisible = false;
            RegistrationButton.IsVisible = false;
            LoadingLable.IsVisible = true;

            if (await registration.Registration(regDataInput))
            {
                LoginDataInput loginData = new LoginDataInput() { Email = regDataInput.Email, Password = regDataInput.Password };
                // Performing AutoLogin Action
                ILoginFunction loginFunction = new LoginFunction();
                if (await loginFunction.Login(loginData))
                {
                    await Navigation.PushAsync(new WalletPage());
                }
                else
                {
                    // View Settings
                    RegistrationButton.IsVisible = true;
                    LoadingLable.IsVisible = false;
                    // Display Error Message
                    ErrorMessage.IsVisible = true;
                }
            }
            else
            {
                // View Settings
                RegistrationButton.IsVisible = true;
                LoadingLable.IsVisible = false;
                // Display Error Message
                ErrorMessage.IsVisible = true;
            }
        }
    }
}