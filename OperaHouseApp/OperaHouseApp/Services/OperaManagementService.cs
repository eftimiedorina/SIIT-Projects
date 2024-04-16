using OperaHouseApp.Data;
using OperaHouseApp.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
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
            Console.WriteLine("Alegeti o optiune:");
            Console.WriteLine("1. Modificari preturi");
            Console.WriteLine("2. Ajustari locuri");
            Console.Write("Introduceti optiunea: ");
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
                    Console.WriteLine("Optiune invalida, incercati din nou.");
                    ConfigureHall();
                    break;
            }
        }

        private void ModifyPrices()
        {
            Console.WriteLine("Selectati zona pentru care doriti sa modificati pretul:");
            Console.WriteLine("1. Loja");
            Console.WriteLine("2. Sala");
            Console.WriteLine("3. Galerie");
            Console.Write("Alegeti o opțiune: ");
            string priceOption = Console.ReadLine();
            string zoneId = "";
            switch (priceOption)
            {
                case "1":
                    zoneId = "A1";
                    break;
                case "2":
                    zoneId = "C1";
                    break;
                case "3":
                    zoneId = "B1";
                    break;
                default:
                    Console.WriteLine("Optiune invalida, incercati din nou.");
                    ModifyPrices();
                    return;
            }

            Console.Write($"Introduceti noul pret pentru {zoneId}: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal newPrice) && newPrice > 0)
            {
                _context.UpdateZonePrice(zoneId, newPrice);
                Console.WriteLine("Pretul a fost actualizat.");
            }
            else
            {
                Console.WriteLine("Pretul introdus este invalid. Introduceti un numar pozitiv.");
                ModifyPrices();
            }
        }

        private void AdjustSeats()
        {
            Console.WriteLine("Alegeti o zona pentru care doriti sa ajustaai numarul de locuri:");
            Console.WriteLine("1) Modificare numar locuri la loja");
            Console.WriteLine("2) Modificare numar locuri în sala");
            Console.WriteLine("3) Modificare numar locuri la galerie");
            Console.Write("Introduceai opaiunea: ");

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
                    Console.WriteLine("Optiune invalida, ancercaai din nou.");
                    AdjustSeats();
                    return;
            }

            Console.WriteLine($"Introduceai numarul de locuri pentru {zoneName}:");
            if (!int.TryParse(Console.ReadLine(), out int newSeatCount) || newSeatCount < 0)
            {
                Console.WriteLine("Numarul introdus este invalid. Introduceți un număr pozitiv.");
                AdjustSeats();
                return;
            }

            _context.UpdateZoneSeats(zoneId, newSeatCount);
            Console.WriteLine($"Numarul de locuri pentru {zoneName} a fost actualizat la {newSeatCount}.");
        }

        public void SellTickets(int currentUserId)
            {
            var zones = _context.GetZones();
            var availableZones = zones.Where(z => z.Seats.Any(s => !s.IsOccupied)).ToList();

            if (!availableZones.Any())
            {
                Console.WriteLine("Ne pare rau. Nu mai sunt locuri libere.");
                return;
            }

            Console.WriteLine("Avem locuri libere in:");
            foreach (var zone in availableZones)
            {
                Console.WriteLine($"{zone.Name} ({zone.ZoneId}): {zone.Seats.Count(s => !s.IsOccupied)} locuri libere");
            }

            string zoneId;
            Zone selectedZone;
            do
            {
                Console.WriteLine("Selectati zona in care doriti locurile:");
                zoneId = Console.ReadLine();
                selectedZone = availableZones.FirstOrDefault(z => z.ZoneId == zoneId);
                if (selectedZone == null)
                {
                    Console.WriteLine("Zona selectata nu este valida sau nu are locuri libere. Incercati din nou.");
                }
            }
            while (selectedZone == null);

            Console.WriteLine("Introduceti numarul de locuri:");
            int numSeats;
            while (!int.TryParse(Console.ReadLine(), out numSeats) || numSeats <= 0 || numSeats > selectedZone.Seats.Count(s => !s.IsOccupied))
            {
                if (numSeats > selectedZone.Seats.Count(s => !s.IsOccupied))
                {
                    Console.WriteLine($"In zona {selectedZone.Name} mai sunt doar {selectedZone.Seats.Count(s => !s.IsOccupied)} locuri libere.");
                    Console.WriteLine("Apasati orice tasta pentru a continua.");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Numarul introdus nu este valid. Incercati din nou:");
                }
            }

            var seatsToOccupy = selectedZone.Seats.Where(s => !s.IsOccupied).Take(numSeats).ToList();
            seatsToOccupy.ForEach(s => s.IsOccupied = true);

            decimal totalPrice = numSeats * selectedZone.Price;
            Console.WriteLine($"Total de plata: {totalPrice:C}. Bilete vândute cu succes pentru {selectedZone.Name}.");

            Ticket newTicket = new Ticket(currentUserId, zoneId, seatsToOccupy.Select(s => s.Number).ToList(), totalPrice);
            
            int ticketId = _context.SaveTicket(newTicket);  // Salvarea biletului în baza de date și obținerea ID-ului biletului
            Console.WriteLine($"Biletul a fost salvat cu ID-ul {ticketId}.");
        }

        public void ReturnTickets(int currentUserId)
        {
            var tickets = _context.GetAllTicketsByUser(currentUserId);

            if (tickets.Count == 0)
            {
                Console.WriteLine("Nu s-a vandut inca nici un bilet.");
                return;
            }

            Console.WriteLine("Retur bilete la:");
            foreach (var ticket in tickets)
            {
                Console.WriteLine($"{ticket.ZoneId}: {ticket.SeatNumbers.Count} locuri ocupate");
            }

            Console.Write("Introduceti ID-ul zonei pentru care doriti sa returnati bilete: ");
            string zoneId = Console.ReadLine();
            var ticketToReturn = tickets.FirstOrDefault(t => t.ZoneId == zoneId);

            if (ticketToReturn == null)
            {
                Console.WriteLine("Nu exista bilete vandute in aceasta zona sau zona introdusa este incorecta.");
                return;
            }

            Console.Write("Cate bilete returnati? ");
            if (!int.TryParse(Console.ReadLine(), out int numberOfTickets) || numberOfTickets <= 0 || numberOfTickets > ticketToReturn.SeatNumbers.Count)
            {
                Console.WriteLine($"Numar invalid de bilete. Puteai returna maximum {ticketToReturn.SeatNumbers.Count} bilete.");
                return;
            }

            // Procesul de returnare a biletelor
            _context.ReturnTickets(ticketToReturn.TicketId, numberOfTickets);

            decimal refundAmount = numberOfTickets * _context.GetZonePrice(ticketToReturn.ZoneId);
            Console.WriteLine($"Suma de returnat este: {refundAmount:C}. Biletele au fost returnate cu succes.");

            
        }

        public void ExitAndSave()
        {
            var zones = _context.GetZones();
            string filePath = "OperaHouseData.json";
            var dataToSave = new
            {
                Zones = zones.Select(z => new
                {
                    ZoneId = z.ZoneId,
                    Name = z.Name,
                    Price = z.Price,
                    Seats = z.Seats.Select(s => new
                    {
                        SeatNumber = s.Number,
                        IsOccupied = s.IsOccupied
                    }).ToList()
                }).ToList()
            };

            string json = JsonSerializer.Serialize(dataToSave, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);

            Console.WriteLine($"Datele au fost salvate în {filePath}. Aplicatia se va inchide.");
            Environment.Exit(0);
        }
    }
        
    
}
