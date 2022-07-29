namespace polyNinheritance
{
    public class RentalBoat : RentalVehicle
    {
        public BoatType Style {get; set;}
        public VehicleColor Color {get; set;}

        public override void Rent()
        {
            Console.WriteLine("You boat has been rented!");
        }
    }
}