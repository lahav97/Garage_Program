namespace GarageLogic.Vehicles.Types.Car
{
    public class ElectricCar : ElectricVehicle
    {
        public ElectricCar()
        {
            InstallWheels((int)eNumberOfWheels.Car, (float)eMaxWheelAirPressure.Car);
            InitializeMaxBatteryTime(3.5f);
            m_VehicleInfo = new CarInfo();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
