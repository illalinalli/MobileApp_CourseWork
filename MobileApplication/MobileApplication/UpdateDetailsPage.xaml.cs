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
    public partial class UpdateDetailsPage : ContentPage
    {
       UserDetails UserDetails { get; set; }
        SqlConnection sqlConnection { get; set; }
        //Users users = new Users();
        public UpdateDetailsPage(Models.UserDetails userDetails)
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
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
            

            ChangeDoubleSquare.Placeholder = userDetails.DoubleSquareCount.ToString() + " шт.";
            TrapeziumCount.Placeholder = userDetails.TrapeziumCount.ToString() + " шт.";
            HexagonCount.Placeholder = userDetails.HexagonCount.ToString() + " шт.";
            PentagonCount.Placeholder = userDetails.RentagonCount.ToString() + " шт.";
            EqualTriangleCount.Placeholder = userDetails.EqualTriangleCount.ToString() + " шт.";
            ParallelogramCount.Placeholder = userDetails.ParallelogramCount.ToString() + " шт.";
            SquareCount.Placeholder = userDetails.SquareCount.ToString() + " шт.";
            IsoscelesTriangleCount.Placeholder = userDetails.IsoscelesTriangleCount.ToString() + " шт.";

            UserDetails = userDetails;

  
        }

        private void ToMineDetailsBtn_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync(); // to mine
        }

        private void SaveChangesBtn_Clicked(object sender, EventArgs e)
        {
            // UPDATE CHANGES
            if (sqlConnection.State == System.Data.ConnectionState.Closed)
            {
                sqlConnection.Open();
            }

            int chDoubleSquare;
            int chTrapeziumCount;
            int chHexagonCount;
            int chPentagonCount;
            int chEqualTriangleCount;
            int chParallelogramCount;
            int chSquareCount;
            int chIsoscelesTriangleCount;

            if (ChangeDoubleSquare.Text == null)
                chDoubleSquare = UserDetails.DoubleSquareCount; 
            else 
                chDoubleSquare = Convert.ToInt32(ChangeDoubleSquare.Text);
            
            if (TrapeziumCount.Text == null)
                chTrapeziumCount = UserDetails.TrapeziumCount;
            else 
                chTrapeziumCount = Convert.ToInt32(TrapeziumCount.Text);
            
            if (HexagonCount.Text == null) 
                chHexagonCount = UserDetails.HexagonCount;
            else 
                chHexagonCount = Convert.ToInt32(HexagonCount.Text);

            if (PentagonCount.Text == null) 
                chPentagonCount = UserDetails.RentagonCount;
            else 
                chPentagonCount = Convert.ToInt32(PentagonCount.Text);

            if (EqualTriangleCount.Text == null) 
                chEqualTriangleCount = UserDetails.EqualTriangleCount;
            else 
                chEqualTriangleCount = Convert.ToInt32(EqualTriangleCount.Text);

            if (ParallelogramCount.Text == null) 
                chParallelogramCount = UserDetails.ParallelogramCount;
            else
                chParallelogramCount = Convert.ToInt32(ParallelogramCount.Text);

            if (SquareCount.Text == null) 
                chSquareCount = UserDetails.SquareCount;
            else
                chSquareCount = Convert.ToInt32(SquareCount.Text);

            if (IsoscelesTriangleCount.Text == null) 
                chIsoscelesTriangleCount = UserDetails.IsoscelesTriangleCount;
            else
                chIsoscelesTriangleCount = Convert.ToInt32(IsoscelesTriangleCount.Text);

            string queryString = $"UPDATE dbo.UserDetails SET DoubleSquareCount = '{chDoubleSquare}', " +
                $"TrapeziumCount = '{chTrapeziumCount}'," +
                $" HexagonCount = '{chHexagonCount}', RentagonCount = '{chPentagonCount}'," +
                $" EqualTriangleCount = '{chEqualTriangleCount}', ParallelogramCount = '{chParallelogramCount}'," +
                $" SquareCount = '{chSquareCount}', IsoscelesTriangleCount = '{chIsoscelesTriangleCount}'" +
                $" WHERE UserId = '{UserDetails.UserId}'";

            using (SqlCommand command2 = new SqlCommand(queryString, sqlConnection))
            {
                command2.ExecuteNonQuery();
            }

            UserDetails.DoubleSquareCount = chDoubleSquare;
            UserDetails.TrapeziumCount = chTrapeziumCount;
            UserDetails.HexagonCount = chHexagonCount;
            UserDetails.RentagonCount = chPentagonCount;
            UserDetails.EqualTriangleCount = chEqualTriangleCount;
            UserDetails.ParallelogramCount = chParallelogramCount;
            UserDetails.SquareCount = chSquareCount;
            UserDetails.IsoscelesTriangleCount = chIsoscelesTriangleCount;

            if (sqlConnection.State == System.Data.ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            if (UserDetails.UserId != 0)
            {
                Navigation.PushModalAsync(new MineDetailsPage(UserDetails));
            }
        }
    }
}