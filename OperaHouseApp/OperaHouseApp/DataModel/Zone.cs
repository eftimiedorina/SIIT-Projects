using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class Zone
    {
        public string Name { get; set; } // lodge, hall, gallery
        public int TotalSeats { get; set; }
        public decimal Price {  get; set; }
        public List<Seat> Seats { get; set; }

        public Zone(string name, int totalSeats, decimal price)
        {
            this.Name = name;
            this.TotalSeats = totalSeats;
            this.Price = price;
            Seats = Enumerable.Range(1, totalSeats).Select(number => new Seat(number)).ToList();
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
    }
}
