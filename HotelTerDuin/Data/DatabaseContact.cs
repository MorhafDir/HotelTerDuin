using MySql.Data.MySqlClient;

namespace HotelTerDuin.Data
{
    public class DatabaseContact
    {
        public void PushContact(string naam, string email, string vraag)
        {
            using (var connection = DatabaseConnection.GetConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "insert into contact (naam, email, vraag) values (@naam, @email, @vraag)";
                    command.Parameters.AddWithValue("@naam", naam);
                    command.Parameters.AddWithValue("@email", email);
                    command.Parameters.AddWithValue("@vraag", vraag);
                    command.ExecuteScalar();
                }
            }
        }
    }
}
