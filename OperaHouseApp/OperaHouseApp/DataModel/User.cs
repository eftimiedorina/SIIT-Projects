using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public abstract class User
    {
        public int Id { get; set; }
        public abstract UserType Type { get; }
        public string Username { get; set; }
        public string Password { get; set; }

        protected User(int id, string username,string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }


        public abstract void DisplayMenu();
        public abstract void ExitApplication();
    }
}


