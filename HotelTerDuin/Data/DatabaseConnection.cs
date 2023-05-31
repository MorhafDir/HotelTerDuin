using MySql.Data.MySqlClient;

namespace HotelTerDuin.Data
{
    public class DatabaseConnection
    {
        private static MySqlConnection connection;

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                string server = "localhost";
                string database = "vinylshop2";
                string uid = "root";
                string password = "";

                string connectionString = $"server={server};database={database};uid={uid};password={password};";
                connection = new MySqlConnection(connectionString);
                connection.Open();
            }

            return connection;
        }
    }
}
