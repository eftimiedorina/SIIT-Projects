using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class AuthenticatedUser : User
    {
        public override UserType Type => UserType.LoggedUser;

        public AuthenticatedUser (int id, string username, string password) : base (id, username, password) { }

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
