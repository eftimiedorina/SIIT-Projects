using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class Zone
    {
        public string ZoneId { get; set; }
        public string Name { get; set; } // lodge, hall, gallery
        public decimal Price {  get; set; }
        public List<Seat> Seats { get; set; }

        public Zone(string zoneId, string name, decimal price, List<Seat> seats = null)
        {
            ZoneId = zoneId;
            Name = name;
            Price = price;
            Seats = seats ?? new List<Seat>(); // Inițializează cu lista furnizată sau cu o listă nouă dacă nu este furnizată
        }

        // Metoda pentru a afla numărul de locuri libere
        public int AvailableSeats => Seats.Count(seat => !seat.IsOccupied);

        // Metoda pentru a ocupa un loc specific
        public bool TryOccupySeat(int seatNumber)
        {
            var seat = Seats.FirstOrDefault(s => s.Number == seatNumber && !s.IsOccupied);
            if (seat != null)
            {
                seat.OccupySeat();
                return true;
            }
            return false;
        }

        // Metoda pentru a elibera un loc specific
        public bool TryFreeSeat(int seatNumber)
        {
            var seat = Seats.FirstOrDefault(s => s.Number == seatNumber && s.IsOccupied);
            if (seat != null)
            {
                seat.FreeSeat();
                return true;
            }
            return false;
        }

        public void UpdatePrice(decimal newPrice)
        {
            Price = newPrice;
        }
    }
}
