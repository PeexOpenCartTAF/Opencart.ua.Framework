using System.Data.SqlClient;

namespace Opencart.ua.Tools.DBHelpers
{
    public static class DataBaseHelper
    {
        public static List<User> GetAllUsers()
        {
            var users = new List<User>();

            using var connection = DbConnectionFactory.CreateSqlConnection();
            connection.Open();

            string sql = "SELECT Id, FirstName, LastName, Email, Password FROM Users";

            using SqlCommand command = new(sql, connection);
            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                users.Add(MapUser(reader));
            }
            return users;
        }

        public static User GetUserByEmail(string email)
        {
            using var connection = DbConnectionFactory.CreateSqlConnection();
            connection.Open();

            string sql = "SELECT Id, FirstName, LastName, Email, Password FROM Users WHERE Email = @Email";

            using SqlCommand command = new(sql, connection);
            command.Parameters.AddWithValue("@Email", email);
            
            using SqlDataReader reader = command.ExecuteReader();

            return reader.Read() ? MapUser(reader) : null;
        }

        private static User MapUser(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(0),
                FirstName = reader.GetString(1),
                LastName = reader.GetString(2),
                Email = reader.GetString(3),
                Password = reader.GetString(4)
            };
        }

    }

    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
