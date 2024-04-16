using OperaHouseApp.Data;
using OperaHouseApp.Services;

namespace OperaHouseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS01;Database=OperaHouseManagement;Integrated Security=SSPI";
            ApplicationContext context = new ApplicationContext(connectionString);
            OperaManagementService managementService = new OperaManagementService(context);

            
            managementService.DisplayAvailableSeats();
        }
    }
}