namespace GarageLogic.Vehicles.Types.Car
{
    public class FuelCar : FuelVehicle
    {
        public CarInfor MotorcycleInfo { get; } = new CarInfor();

        public FuelCar()
        {
            InstallWheels((int)eNumberOfWheels.Car, (float)eMaxWheelAirPressure.Car);
            InitializeFuelTank(eFuelTypes.Octan95, 45f);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
