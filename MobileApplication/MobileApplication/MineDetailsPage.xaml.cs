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
    public partial class MineDetailsPage : ContentPage
    {
        public int DoubleSqCount = 30;
        UserDetails UserDetails { get; set; }
        Users User = new Users() { };
        public MineDetailsPage(UserDetails userDetails)
        {
            InitializeComponent();
            Title = "Мои детали";

            DoubleSquareCount.Text = userDetails.DoubleSquareCount.ToString() + " шт.";
            TrapeziumCount.Text = userDetails.TrapeziumCount.ToString() + " шт.";
            HexagonCount.Text = userDetails.HexagonCount.ToString() + " шт.";
            PentagonCount.Text = userDetails.RentagonCount.ToString() + " шт.";
            EqualTriangleCount.Text = userDetails.EqualTriangleCount.ToString() + " шт.";
            ParallelogramCount.Text = userDetails.ParallelogramCount.ToString() + " шт.";
            SquareCount.Text = userDetails.SquareCount.ToString() + " шт.";
            IsoscelesTriangleCount.Text = userDetails.IsoscelesTriangleCount.ToString() + " шт.";

            UserDetails = userDetails;
            User.Id = UserDetails.UserId;
        }

        private void BackToMainBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainAppPage(User)); // на главную
        }

        private void ChangeCountBtn_Clicked(object sender, EventArgs e)
        {
            // UPDATE
            Navigation.PushModalAsync(new UpdateDetailsPage(UserDetails));
        }
    }
}