using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinAPISolution.Views;
using XamarinAPISolution.Pages;

namespace XamarinAPISolution
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            MainViewContent.Content = new WelcomeView();
        }
    }
}
