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
	public partial class SettingsPage : ContentPage
	{
        SqlConnection sqlConnection { get; set; }
        Users User { get; set; }
        public SettingsPage (Models.Users user)
		{
			InitializeComponent ();

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

            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            // нужно прочитать имя пользователя
            string queryString = $"select Username from dbo.Users where ID = '{user.Id}'";
            SqlCommand command1 = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.Username = reader["Username"].ToString();
                }
            }

            UsernameField.Text = user.Username;
            User = user;
            reader.Close();
            sqlConnection.Close();
		}

        private void BackToMainBtn_Clicked(object sender, EventArgs e)
        {
           
            Navigation.PopModalAsync(); // на главную
        }

        private void ExitFromAccountBtn_Clicked(object sender, EventArgs e)
        {
			// на авторизацию

			Navigation.PushModalAsync(new FirstPage());
        }

        private async void DeleteAccountBtn_Clicked(object sender, EventArgs e)
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            int IdUser = User.Id;
            string queryDel = $"delete from Favorites where UserId = {IdUser}\r\n" +
                $"delete from UserDetails where UserId = {IdUser}\r\n" +
                $"delete from Users where ID = {IdUser}";
            using (SqlCommand command1 = new SqlCommand(queryDel, 
                sqlConnection))
            {
                command1.ExecuteNonQuery();
            }

            sqlConnection.Close();

            await DisplayAlert("Успешно", "Удаление пользователя прошло успешно.", "Ок");
            Navigation.PushModalAsync(new FirstPage());

        }
    }
}