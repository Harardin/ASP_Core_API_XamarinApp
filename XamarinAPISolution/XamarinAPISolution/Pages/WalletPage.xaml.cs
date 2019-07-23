using dotMorten.Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinAPISolution.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WalletPage : ContentPage
    {
        public List<TestList> list = new List<TestList>()
        {
            new TestList {Name = "Alan", Email = "test@test.com"},
            new TestList {Name = "Alex", Email = "mail@test.com"},
            new TestList {Name = "Alyna", Email = "test@mail.com"},
            new TestList {Name = "Alyona", Email = "test23@test.com"},
            new TestList {Name = "Brayan", Email = "test234@test.com"},
            new TestList {Name = "Jhon", Email = "test5@test.com"},
            new TestList {Name = "Scot", Email = "test34@test.com"},
            new TestList {Name = "Alon", Email = "test656@test.com"},
            new TestList {Name = "Akon", Email = "test76@test.com"},
        };

        public WalletPage()
        {
            InitializeComponent();

            UsersSearchField.ItemsSource = list.Select(a => a.Name).ToList();
            
        }

        public class TestList
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }

        private async void ChoseReciever(object sender, EventArgs e)
        {
            string user = UsersSearchField.Text;


            if (user != null || user != "" && list.Contains(new TestList{ Name = user }))
            {
                await Navigation.PushAsync(new TransactionPage(user));
            }
            else
            {

            }

        }
    }
}