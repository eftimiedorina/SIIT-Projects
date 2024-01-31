using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NearestCity
{
    class City
    {
        private string _name;
        private double _x;
        private double _y;

        public City(string name, double x, double y)
        {
            Name = name;
            X = x;
            Y = y;
        }

        public string Name { get => _name; set => _name = value; }
        public double X { get => _x; set => _x = value; }
        public double Y { get => _y; set => _y = value; }

        
    }
}
