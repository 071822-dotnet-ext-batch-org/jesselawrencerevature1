using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace polyNinheritance
{
    class Program
    {
        static void Main(string[] args)
        {
        
            RentalCar car = new RentalCar();
            RentalBoat boat = new RentalBoat();
            RentalTruck truck = new RentalTruck();

            RentalVehicle[] vehicles = {car, boat, truck};

            foreach (RentalVehicle vehicle in vehicles)
            {
                vehicle.Rent();
            }

            Console.ReadKey();
        }
    }
}