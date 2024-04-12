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
    }
}
