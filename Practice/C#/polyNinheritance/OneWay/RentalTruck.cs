namespace polyNinheritance
{
    public class RentalTruck : RentalVehicle
    {
        public TruckType Style {get; set;}
        public VehicleColor Color {get; set;}

        public override void Rent()
        {
            Console.WriteLine("You truck has been rented!");
        }
    }
}