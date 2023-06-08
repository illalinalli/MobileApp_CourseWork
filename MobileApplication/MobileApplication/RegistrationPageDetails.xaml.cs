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
    public partial class RegistrationPageDetails : ContentPage
    {
        SqlConnection sqlConnection;
        UserDetails userDetails { get; set; }
        Users user { get; set; }
        public RegistrationPageDetails(Users user)
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

        private void NavigateToMainBtn_Clicked(object sender, EventArgs e)
        {
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            string queryString = $"select ID from dbo.Users where Login_ = '{user.Login_}' and Password_ = '{user.Password_}'";
            SqlCommand command1 = new SqlCommand(queryString, sqlConnection);
            SqlDataReader reader = command1.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    user.Id = (int)reader["ID"];
                }
            }
            userDetails = new UserDetails();
            userDetails.UserId = user.Id;
            userDetails.SquareCount = Convert.ToInt32(SquareFieldReg.Text);
            userDetails.EqualTriangleCount = Convert.ToInt32(EqualTriangleFieldReg.Text);
            userDetails.ParallelogramCount = Convert.ToInt32(ParallelogramFieldReg.Text);
            userDetails.TrapeziumCount = Convert.ToInt32(TrapeziumFieldReg.Text);
            userDetails.RentagonCount = Convert.ToInt32(RentagonFieldReg.Text);
            userDetails.IsoscelesTriangleCount = Convert.ToInt32(IsoscelesTriangleFieldReg.Text);
            userDetails.HexagonCount = Convert.ToInt32(HexagonFieldReg.Text);
            userDetails.DoubleSquareCount = Convert.ToInt32(DoubleSquareFieldReg.Text);

            using (SqlCommand command2 = new SqlCommand(
                "insert into dbo.UserDetails values(@UserId,@SquareCount," +
                "@EqualTriangleCount,@ParallelogramCount,@TrapeziumCount,@RentagonCount," +
                "@IsoscelesTriangleCount,@HexagonCount,@DoubleSquareCount)", sqlConnection))
            {
                command2.Parameters.Add(new SqlParameter("UserId", userDetails.UserId));
                command2.Parameters.Add(new SqlParameter("SquareCount", userDetails.SquareCount));
                command2.Parameters.Add(new SqlParameter("EqualTriangleCount", userDetails.EqualTriangleCount));
                command2.Parameters.Add(new SqlParameter("ParallelogramCount", userDetails.ParallelogramCount));
                command2.Parameters.Add(new SqlParameter("TrapeziumCount", userDetails.TrapeziumCount));
                command2.Parameters.Add(new SqlParameter("RentagonCount", userDetails.RentagonCount));
                command2.Parameters.Add(new SqlParameter("IsoscelesTriangleCount", userDetails.IsoscelesTriangleCount));
                command2.Parameters.Add(new SqlParameter("HexagonCount", userDetails.HexagonCount));
                command2.Parameters.Add(new SqlParameter("DoubleSquareCount", userDetails.DoubleSquareCount));
                reader.Close();
                command2.ExecuteNonQuery();
            }
            reader.Close();
            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            if (userDetails.UserId != 0)
            {
                Navigation.PushModalAsync(new MainAppPage(user));
            }
        }
    }
}