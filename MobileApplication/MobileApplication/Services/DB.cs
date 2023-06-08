using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using MobileApplication.Models;

namespace MobileApplication.Services
{
    class DB
    {
        // подключение с помощью этого объекта
        readonly SQLiteAsyncConnection db;

        // конструктор со строкой подключения
        public DB(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);
            // создаем таблицу с типом объектов - User
            db.CreateTableAsync<Users>().Wait();
            
        }
        public int SaveUser(Users user)
        {
            return db.InsertAsync(user).Result;
        }

    }
}
