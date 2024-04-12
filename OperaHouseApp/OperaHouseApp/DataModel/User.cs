using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public abstract class User
    {
        public UserType Type { get; set; }
        public string Username { get; set; }

        protected User(UserType type, string username)
        {
            Type = type;
            Username = username;
        }

        protected User(int v, string username)
        {
            V = v;
            Username = username;
        }

        public abstract void DisplayMenu();
        public abstract void ExitApplication();
    }
}


