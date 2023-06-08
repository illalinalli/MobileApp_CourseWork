using MobileApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FishFirstPage : ContentPage
    {
        public FishFirstPage(Users user)
        {
            InitializeComponent();
        }
        Users user { get; set; }
        private void FishToMainPageBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();
        }
    }
}