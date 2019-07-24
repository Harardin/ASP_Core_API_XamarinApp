using asp_xamar_solution.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinAPISolution.Models;
using System.Net.Http.Headers;
using System.ComponentModel;

namespace XamarinAPISolution.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletPage : ContentPage
    {
        // Bindable Property
        private string hostName;
        private string hostBalance;
        public string HostName
        {
            get { return hostName; }
            set
            {
                hostName = value;
                OnPropertyChanged("HostName");
            }
        }
        public string HostBalance
        {
            get { return hostBalance; }
            set
            {
                hostBalance = value;
                OnPropertyChanged("HostBalance");
            }
        }


        private List<UsersListModel> UsersList = new List<UsersListModel>();
        private HttpClient httpClient = new HttpClient();
        private readonly ApiMethodsList apiMethods = new ApiMethodsList();
        private UserWalletData userWalletData = new UserWalletData();
        private UserDataModel localUserData = new UserDataModel();
        private ObservableCollection<TransactionsHistoryModel> TrObservable = new ObservableCollection<TransactionsHistoryModel>();


        public WalletPage()
        {
            InitializeComponent();
            BindingContext = this;
            HostName = "Name: Loading...";
            HostBalance = "Balance: Loading...";

            // Geting Token and other UserData
            if (!File.Exists(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userToken.json")))
            {
                NavigateToWelcomePage();
            }

            string LocalJsonToken = File.ReadAllText(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "userToken.json"));
            localUserData = JsonConvert.DeserializeObject<UserDataModel>(LocalJsonToken);

            // Setting out HttpClient
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", localUserData.token);

            // Setting Data for a Host user
            Task.Run(async () => await GetHostUserDataAsync(localUserData.useremail));

            // Setting Data for autocompleat search
            Task.Run(async () => await GetListOfUsersAsync());

            DataTemplate template = new DataTemplate(() => 
            {
                Grid grid = new Grid() { BackgroundColor = Color.AliceBlue };

                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 0
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 1
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 2
                grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // 3
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto }); // 0

                Label trDate = new Label();
                Label CorrespondentName = new Label();
                Label trAmount = new Label();
                Label RestBalance = new Label();
                trDate.SetBinding(Label.TextProperty, "trDate");
                CorrespondentName.SetBinding(Label.TextProperty, "CorrespondentName");
                trAmount.SetBinding(Label.TextProperty, "trAmount");
                RestBalance.SetBinding(Label.TextProperty, "RestBalance");

                grid.Children.Add(trDate, 0, 0);
                grid.Children.Add(CorrespondentName, 1, 0);
                grid.Children.Add(trAmount, 2, 0);
                grid.Children.Add(RestBalance, 3, 0);
                return new ViewCell { View = grid };
            });
            TrListView.ItemTemplate = template;
            TrListView.ItemsSource = TrObservable;
            // Setting Data for Transactions History
            Task.Run(async () => await SettingHistoryAsync(localUserData.useremail)).Wait();
        }


        // Setting Transactions History
        private async Task SettingHistoryAsync(string email)
        {
            HttpResponseMessage responce = await httpClient.GetAsync(apiMethods.API_TransactionsHistory_GET() + email);
            string JsonResponce = await responce.Content.ReadAsStringAsync();
            List<TransactionsHistoryModel> TrModel = new List<TransactionsHistoryModel>();
            TrModel = JsonConvert.DeserializeObject<List<TransactionsHistoryModel>>(JsonResponce).ToList();
            foreach(var model in TrModel)
            {
                System.Threading.Thread.Sleep(2000);
                TrObservable.Add(model);
            }
        }
        // Setting Data for AutoCompleatSearch
        private async Task GetListOfUsersAsync()
        {
            HttpResponseMessage responce = await httpClient.GetAsync(apiMethods.API_UsersList_GET());
            string JsonResponce = await responce.Content.ReadAsStringAsync();
            UsersList = JsonConvert.DeserializeObject<List<UsersListModel>>(JsonResponce).ToList();
            UsersSearchField.ItemsSource = UsersList.Select(a => a.Name).ToList();
        }
        // Seting Data for Host User
        private async Task GetHostUserDataAsync(string email)
        {
            HttpResponseMessage responce = await httpClient.GetAsync(apiMethods.API_UserWalletInfo_GET() + email);
            string JsonResponce = await responce.Content.ReadAsStringAsync();
            userWalletData = JsonConvert.DeserializeObject<UserWalletData>(JsonResponce);
            HostName = "Name: " + userWalletData.UserName;
            HostBalance = "Balance: " + userWalletData.Coins;
        }

        private async void NavigateToWelcomePage()
        {
            await Navigation.PushAsync(new MainPage());
        }

        private async void ChoseReciever(object sender, EventArgs e)
        {
            string user = UsersSearchField.Text;
            RecipientError.IsVisible = false;
            int element = UsersList.Where(s => s.Name == user).Select<UsersListModel, int>(x => UsersList.IndexOf(x)).Single<int>();
            if (user != null && user != "" && UsersList.Contains(UsersList.ElementAt(element)))
            {
                await Navigation.PushAsync(new TransactionPage(localUserData.token, userWalletData, user));
            }
            else
            {
                RecipientError.IsVisible = true;
            }
        }

        private async void RefreshScreen(object sender, SwipedEventArgs e)
        {
            // Refreshes data on SwipeDown
            TrObservable.Clear();
            await SettingHistoryAsync(localUserData.useremail);
            await GetHostUserDataAsync(localUserData.useremail);
        }
    }
}