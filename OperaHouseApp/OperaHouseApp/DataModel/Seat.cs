﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.DataModel
{
    public class Seat
    {
        public int SeatId { get; set; }
        public int Number {  get; set; }
        public bool IsOccupied {  get; set; }

        public Seat(int seatId, int number, bool isOccupied = false)
        {
            SeatId = seatId;
            Number = number;
            IsOccupied = isOccupied;
        }

        public void OccupySeat()
        {
            IsOccupied = true;
        }

        public void FreeSeat()
        {
            IsOccupied = false;
        }
    }
}
