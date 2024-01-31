using System.Runtime.CompilerServices;

namespace NearestCity
{
    internal class Program
    {
        static void Main(string[] args)
        {
            City _cluj = new City("Cluj", 2, 3);
            City _huedin = new City("Huedin", 4, 8);
            City _dej = new City("Dej", 5, 2);

            City[] cities = new City[] { _cluj, _huedin, _dej };

            Console.Write("Cities: ");
            foreach (City city in cities) 
            {
                Console.Write($"{city.Name} ({city.X}, {city.Y}) ");
            }
            Console.WriteLine();

            Console.WriteLine("Choose a city from input: ");
            string _cityName = Console.ReadLine();
            City _chosenCity = null;
            foreach (City city in cities) 
            {
                if(city.Name == _cityName)
                {
                    _chosenCity = city;
                    break;
                }
            }

            double minDistance = double.MaxValue;
            string _nearestCity = null;

            foreach(City city in cities)  
            {
                if (city != _chosenCity) 
                
                {
                    double distance = CalculateDistance(_chosenCity,city);
                    Console.WriteLine($"From {_chosenCity.Name} to {city.Name}: {distance} ");

                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        _nearestCity = city.Name;
                    }
                }
            }

            Console.WriteLine($"Nearest city: {_nearestCity}");


        }

        static double CalculateDistance(City city1, City city2)
        {
            double deltaX = city2.X - city1.X;
            double deltaY = city2.Y - city1.Y;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
    }
}