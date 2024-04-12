using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class LoggedUser : User
    {
        public LoggedUser (string username) : base (1, username) { }

        public override void DisplayMenu()
        {
            Console.WriteLine($"Bine ai venit, {Username}!");
            Console.WriteLine("1. Afișează situație locuri libere");
            Console.WriteLine("2. Cumpără bilete");
            Console.WriteLine("3. Returnează bilete");
            // Adaugă aici logica pentru a permite utilizatorului să selecteze o opțiune
        }

        public override void ExitApplication()
        {
            throw new NotImplementedException();
        }
    }
}
