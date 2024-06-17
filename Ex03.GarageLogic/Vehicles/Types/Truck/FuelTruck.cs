namespace GarageLogic.Vehicles.Types.Truck
{
    public class FuelTruck : FuelVehicle
    {
        public TruckInfo m_TruckInformation;

        public TruckInfo TruckInformation { get; set; }

        public FuelTruck()
        {
            InstallWheels((int)eNumberOfWheels.Truck, (float)eMaxWheelAirPressure.Truck);
            InitializeFuelTank(eFuelTypes.Soler, 120f);
            VehicleInfo = new TruckInfo();
        }
        
        public override string ToString()
        {
            return base.ToString();
        }
    }
}