using OperaHouseApp.DataModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
                        zone = new Zone(zoneId, reader.GetString(1), reader.GetDecimal(2), new List<Seat>());
                        zones[zoneId] = zone;
                    }

                    if (!reader.IsDBNull(3))  // Ensure that we have a seat record
                    {
                        zone.Seats.Add(new Seat(reader.GetInt32(3), reader.GetInt32(4), reader.GetBoolean(5)));
                    }
                }
                reader.Close();
                return new List<Zone>(zones.Values);
            }
        }

        // Metodă pentru a obține un utilizator după username
        public User GetUserByUsername(string username)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT UserId, Username, PasswordHash, UserType FROM Users WHERE Username = @username", connection);
                command.Parameters.AddWithValue("@username", username);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int userType = reader.GetInt32(3);
                    int userId = reader.GetInt32(0);
                    string userName = reader.GetString(1);
                    string password = reader.GetString(2);
                    switch (userType)
                    {
                        case 1:
                            return new Visitor(reader.GetInt32(0),"Guest","");
                        case 2:
                            return new AuthenticatedUser(userId,userName,password); 
                        case 3:
                            return new Administrator(userId,userName,password);
                        default:
                            throw new Exception("Unknown user type.");
                    }
                }
            }
            return null;
        }

        public void UpdateSeat(Seat seat)
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE Seats SET IsOccupied = @IsOccupied WHERE SeatId = @SeatId", connection);
                command.Parameters.AddWithValue("@IsOccupied", seat.IsOccupied);
                command.Parameters.AddWithValue("@SeatId", seat.SeatId);

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void SaveTicket(Ticket ticket)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO Tickets (UserId, ZoneId, TotalPrice) VALUES (@UserId, @ZoneId, @TotalPrice); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@UserId", ticket.UserId);
                command.Parameters.AddWithValue("@ZoneId", ticket.ZoneId);
                command.Parameters.AddWithValue("@TotalPrice", ticket.TotalPrice);

                connection.Open();
                int ticketId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var seatNumber in ticket.SeatNumbers)
                {
                    var seatCommand = new SqlCommand("INSERT INTO TicketSeats (TicketId, SeatId) VALUES (@TicketId, (SELECT SeatId FROM Seats WHERE Number = @Number AND ZoneId = @ZoneId))", connection);
                    seatCommand.Parameters.AddWithValue("@TicketId", ticketId);
                    seatCommand.Parameters.AddWithValue("@Number", seatNumber);
                    seatCommand.Parameters.AddWithValue("@ZoneId", ticket.ZoneId);
                    seatCommand.ExecuteNonQuery();
                }
            }
        }
        
    }

}
