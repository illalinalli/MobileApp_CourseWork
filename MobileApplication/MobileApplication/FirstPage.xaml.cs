using MobileApplication.Models;
using MobileApplication.Views;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace MobileApplication
{
    
    [XamlCompilation(XamlCompilationOptions.Compile)]
   
    public partial class FirstPage : ContentPage
    {
        


        Entry Login = new Entry();
        Entry Password = new Entry();
        Button LoginButton = new Button();
        Button RegistrationButton = new Button();

        SqlConnection sqlConnection { get; set; }
        public FirstPage()
        {
            InitializeComponent();
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

            // Открываем это соединение
            if (sqlConnection.State == System.Data.ConnectionState.Closed) {
                sqlConnection.Open();
            }
            
        }
        // GET запрос
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            Users user = new Users();
            
            user.Login_ = LoginField.Text;
            user.Password_ = PasswordField.Text;


            string queryString = $"select ID from dbo.Users where " +
                $"Login_ = '{user.Login_}' and Password_ = '{user.Password_}'";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();
            var mainPage = new MainAppPage();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.Id = (int)reader["ID"];
                }
                
            }
            reader.Close();
            sqlConnection.Close();

            if (user.Id != 0)
            {
                Navigation.PushModalAsync(new MainAppPage(user));
            }
            else
            {
                await DisplayAlert("Ошибка", "Неверно введен логин или пароль. Попробуйте ещё раз.", "Ок");
            }
        }

        private void RegistrationButton_Clicked(object sender, EventArgs e)
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            // при нажатии на кнопку - она перекрашивается в другой цвет
            RegistrationBtn.TextColor = Color.White;
            RegistrationBtn.BackgroundColor = Color.FromHex("#025464");
            RegistrationBtn.CornerRadius = 50;
            
            // для примера, перенаправим пользователя на другую страницу
            Navigation.PushModalAsync(new RegistrationPage());
        }

    }
}