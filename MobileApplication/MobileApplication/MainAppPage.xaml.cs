using MobileApplication.Models;
using MobileApplication.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Xamarin.Essentials.Permissions;

namespace MobileApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainAppPage : ContentPage
    {
        List<FigureCatalog> FavFigures = new List<FigureCatalog>();
        public Users user { get; set; }
        UserDetails userDetails { get; set; }

        SqlConnection sqlConnection { get; set; }
        public MainAppPage()
        {
            InitializeComponent();
            try {
                string serverDbName = "CourseWork";
                string serverName = "172.28.32.1";//создаём общего посредника - общий IP адрес
                                                   // логин и пароль кпользователя к серверу
                string serverUserName = "alina";
                string serverUserPassword = "alina";

                var sqlConnString = new System.Data.SqlClient.SqlConnectionStringBuilder
                {
                    InitialCatalog = serverDbName,
                    DataSource = serverName,
                    IntegratedSecurity = false,
                    UserID = serverUserName,
                    Password = serverUserPassword
                }.ConnectionString;
                // создаем SQL соединение
                sqlConnection = new SqlConnection(sqlConnString); // таким образом происходит взаимождействие приложения и БД
            }
            catch (Exception ex) {
                var msg = ex.Message;
                throw;
            }
            
            // открываем это соединение
            sqlConnection.Open();

        }

        public MainAppPage(Users user)
        {
            InitializeComponent();
            this.user = user;

            try
            {
                string serverDbName = "CourseWork";
                string serverName = "172.28.32.1";//создаём общего посредника - общий IP адрес
                                                   // логин и пароль кпользователя к серверу
                string serverUserName = "alina";
                string serverUserPassword = "alina";

                var sqlConnString = new System.Data.SqlClient.SqlConnectionStringBuilder
                {
                    InitialCatalog = serverDbName,
                    DataSource = serverName,
                    IntegratedSecurity = false,
                    UserID = serverUserName,
                    Password = serverUserPassword
                }.ConnectionString;
                // создаем SQL соединение
                sqlConnection = new SqlConnection(sqlConnString); // таким образом происходит взаимождействие приложения и БД
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw;
            }
            // открываем это соединение
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
           
        }

        private void MineDetailsButton_Clicked(object sender, EventArgs e)
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            // по user находим его ID и кол-во деталей
            string queryString = $"select * from dbo.UserDetails where UserId = '{user.Id}'";
            SqlCommand command1 = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command1.ExecuteReader();
            userDetails = new UserDetails();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    
                    userDetails.UserId = user.Id;
                    userDetails.SquareCount = Convert.ToInt32(reader["SquareCount"]);
                    userDetails.EqualTriangleCount = Convert.ToInt32(reader["EqualTriangleCount"]);
                    userDetails.ParallelogramCount = Convert.ToInt32(reader["ParallelogramCount"]);
                    userDetails.TrapeziumCount = Convert.ToInt32(reader["TrapeziumCount"]);
                    userDetails.RentagonCount = Convert.ToInt32(reader["RentagonCount"]);
                    userDetails.IsoscelesTriangleCount = Convert.ToInt32(reader["IsoscelesTriangleCount"]);
                    userDetails.HexagonCount = Convert.ToInt32(reader["HexagonCount"]);
                    userDetails.DoubleSquareCount = Convert.ToInt32(reader["DoubleSquareCount"]);
                }
            }
            reader.Close();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            if (userDetails.UserId != 0)
            {
                Navigation.PushModalAsync(new MineDetailsPage(userDetails));
            }
           
        }
        private void FavoritesBtn_Clicked(object sender, EventArgs e)
        {
           
            sqlConnection.Close();
            // переносим сюда favFigures
            Navigation.PushModalAsync(new FavoritesFiguresPage(FavFigures));
        }

        private async void ShowFishSchemeBtn_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Рыбка.Text;
            UserDetails userDetails = new UserDetails();
            userDetails.UserId = user.Id; // передали айдишник
            
            string queryString = $"select * from FigureCatalog where Name = '{figureCatalog.Name}'";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            //sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // присваиваем объекту каталога кол-во деталей
                    figureCatalog.SquareCount = Convert.ToInt32(reader["SquareCount"]);
                    figureCatalog.EqualTriangleCount = Convert.ToInt32(reader["EqualTriangleCount"]);
                }
            }
            reader.Close();
            sqlConnection.Close();
            sqlConnection.Open();
            // ищем кол-во деталей у пользователя
            string queryStringUser = $"select * from UserDetails where UserId = {userDetails.UserId}"; //
            SqlCommand commandUser = new SqlCommand(queryStringUser, sqlConnection);
            SqlDataReader readerUser = commandUser.ExecuteReader();
            if (readerUser.HasRows)
            {
                while (readerUser.Read())
                {
                    userDetails.SquareCount = Convert.ToInt32(readerUser["SquareCount"]);
                    userDetails.EqualTriangleCount = Convert.ToInt32(readerUser["EqualTriangleCount"]);

                }
            }
           
            readerUser.Close();

            if (userDetails.SquareCount >= figureCatalog.SquareCount
                && userDetails.SquareCount >= figureCatalog.EqualTriangleCount) {
                Navigation.PushModalAsync(new FishFirstPage(user)); // к cборке схемы если деталей хватает
            }
            // иначе уведомление - деталей не хватает
            else
            {
                await DisplayAlert("Предупреждение", "У вас недостаточно деталей для сборки данной схемы.", "Ок");
            }
        }
       
        private async void ShowRombSchemeBtn_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Ромб.Text;
            UserDetails userDetails = new UserDetails();
            userDetails.UserId = user.Id; // передали айдишник

            string queryString = $"select * from FigureCatalog where Name = '{figureCatalog.Name}'";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            //sqlConnection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    // присваиваем объекту каталога кол-во деталей
                    figureCatalog.SquareCount = Convert.ToInt32(reader["SquareCount"]);
                    figureCatalog.EqualTriangleCount = Convert.ToInt32(reader["EqualTriangleCount"]);
                }
            }
            reader.Close();
            sqlConnection.Close();
            sqlConnection.Open();
            // ищем кол-во деталей у пользователя
            string queryStringUser = $"select * from UserDetails where UserId = {userDetails.UserId}"; //
            SqlCommand commandUser = new SqlCommand(queryStringUser, sqlConnection);
            SqlDataReader readerUser = commandUser.ExecuteReader();
            if (readerUser.HasRows)
            {
                while (readerUser.Read())
                {
                    userDetails.SquareCount = Convert.ToInt32(readerUser["SquareCount"]);
                    userDetails.EqualTriangleCount = Convert.ToInt32(readerUser["EqualTriangleCount"]);

                }
            }

            readerUser.Close();

            if (userDetails.SquareCount >= figureCatalog.SquareCount
                && userDetails.SquareCount >= figureCatalog.EqualTriangleCount)
            {
                //Navigation.PushModalAsync(new FishFirstPage()); // к борке схемы если деталей хватает
            }
            // иначе уведомление - деталей не хватает
            else
            {
                await DisplayAlert("Предупреждение", "У вас недостаточно деталей для сборки данной схемы.", "Ок");
            }
        }


        private void favFishNotSelected_Clicked(object sender, EventArgs e)
        {
            // будет список избранных фигур - туда будут заноситься все выбранные фигуры
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Рыбка.Text;
            
            // ищем строку source
            var curImageSource = favFishNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favFishNotSelected.ImageSource = "favSel.png";
                // заносим выбранный элемент в избранное
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favFishNotSelected.ImageSource = "favNoneSel.png";
                // удаляем из избранного выбранный элемент
                // сначала ищем этот элемент в списке
                var fish = FavFigures.Find(f => f.Name == "Рыбка");
                FavFigures.Remove(fish);
            }
            
        }

        private void favRombNotSelected_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Ромб.Text;


            
            var curImageSource = favRombNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favRombNotSelected.ImageSource = "favSel.png";
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favRombNotSelected.ImageSource = "favNoneSel.png";
                var romb = FavFigures.Find(f => f.Name == "Ромб");
                FavFigures.Remove(romb);
            }
        }

        private void ShowYlitkaSchemeBtn_Clicked(object sender, EventArgs e)
        {
        }

        private void favYlitkaNotSelected_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Улитка.Text;

            var curImageSource = favYlitkaNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favYlitkaNotSelected.ImageSource = "favSel.png";
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favYlitkaNotSelected.ImageSource = "favNoneSel.png";
                var ylitka = FavFigures.Find(f => f.Name == "Улитка");
                FavFigures.Remove(ylitka);
            }
        }

        private void ShowChasiSchemeBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void favChasiNotSelected_Clicked(object sender, EventArgs e)
        {

            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Часы.Text;

            var curImageSource = favChasiNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favChasiNotSelected.ImageSource = "favSel.png";
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favChasiNotSelected.ImageSource = "favNoneSel.png";
                var chasi = FavFigures.Find(f => f.Name == "Часы");
                FavFigures.Remove(chasi);
            }
        }

        private void ShowOsminogSchemeBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void favOsminogNotSelected_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Осьминог.Text;

            var curImageSource = favOsminogNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favOsminogNotSelected.ImageSource = "favSel.png";
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favOsminogNotSelected.ImageSource = "favNoneSel.png";
                var osminog = FavFigures.Find(f => f.Name == "Осьминог");
                FavFigures.Remove(osminog);
            }
        }

        private void ShowRaketaSchemeBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void favRaketaNotSelected_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Ракета.Text;

            var curImageSource = favRaketaNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favRaketaNotSelected.ImageSource = "favSel.png";
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favRaketaNotSelected.ImageSource = "favNoneSel.png";
                var raketa = FavFigures.Find(f => f.Name == "Ракета");
                FavFigures.Remove(raketa);
            }
        }

        private void ShowHouseSchemeBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void favHouseNotSelected_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Дом.Text;

            var curImageSource = favHouseNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favHouseNotSelected.ImageSource = "favSel.png";
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favHouseNotSelected.ImageSource = "favNoneSel.png";
                var house = FavFigures.Find(f => f.Name == "Дом");
                FavFigures.Remove(house);
            }
        }

        private void ShowRobotSchemeBtn_Clicked(object sender, EventArgs e)
        {

        }

        private void favRobotNotSelected_Clicked(object sender, EventArgs e)
        {
            FigureCatalog figureCatalog = new FigureCatalog();
            figureCatalog.Name = Робот.Text;

            var curImageSource = favRobotNotSelected.ImageSource.ToString();
            if (curImageSource == "File: favNoneSel.png")
            {
                favRobotNotSelected.ImageSource = "favSel.png";
                FavFigures.Add(figureCatalog);
            }
            else
            {
                favRobotNotSelected.ImageSource = "favNoneSel.png";
                var robot = FavFigures.Find(f => f.Name == "Робот");
                FavFigures.Remove(robot);
            }
        }

        private void settingsBtn_Clicked(object sender, EventArgs e)
        {
            // переход на страницу управления
            Navigation.PushModalAsync(new SettingsPage(user));
        }
    }
}