using System.Data;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace HotelTerDuin.Data
{
    public class LoginService
    {
        public bool Login(string voornaam, string password, out bool isAdmin)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM klant WHERE voornaam = @user AND password = @password";
                    command.Parameters.AddWithValue("@user", voornaam);
                    command.Parameters.AddWithValue("@password", password);
                    var result = command.ExecuteScalar();
                    var count = Convert.ToInt64(result);

                    if (count == 1)
                    {
                        command.CommandText = "SELECT admin FROM klant WHERE voornaam = @user";
                        var isAdminResult = command.ExecuteScalar();
                        isAdmin = Convert.ToBoolean(isAdminResult);
                        return true;
                    }
                }
            }

            isAdmin = false;
            return false;
        }

        public void Register(string voornaam, string password)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                if (connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO klant (voornaam, password) VALUES (@user, @password)";
                    command.Parameters.AddWithValue("@user", voornaam);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}