using MobileApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApplication
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
        Label label;
        SqlConnection sqlConnection { get; set; }

        Users user { get; set; }
        public RegistrationPage()
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
            // открываем это соединение
            if (sqlConnection.State == System.Data.ConnectionState.Closed) {
                sqlConnection.Open();
            }
        }

        private void NextToDetailsBtn_Clicked(object sender, EventArgs e)
        {
            user = new Users();
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            // добавление данных в базу данных POST запрос
            user.Username = UsernameFieldReg.Text;
            user.Login_ = LoginFieldReg.Text;
            user.Password_ = PasswordFieldReg.Text;

            using (SqlCommand command1 = new SqlCommand("insert into dbo.Users values(@Username," +
                " @Login_, @Password_)", sqlConnection))
            {
                command1.Parameters.Add(new SqlParameter("Username", user.Username));
                command1.Parameters.Add(new SqlParameter("Login_", user.Login_));
                command1.Parameters.Add(new SqlParameter("Password_", user.Password_));
                command1.ExecuteNonQuery();
            }
            sqlConnection.Close();
            sqlConnection.Open();
            string queryString = $"select ID from dbo.Users where Login_ = '{user.Login_}'" +
                $" and Password_ = '{user.Password_}'";
            SqlCommand command2 = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command2.ExecuteReader();
            // считываем добавленного пользователя
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.Id = (int)reader["ID"];
                }
            }
            reader.Close();
            // навигация к заполнению кол-ва деталей
            if (user.Id != 0)
            {
                Navigation.PushModalAsync(new RegistrationPageDetails(user));
            }
        }

        private void NavigateToAuthorizationBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new FirstPage());
        }
    }
}