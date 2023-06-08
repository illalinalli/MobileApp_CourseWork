using MobileApplication.Services;
using MobileApplication.Views;
using System;
using System.Drawing;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication
{
    public partial class App : Application
    {

        
        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new FirstPage();
            //MainPage = new NavigationPage(new FirstPage());
        }
        
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
