namespace GarageLogic.Vehicles.Types.Truck
{
    public class FuelTruck : FuelVehicle
    { 
        public FuelTruck()
        {
            InstallWheels((int)eNumberOfWheels.Truck, (float)eMaxWheelAirPressure.Truck);
            InitializeFuelTank(eFuelTypes.Soler, 120f);
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
