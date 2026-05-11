using System.Configuration;
using System.Data.SqlClient;

namespace Opencart.ua.Tools.DBHelpers
{
    public static class DbConnectionFactory
    {
        private const string ConnectionStringName = "OpencartDB";
        private const string EnvVarName = "DB_PASSWORD";

        public static string GetConnectionString()
        {
            var connectionStringSettings =
                ConfigurationManager.ConnectionStrings[ConnectionStringName];

            if (connectionStringSettings == null || string.IsNullOrWhiteSpace(connectionStringSettings.ConnectionString))
            {
                throw new InvalidOperationException($"Connection string '{ConnectionStringName}' is not configured in app.config.");
            }

            string password = Environment.GetEnvironmentVariable(EnvVarName);

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new InvalidOperationException($"Database password not found. Set the environment variable '{EnvVarName}'.");
            }

            var builder = new SqlConnectionStringBuilder(connectionStringSettings.ConnectionString)
            {
                Password = password
            };

            return builder.ConnectionString;
        }

        public static SqlConnection CreateSqlConnection()
        {
            return new SqlConnection(GetConnectionString());
        }
    }
}