using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace MobileApplication.Models
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; } // имя + фамилия
        public string Login_ { get; set; }
        public string Password_ { get; set; }

        //public Users() { }
    }
}
