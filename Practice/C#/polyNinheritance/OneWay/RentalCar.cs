namespace polyNinheritance
{

    public class RentalCar : RentalVehicle
    {
        public CarType Style {get; set;}
        public int Weight {get; set;}
    }

    public class MatchBoxCar
    {
        public int Weight {get; set;}
    }
}