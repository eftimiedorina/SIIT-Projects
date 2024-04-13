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
        public Ticket SellTickets(string zoneId, List<int> seatNumbers, int userId) 
        {
            Zone zone = _context.GetZone(zoneId);
            if (zone == null || !zone.Seats.Any(s => seatNumbers.Contains(s.Number) && !s.IsOccupied))
            {
                Console.WriteLine("Some of the seats are either occupied or do not exist.");
                return null;
            }

            // Ocupă locurile
            foreach (int seatNumber in seatNumbers)
            {
                zone.TryOccupySeat(seatNumber);
            }

            decimal totalPrice = seatNumbers.Count * zone.Price;
            Ticket ticket = new Ticket(zoneId, seatNumbers, totalPrice);

            _context.SaveTicket(ticket);
            return ticket;
        }

        // Metoda pentru afișarea situației locurilor libere
        public void DisplayAvailableSeats()
        {
            var zones = _context.GetZones();
            foreach (var zone in zones)
            {
                Console.WriteLine($"{zone.ZoneId}: {zone.AvailableSeats} seats available");
            }
        }

        // Metoda pentru configurarea sălii (de exemplu, ajustarea prețurilor)
        public void ConfigureZone(string zoneId, decimal newPrice)
        {
            Zone zone = _context.GetZone(zoneId);
            if (zone != null)
            {
                zone.Price = newPrice;
                _context.UpdateZone(zone);
                Console.WriteLine("Zone price updated successfully.");
            }
            else
            {
                Console.WriteLine("Zone not found.");
            }
        }
    }
}
