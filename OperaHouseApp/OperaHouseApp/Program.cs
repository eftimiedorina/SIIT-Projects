using OperaHouseApp.Authentication;
using OperaHouseApp.Data;
using OperaHouseApp.DataModel;
using OperaHouseApp.Services;

namespace OperaHouseApp
{
    internal class Program
    {
        static string connectionString = @"Data Source=localhost\SQLEXPRESS01;Database=OperaHouseManagement;Integrated Security=SSPI";
        static ApplicationContext context = new ApplicationContext(connectionString);
        static OperaManagementService managementService = new OperaManagementService(context);
        static AuthenticationService authenticationService = new AuthenticationService(context);
        static User currentUser;

        static void Main(string[] args)
        {

            Console.WriteLine("Bine ati venit la sistemul de gestionare a salii de opera!");
            ShowVisitorMenu();
        }

        static void ShowVisitorMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMeniu Vizitator:");
                Console.WriteLine("1. Afiseaza situatia locurilor libere");
                Console.WriteLine("2. Autentificare");
                Console.WriteLine("3. Iesire");

                Console.Write("Alegeti o optiune: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        managementService.DisplayAvailableSeats();
                        break;
                    case "2":
                        AuthenticateUser();
                        break;
                    case "3":
                        Console.WriteLine("La revedere!");
                        return;
                    default:
                        Console.WriteLine("Optiune invalida, incercati din nou.");
                        break;
                }
            }
        }

        static void AuthenticateUser()
        {
            Console.Write("Nume utilizator: ");
            string username = Console.ReadLine();
            Console.Write("Parola: ");
            string password = Console.ReadLine();

            currentUser = authenticationService.Authenticate(username, password);
            if (currentUser != null)
            {
                Console.WriteLine($"Bine ati venit, {currentUser.Username}!");
                ShowUserMenu();
            }
            else
            {
                Console.WriteLine("Autentificare esuata. Incercati din nou.");
                ShowVisitorMenu();
            }
        }

        static void ShowUserMenu()
        {
            while (true)
            {
                Console.WriteLine("\nMeniu Utilizator:");
                Console.WriteLine("1. Vanzare bilete");
                Console.WriteLine("2. Returnare bilete");
                Console.WriteLine("3. Afiseaza situatia locurilor libere");
                
                if (currentUser.Type == UserType.Administrator)
                {
                    Console.WriteLine("4. Configureaza sala");
                }
                Console.WriteLine("5. Deconectare");

                Console.Write("Alegeti o optiune: ");
                string option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        managementService.SellTickets(currentUser.Id);
                        break;
                    case "2":
                        managementService.ReturnTickets(currentUser.Id);
                        break;
                    case "3":
                        managementService.DisplayAvailableSeats();
                        break;
                    case "4":
                        if (currentUser.Type == UserType.Administrator)
                        {
                            managementService.ConfigureHall();
                        }
                        else
                        {
                            Console.WriteLine("Acces neautorizat.");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Deconectare reușita.");
                        currentUser = null; // Reset currentUser to simulate logout
                        ShowVisitorMenu();
                        break;
                    default:
                        Console.WriteLine("Optiune invalida, incercati din nou.");
                        break;
                }
            }



        }
    }
}