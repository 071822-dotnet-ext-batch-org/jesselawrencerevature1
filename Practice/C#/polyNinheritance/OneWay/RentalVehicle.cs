namespace polyNinheritance
{
    public class RentalVehicle
    {
       
        public int RentalId {get; set;}

        public string CurrentRenter {get; set;}

        public decimal PricePerDay {get; set;}

        public int NumberOfPassengers {get; set;}
        public virtual void Rent()
        {
            
        }
        }
    }

