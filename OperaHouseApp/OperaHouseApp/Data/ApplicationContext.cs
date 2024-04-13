using OperaHouseApp.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperaHouseApp.Data
{
    public class ApplicationContext
    {
        private readonly string _connectionString;

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        // Metodă pentru a obține toate zonele
        public List<Zone> GetZones()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT z.ZoneId, z.Name, z.Price, s.SeatId, s.Number, s.IsOccupied " +
                    "FROM Zones z LEFT JOIN Seats s ON z.ZoneId = s.ZoneId ORDER BY z.ZoneId, s.Number",
                    connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                // colectam zonele si locurile fiecarei iteratii
                var zones = new Dictionary<string, Zone>();

                while (reader.Read())
                {
                    string zoneId = reader.GetString(0);
                    if (!zones.TryGetValue(zoneId, out var zone))
                    {
                        zone = new Zone
                        {
                            ZoneId = zoneId,
                            Name = reader.GetString(1),
                            Price = reader.GetDecimal(2),
                            Seats = new List<Seat>()
                        };
                        zones[zoneId] = zone;
                    }

                    if (!reader.IsDBNull(3))  // Ensure that we have a seat record
                    {
                        zone.Seats.Add(new Seat
                        {
                            SeatId = reader.GetInt32(3),
                            Number = reader.GetInt32(4),
                            IsOccupied = reader.GetBoolean(5)
                        });
                    }
                }
                return new List<Zone>(zones.Values);
            }
        }
    }

}
