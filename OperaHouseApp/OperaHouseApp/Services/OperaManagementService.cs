using OperaHouseApp.Data;
using OperaHouseApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.Services
{
    public class OperaManagementService
    {
        private ApplicationContext _context;

        public OperaManagementService(ApplicationContext context)
        {
            _context = context;
        }

        // Metoda pentru vânzarea biletelor
      /*  public Ticket SellTickets(string zoneId, List<int> seatNumbers, int userId) 
        {
            
        }*/

        // Metoda pentru afișarea situației locurilor libere
        public void DisplayAvailableSeats()
        {
            var zones = _context.GetZones();
            Console.WriteLine("Available Seats in Each Zone:");

            foreach (var zone in zones)
            {
                // Calculăm numărul de locuri libere
                int availableSeats = zone.Seats.FindAll(seat => !seat.IsOccupied).Count;
                Console.WriteLine($"{zone.Name} (Zone ID: {zone.ZoneId}): {availableSeats} seats available");
            }
        }

        // Metoda pentru configurarea sălii (de exemplu, ajustarea prețurilor)
        public void ConfigureZone(string zoneId, decimal newPrice)
        {
          
        }
    }
}
