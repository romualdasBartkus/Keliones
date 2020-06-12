using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Keliones
{
    public static class DbContext
    {
        public static void InsertUser(User user)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            SQLiteCommand command = con.CreateCommand();

            con.Open();
            command.CommandText = "INSERT INTO User(name, last_name, email, phone, country)" +
                                  "VALUES (@name, @last_name, @email, @phone, @country)";
            command.Parameters.AddWithValue("@name", user.Name);
            command.Parameters.AddWithValue("@last_name", user.LastName);
            command.Parameters.AddWithValue("@email", user.Email);
            command.Parameters.AddWithValue("@phone", user.Phone);
            command.Parameters.AddWithValue("@country", user.UserCountry);
            command.Prepare();
            command.ExecuteNonQuery();
            con.Close();
        }

        public static void InsertTrip(Trip trip)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            SQLiteCommand command = con.CreateCommand();

            con.Open();
            command.CommandText = "INSERT INTO Trip(name, start_date, end_date, country, rating, price)" +
                                  "VALUES (@name, @start_date, @end_date, @country, @rating, @price)";
            command.Parameters.AddWithValue("@name", trip.Name);
            command.Parameters.AddWithValue("@start_date", trip.StartDate);
            command.Parameters.AddWithValue("@end_date", trip.EndDate);
            command.Parameters.AddWithValue("@country", trip.Country);
            command.Parameters.AddWithValue("@rating", trip.Rating);
            command.Parameters.AddWithValue("@price", trip.Price);
            command.Prepare();
            command.ExecuteNonQuery();
            con.Close();
        }

        public static void InsertOrder(int userID, int tripID)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            SQLiteCommand command = con.CreateCommand();

            con.Open();
            command.CommandText = "INSERT INTO Trip_order(user_id, trip_id)" +
                                  "VALUES (@user_id, @trip_id)";
            command.Parameters.AddWithValue("@user_id", userID);
            command.Parameters.AddWithValue("@trip_id", tripID);
            command.Prepare();
            command.ExecuteNonQuery();
            con.Close();
        }

        public static void InsertCity(string name, int tripID)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            SQLiteCommand command = con.CreateCommand();

            con.Open();
            command.CommandText = "INSERT INTO City(trip_id, name)" +
                                  "VALUES (@trip_id, @name)";
            command.Parameters.AddWithValue("@trip_id", tripID);
            command.Parameters.AddWithValue("@name", name);
            command.Prepare();
            command.ExecuteNonQuery();
            con.Close();
        }

        public static void DeleteTrip(int tripID)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            con.Open();
            string sql = $"DELETE FROM Trip WHERE trip_id = {tripID}";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Close();
            con.Close();
            con.Dispose();

        }

        public static void DeleteUser(int userID)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            con.Open();
            string sql = $"DELETE FROM User WHERE user_id = {userID}";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Close();
            con.Close();
            con.Dispose();

        }

        public static void DeleteOrder(int orderID)
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            con.Open();
            string sql = $"DELETE FROM Trip_order WHERE order_id = {orderID}";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();

            reader.Close();
            con.Close();
            con.Dispose();

        }

        public static List<User> GetUser()
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            con.Open();
            string sql = "SELECT * FROM User";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            List<User> users = new List<User>();
            while (reader.Read())
            {
                User newUser = new User();
                newUser.ID = reader.GetInt32(reader.GetOrdinal("user_id"));
                newUser.Name = reader.GetString(reader.GetOrdinal("name"));
                newUser.LastName = reader.GetString(reader.GetOrdinal("last_name"));
                newUser.Email = reader.GetString(reader.GetOrdinal("email"));
                newUser.Phone = reader.GetString(reader.GetOrdinal("phone"));
                newUser.UserCountry = reader.GetString(reader.GetOrdinal("country"));
                users.Add(newUser);
            }
            reader.Close();
            con.Close();
            con.Dispose();

            List<Trip> trips = GetTrips();

            con = new SQLiteConnection(@"Data Source=keliones.db");
            con.Open();
            sql = "SELECT * FROM Trip_order";
            command = new SQLiteCommand(sql, con);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                int trip_id = reader.GetInt32(reader.GetOrdinal("trip_id"));
                int user_id = reader.GetInt32(reader.GetOrdinal("user_id"));

                foreach(User u in users)
                {
                    if(u.ID==user_id)
                    {
                        foreach(Trip t in trips)
                        {
                            if (t.ID == trip_id) u.UserTrips.Add(t);
                        }
                    }
                }
            }

            reader.Close();
            con.Close();
            con.Dispose();

            return users;
        }

        public static List<Trip> GetTrips()
        {
            SQLiteConnection con = new SQLiteConnection(@"Data Source=keliones.db");
            con.Open();
            string sql = "SELECT * FROM Trip";
            SQLiteCommand command = new SQLiteCommand(sql, con);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Trip> trips = new List<Trip>();
            while (reader.Read())
            {
                Trip newTrip = new Trip();
                newTrip.ID = reader.GetInt32(reader.GetOrdinal("trip_id"));
                newTrip.Name = reader.GetString(reader.GetOrdinal("name"));
                newTrip.StartDate = reader.GetString(reader.GetOrdinal("start_date"));
                newTrip.EndDate = reader.GetString(reader.GetOrdinal("end_date"));
                newTrip.Country = reader.GetString(reader.GetOrdinal("country"));
                newTrip.Price = reader.GetDecimal(reader.GetOrdinal("price"));
                newTrip.Rating = reader.GetDecimal(reader.GetOrdinal("rating"));
                trips.Add(newTrip);
            }
            reader.Close();
            con.Close();
            con.Dispose();

            sql = "SELECT * FROM City";
            con = new SQLiteConnection(@"Data Source=keliones.db");
            con.Open();
            command = new SQLiteCommand(sql, con);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                int trip_id = reader.GetInt32(reader.GetOrdinal("trip_id"));
                string name = reader.GetString(reader.GetOrdinal("name"));
                foreach (Trip t in trips)
                {
                    if (t.ID == trip_id) t.Cities.Add(name);
                }
            }

            reader.Close();
            con.Close();
            con.Dispose();
            return trips;
        }
    }
}
