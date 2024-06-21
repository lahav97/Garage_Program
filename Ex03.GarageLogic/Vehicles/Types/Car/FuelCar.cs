namespace GarageLogic.Vehicles.Types.Car
{
    public class FuelCar : FuelVehicle
    {
        public CarInfo MotorcycleInfo { get; } = new CarInfo();

        public FuelCar()
        {
            InstallWheels((int)eNumberOfWheels.Car, (float)eMaxWheelAirPressure.Car);
            InitializeFuelTank(eFuelTypes.Octan95, 45f);
            m_VehicleInfo = new CarInfo();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
