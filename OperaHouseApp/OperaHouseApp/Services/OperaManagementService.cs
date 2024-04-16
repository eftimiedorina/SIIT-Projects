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
        public void ConfigureHall()
        {
            Console.WriteLine("Alegeți o opțiune:");
            Console.WriteLine("1. Modificări prețuri");
            Console.WriteLine("2. Ajustări locuri");
            Console.Write("Introduceți opțiunea: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    ModifyPrices();
                    break;
                case "2":
                    AdjustSeats();
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă, încercați din nou.");
                    ConfigureHall();
                    break;
            }
        }

        private void ModifyPrices()
        {
            Console.WriteLine("Selectați zona pentru care doriți să modificați prețul:");
            Console.WriteLine("1. Lojă");
            Console.WriteLine("2. Sală");
            Console.WriteLine("3. Galerie");
            Console.Write("Alegeți o opțiune: ");
            string priceOption = Console.ReadLine();
            string zoneId = "";
            switch (priceOption)
            {
                case "1":
                    zoneId = "Lojă";
                    break;
                case "2":
                    zoneId = "Sală";
                    break;
                case "3":
                    zoneId = "Galerie";
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă, încercați din nou.");
                    ModifyPrices();
                    return;
            }

            Console.Write($"Introduceți noul preț pentru {zoneId}: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
            {
                _context.UpdateZonePrice(zoneId, newPrice);
                Console.WriteLine("Prețul a fost actualizat.");
            }
            else
            {
                Console.WriteLine("Prețul introdus este invalid. Introduceți un număr pozitiv.");
                ModifyPrices();
            }
        }

        private void AdjustSeats()
        {
            Console.WriteLine("Alegeți o zonă pentru care doriți să ajustați numărul de locuri:");
            Console.WriteLine("1) Modificare număr locuri la lojă");
            Console.WriteLine("2) Modificare număr locuri în sală");
            Console.WriteLine("3) Modificare număr locuri la galerie");
            Console.Write("Introduceți opțiunea: ");

            string option = Console.ReadLine();
            string zoneId = "";
            string zoneName = "";

            switch (option)
            {
                case "1":
                    zoneId = "A1";
                    zoneName = "Lodge Zone";
                    break;
                case "2":
                    zoneId = "C1";
                    zoneName = "Hall Zone";
                    break;
                case "3":
                    zoneId = "B1";
                    zoneName = "Gallery Zone";
                    break;
                default:
                    Console.WriteLine("Opțiune invalidă, încercați din nou.");
                    AdjustSeats();
                    return;
            }

            Console.WriteLine($"Introduceți numărul de locuri pentru {zoneName}:");
            if (!int.TryParse(Console.ReadLine(), out int newSeatCount) || newSeatCount < 0)
            {
                Console.WriteLine("Numărul introdus este invalid. Introduceți un număr pozitiv.");
                AdjustSeats();
                return;
            }

            _context.UpdateZoneSeats(zoneId, newSeatCount);
            Console.WriteLine($"Numărul de locuri pentru {zoneName} a fost actualizat la {newSeatCount}.");
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

            
        }
    }
        
    
}
