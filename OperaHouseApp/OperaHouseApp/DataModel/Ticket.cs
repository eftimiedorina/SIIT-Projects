﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class Ticket
    {
        public int UserId { get; set; }
        public string ZoneId {  get; set; }
        public List<int> SeatNumbers { get; set; }
        public decimal TotalPrice { get; set; }

        public Ticket(int userID, string zoneId, List<int> seatNumbers, decimal totalPrice)
        {
            UserId = userID;
            ZoneId = zoneId;
            SeatNumbers = seatNumbers;
            TotalPrice = totalPrice;
        }

        // Metoda pentru adăugarea unui loc la bilet, în cazul în care se cumpără locuri pe rând
        public void AddSeat(int seatNumber)
        {
            SeatNumbers.Add(seatNumber);
        }
    }
}
