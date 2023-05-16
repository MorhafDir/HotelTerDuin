using System.Security.Cryptography;
using MySql.Data.MySqlClient;

namespace HotelTerDuin.Data
{
    public class LoginService
    {
        string server = "localhost";
        string database = "vinylshop2";
        string uid = "root";
        string password = "";

        string connectionString => $"server={server};database={database};uid={uid};password={password};";

        public LoginService()
        {
            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        public bool Login(string voornaam, string password)
        {
            using var command = connection.CreateCommand();
            command.CommandText = "select count(*) from klant where voornaam = @user and password = @password";
            command.Parameters.AddWithValue("@user", voornaam);
            command.Parameters.AddWithValue("@password", password);
            var result = command.ExecuteScalar();
            return (long)result == 1;
        }

        public void Register(string voornaam, string password)
        {
            var hashedPassword =  HashPassword(password);
            using var command = connection.CreateCommand();
            command.CommandText = "insert into klant (voornaam, password) values (@user, @password)";
            command.Parameters.AddWithValue("@user", voornaam);
            command.Parameters.AddWithValue("@password", hashedPassword);
            command.ExecuteScalar();
        }

public static string HashPassword(string password)
    {
        byte[] salt;
        new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

        var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
        byte[] hash = pbkdf2.GetBytes(20);

        byte[] hashBytes = new byte[36];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 20);

        return Convert.ToBase64String(hashBytes);
    }


    private readonly MySqlConnection connection;

    }

}