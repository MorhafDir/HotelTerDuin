using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace HotelTerDuin.Data
{
    public class LoginService
    {
        public bool Login(string voornaam, string password)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT COUNT(*) FROM klant WHERE voornaam = @user AND password = @password";
                    command.Parameters.AddWithValue("@user", voornaam);
                    command.Parameters.AddWithValue("@password", password);
                    var result = command.ExecuteScalar();
                    return (long)result == 1;
                }
            }
        }

        public void Register(string voornaam, string password)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into klant (voornaam, password) values (@user, @password)";
                    command.Parameters.AddWithValue("@user", voornaam);
                    command.Parameters.AddWithValue("@password", password);
                    command.ExecuteScalar();
                }
            }
        }

    }
    }