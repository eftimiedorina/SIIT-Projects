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

        public int SaveTicket(Ticket ticket)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                // var command = new SqlCommand("INSERT INTO Tickets (UserId, ZoneId, TotalPrice) VALUES (@UserId, @ZoneId, @TotalPrice); SELECT SCOPE_IDENTITY();", connection);
                var command = new SqlCommand("INSERT INTO Tickets (UserId, ZoneId, TotalPrice) VALUES (@UserId, @ZoneId, @TotalPrice); SELECT SCOPE_IDENTITY();", connection);
                command.Parameters.AddWithValue("@UserId", ticket.UserId);
                command.Parameters.AddWithValue("@ZoneId", ticket.ZoneId);
                command.Parameters.AddWithValue("@TotalPrice", ticket.TotalPrice);

                connection.Open();
                int ticketId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var seatNumber in ticket.SeatNumbers)
                {
                    var seatCommand = new SqlCommand("INSERT INTO TicketSeats (TicketId, SeatId) VALUES (@TicketId, (SELECT TOP 1 SeatId FROM Seats WHERE Number = @Number AND ZoneId = @ZoneId ORDER BY SeatId))", connection);
                    seatCommand.Parameters.AddWithValue("@TicketId", ticketId);
                    seatCommand.Parameters.AddWithValue("@Number", seatNumber);
                    seatCommand.Parameters.AddWithValue("@ZoneId", ticket.ZoneId);
                    seatCommand.ExecuteNonQuery();
                  /*  try
                    {
                        seatCommand.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        // Handle exception, e.g., log the error or notify the user
                        Console.WriteLine("An error occurred when inserting ticket seats: " + ex.Message);
                    }*/
                }

                return ticketId;
            }
        }

        public List<Ticket> GetAllTicketsByUser(int userId)
        {
            List<Ticket> tickets = new List<Ticket>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT TicketId, ZoneId, TotalPrice FROM Tickets WHERE UserId = @UserId",
                    connection);
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ticket = new Ticket(userId, reader.GetString(1), new List<int>(), reader.GetDecimal(2));
                        
                        tickets.Add(ticket);
                    }
                }
            }

            

            return tickets;
        }

        private List<int> GetSeatNumbersByTicket(int ticketId)
        {
            List<int> seatNumbers = new List<int>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand(
                    "SELECT SeatId FROM TicketSeats WHERE TicketId = @TicketId",
                    connection);
                command.Parameters.AddWithValue("@TicketId", ticketId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        seatNumbers.Add(reader.GetInt32(0));
                    }
                }
            }
            return seatNumbers;
        }

        public void ReturnTickets(int ticketId, int numberOfTicketsToReturn)
        {
            // Pasul 1: Identificarea locurilor care trebuie eliberate
            var seatsToFree = GetSeatsToFree(ticketId, numberOfTicketsToReturn);

            // Pasul 2: Marcarea locurilor ca fiind libere
            foreach (var seatId in seatsToFree)
            {
                FreeSeat(seatId);
            }

            // Pasul 3: Actualizarea biletului sau ștergerea acestuia, dacă toate locurile sunt returnate
            UpdateOrDeleteTicket(ticketId, seatsToFree.Count());
        }

        private void UpdateOrDeleteTicket(int ticketId, int numberOfSeatsFreed)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("DELETE FROM TicketSeats WHERE TicketId = @TicketId", connection);
                command.Parameters.AddWithValue("@TicketId", ticketId);
                command.ExecuteNonQuery();
            }
        }

        private void FreeSeat(int seatId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Seats SET IsOccupied = 0 WHERE SeatId = @SeatId", connection);
                command.Parameters.AddWithValue("@SeatId", seatId);
                command.ExecuteNonQuery();
            }
        }

        private int[] GetSeatsToFree(int ticketId, int numberOfTicketsToReturn)
        {
            int[] seatIds = new int[numberOfTicketsToReturn];
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT SeatId FROM TicketSeats WHERE TicketId = @TicketId", connection);
                command.Parameters.AddWithValue("@TicketId", ticketId);

                using (var reader = command.ExecuteReader())
                {
                    int index = 0;
                    while (reader.Read() && index < numberOfTicketsToReturn)
                    {
                        seatIds[index++] = reader.GetInt32(0);
                    }
                }
            }
            return seatIds;
        }

        public decimal GetZonePrice(string zoneId)
        {
            decimal price = 0.0m;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT Price FROM Zones WHERE ZoneId = @ZoneId", connection);
                command.Parameters.AddWithValue("@ZoneId", zoneId);

                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        price = reader.GetDecimal(0);
                    }
                }
            }
            return price;
        }

        public void UpdateZonePrice(string zoneId, decimal newPrice)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Zones SET Price = @NewPrice WHERE ZoneId = @ZoneId", connection);
                command.Parameters.AddWithValue("@NewPrice", newPrice);
                command.Parameters.AddWithValue("@ZoneId", zoneId);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateZoneSeats(string zoneId, int newSeatCount)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("UPDATE Zones SET SeatCount = @NewSeatCount WHERE ZoneId = @ZoneId", connection);
                command.Parameters.AddWithValue("@NewSeatCount", newSeatCount);
                command.Parameters.AddWithValue("@ZoneId", zoneId);
                command.ExecuteNonQuery();
            }
        }

    }

}
