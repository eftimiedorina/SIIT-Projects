using OperaHouseApp.Data;
using OperaHouseApp.Services;

namespace OperaHouseApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string connectionString = @"Server=DORINA-LAPTOP\SQLEXPRESS01;Database=OperaHouseManagement;User Id=DORINA-LAPTOP\Dorina;Password='';";
            ApplicationContext context = new ApplicationContext(connectionString);
            OperaManagementService managementService = new OperaManagementService(context);

            
            managementService.DisplayAvailableSeats();
        }
    }
}