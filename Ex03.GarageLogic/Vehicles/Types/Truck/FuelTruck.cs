namespace GarageLogic.Vehicles.Types.Truck
{
    public class FuelTruck : FuelVehicle
    {
        public FuelTruck()
        {
            InstallWheels((int)eNumberOfWheels.Truck, (float)eMaxWheelAirPressure.Truck);
            InitializeFuelTank(eFuelTypes.Soler, 120f);
            m_VehicleInfo = new TruckInfo();
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
