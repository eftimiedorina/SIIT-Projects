using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class Visitor : User
    {
        public Visitor(UserType usertype, string username) : base(0, "Guest") { }

        public override void DisplayMenu()
        {
            Console.WriteLine("Bine ai venit, Vizitator!");
            Console.WriteLine("1. Afișează situație locuri libere");
            // Adaugă aici logica pentru a permite vizitatorului să selecteze opțiunea de a vedea locurile libere
        }

        public override void ExitApplication()
        {
            throw new NotImplementedException();
        }
    }
}
