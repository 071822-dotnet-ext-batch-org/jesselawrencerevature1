namespace polyNinheritance
{

    public class RentalCar : RentalVehicle
    {
        public CarType Style {get; set;}
        public VehicleColor Color {get; set;}
        public override void Rent()
        {
            Console.WriteLine("You car has been rented!");
        }
    }

}