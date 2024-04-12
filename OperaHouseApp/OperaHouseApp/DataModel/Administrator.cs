using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class Administrator : User
    {
        public Administrator(UserType userType, string username) : base(2,username) { }

        public override void DisplayMenu()
        {
            Console.WriteLine($"Bine ai venit, Administrator {Username}!");
            Console.WriteLine("1. Afișează situație locuri libere");
            Console.WriteLine("2. Configurează sala");
            Console.WriteLine("3. Ajustează prețurile");
            Console.WriteLine("4. Modifică numărul de locuri");
            // Adaugă aici logica pentru a permite administratorului să selecteze o opțiune
        }

        public override void ExitApplication()
        {
            throw new NotImplementedException();
        }

        // Metode specifice pentru administrare
        public void ConfigureHall()
        {
            // Logica pentru configurarea sălii
        }

        public void AdjustPrices()
        {
            // Logica pentru ajustarea prețurilor
        }

        public void ModifySeatNumbers()
        {
            // Logica pentru modificarea numărului de locuri
        }
    }
}
