using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication
{
    public class PathsToPic { 
        public string SourceToPic { get; set; }
    }
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FavoritesFiguresPage : ContentPage
    {
        public FavoritesFiguresPage(List<Models.FigureCatalog> favFigures)
        {
            InitializeComponent();
           
            Image image = new Image()
            {
            };
            
            List<PathsToPic> paths = new List<PathsToPic>();

            foreach (var figures in favFigures)
            {
                if (figures.Name == "Рыбка")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "fish.png" });
                    
                }
                if (figures.Name == "Ромб")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "romb.png" });
                   
                }
                if (figures.Name == "Осьминог")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "osminog.png" });

                }
                if (figures.Name == "Робот")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "robot.png" });

                }
                if (figures.Name == "Дом")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "house.png" });

                }
                if (figures.Name == "Ракета")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "raketa.png" });

                }
                if (figures.Name == "Часы")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "chasi.png" });

                }
                if (figures.Name == "Улитка")
                {
                    UserFavFigures.Opacity = 1;
                    paths.Add(new PathsToPic { SourceToPic = "ylitka.png" });

                }
            }

            UserFavFigures.ItemsSource = paths;

        }

        private void BackToMainBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(); // на главную
           //Navigation.PopAsync();
        }
    }
}