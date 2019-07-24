using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAPISolution.Models;
using System.Net.Http;
using System.Net.Http.Headers;

namespace XamarinAPISolution.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TransactionPage : ContentPage
    {
        public readonly string RecieverName;
        private readonly UserWalletData userWalletData;
        private readonly string Token;

        public TransactionPage(string token, UserWalletData _userWallet, string _user)
        {
            InitializeComponent();
            Token = token;
            userWalletData = _userWallet;
            RecieverName = _user;
            CoinsRecieverName.Text = "Coins Reciever: " + RecieverName;
        }

        private async void SendCoinsAsync(object sender, EventArgs e)
        {
            // Main Checks
            ErrorLable.IsVisible = false;
            if (0 >= int.Parse(CoinsToSendField.Text) || CoinsToSendField.Text == null)
            {
                ErrorLable.Text = "Amouth should be atleast 1 Coin";
                ErrorLable.IsVisible = true;
                return;
            }
            if (userWalletData.Coins < int.Parse(CoinsToSendField.Text))
            {
                ErrorLable.Text = "You don't have enough Coins";
                ErrorLable.IsVisible = true;
                return;
            }
            MainButton.IsVisible = false;
            LoadingLable.IsVisible = true;

            ApiMethodsList api = new ApiMethodsList();
            Api_SendCoinsModel ApiModel = new Api_SendCoinsModel() { Sender = userWalletData.UserName, Reciever = RecieverName, Amount = int.Parse(CoinsToSendField.Text) };
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

            string JsonData = JsonConvert.SerializeObject(ApiModel);
            StringContent content = new StringContent(JsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage result = await client.PostAsync(api.API_MakeTransaction_POST(), content);
            string answer = await result.Content.ReadAsStringAsync();

            MainButton.IsVisible = true;
            if(answer.Contains("true"))
            {
                BackToWalletPage.IsVisible = true;
                LoadingLable.Text = "Transaction success";
            }
            else
            {
                BackToWalletPage.IsVisible = true;
                LoadingLable.Text = "Transaction error occurred";
            }
        }

        private async void BackToWalletPageAsync(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}