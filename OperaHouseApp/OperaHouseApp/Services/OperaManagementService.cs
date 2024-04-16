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

        public void SellTickets(int currentUserId)
        {
            var zones = _context.GetZones();
            var availableZones = zones.Where(z => z.Seats.Any(s => !s.IsOccupied)).ToList();

            if (!availableZones.Any())
            {
                Console.WriteLine("Ne pare rău. Nu mai sunt locuri libere.");
                return;
            }

            Console.WriteLine("Avem locuri libere în:");
            foreach (var zone in availableZones)
            {
                Console.WriteLine($"{zone.Name} ({zone.ZoneId}): {zone.Seats.Count(s => !s.IsOccupied)} locuri libere");
            }

            string zoneId;
            Zone selectedZone;
            do
            {
                Console.WriteLine("Selectați zona în care doriți locurile:");
                zoneId = Console.ReadLine();
                selectedZone = availableZones.FirstOrDefault(z => z.ZoneId == zoneId);
                if (selectedZone == null)
                {
                    Console.WriteLine("Zona selectată nu este validă sau nu are locuri libere. Încercați din nou.");
                }
            }
            while (selectedZone == null);

            Console.WriteLine("Introduceți numărul de locuri:");
            int numSeats;
            while (!int.TryParse(Console.ReadLine(), out numSeats) || numSeats <= 0 || numSeats > selectedZone.Seats.Count(s => !s.IsOccupied))
            {
                if (numSeats > selectedZone.Seats.Count(s => !s.IsOccupied))
                {
                    Console.WriteLine($"În zona {selectedZone.Name} mai sunt doar {selectedZone.Seats.Count(s => !s.IsOccupied)} locuri libere.");
                    Console.WriteLine("Apăsați orice tastă pentru a continua.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Numărul introdus nu este valid. Încercați din nou:");
                }
            }

            var seatsToOccupy = selectedZone.Seats.Where(s => !s.IsOccupied).Take(numSeats).ToList();
            seatsToOccupy.ForEach(s => s.IsOccupied = true);

            decimal totalPrice = numSeats * selectedZone.Price;
            Console.WriteLine($"Total de plată: {totalPrice:C}. Bilete vândute cu succes pentru {selectedZone.Name}.");

            Ticket newTicket = new Ticket(currentUserId, zoneId, seatsToOccupy.Select(s => s.Number).ToList(), totalPrice);
            
            int ticketId = _context.SaveTicket(newTicket);  // Salvarea biletului în baza de date și obținerea ID-ului biletului
            Console.WriteLine($"Biletul a fost salvat cu ID-ul {ticketId}.");
        }

        public void ReturnTickets(int currentUserId)
        {
            var tickets = _context.GetAllTicketsByUser(currentUserId);

            if (tickets.Count == 0)
            {
                Console.WriteLine("Nu s-a vândut încă nici un bilet.");
                return;
            }

            Console.WriteLine("Retur bilete la:");
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"{ticket.ZoneId}: {ticket.SeatNumbers.Count} locuri ocupate");
            }

            Console.Write("Introduceți ID-ul zonei pentru care doriți să returnați bilete: ");
            string zoneId = Console.ReadLine();
            var ticketToReturn = tickets.FirstOrDefault(t => t.ZoneId == zoneId);

            if (ticketToReturn == null)
            {
                Console.WriteLine("Nu există bilete vândute în această zonă sau zona introdusă este incorectă.");
                return;
            }

            Console.Write("Câte bilete returnați? ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfTickets) || numberOfTickets <= 0 || numberOfTickets > ticketToReturn.SeatNumbers.Count)
            {
                Console.WriteLine($"Număr invalid de bilete. Puteți returna maximum {ticketToReturn.SeatNumbers.Count} bilete.");
                return;
            }

            // Procesul de returnare a biletelor
            _context.ReturnTickets(ticketToReturn.TicketId, numberOfTickets);

            decimal refundAmount = numberOfTickets * _context.GetZonePrice(ticketToReturn.ZoneId);
            Console.WriteLine($"Suma de returnat este: {refundAmount:C}. Biletele au fost returnate cu succes.");

            // Actualizarea bazei de date
            // Presupunând că există o metodă în ApplicationContext care să gestioneze logica de returnare
        }
    }
        
    
}
