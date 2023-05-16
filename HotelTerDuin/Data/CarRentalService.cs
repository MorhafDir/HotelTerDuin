namespace HotelTerDuin.Data
{
    public class CarRentalService
    {
        public string Rent(int CarId)
        {
            return $"{CarId} rented!";
        }

        public string Return(int CarId)
        {
            return $"{CarId} returned!";
        }
    }
}