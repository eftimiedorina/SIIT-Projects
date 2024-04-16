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

        public bool SellTickets(string zoneId, List<int> seatNumbers, int userId)
        {
            // Obține zona după ID
            var zone = _context.GetZones().FirstOrDefault(z => z.ZoneId == zoneId);
            if (zone == null)
            {
                Console.WriteLine("Zone not found.");
                return false;
            }

            // Verifică dacă toate locurile solicitate sunt libere
            var availableSeats = zone.Seats.Where(seat => !seat.IsOccupied && seatNumbers.Contains(seat.Number)).ToList();
            if (availableSeats.Count != seatNumbers.Count)
            {
                Console.WriteLine("Some of the requested seats are not available.");
                return false;
            }

            // Ocupă locurile
            foreach (var seat in availableSeats)
            {
                seat.IsOccupied = true;
                _context.UpdateSeat(seat);  // Presupunem existența acestei metode în ApplicationContext
            }

            // Calculează prețul total
            decimal totalPrice = availableSeats.Count * zone.Price;

            // Crează biletul
            Ticket ticket = new Ticket(userId, zoneId, availableSeats.Select(s => s.Number).ToList(), totalPrice);
            
            // Salvează biletul în baza de date
            _context.SaveTicket(ticket); 

            Console.WriteLine("Ticket sold successfully.");
            return true;
        }
        
    }
}
