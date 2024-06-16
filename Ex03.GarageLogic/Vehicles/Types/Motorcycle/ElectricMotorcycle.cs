using GarageLogic.Vehicles.Types.Truck;

namespace GarageLogic.Vehicles.Types.Motorcycle
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        public MotorcycleInfo MotorcycleInfo { get; } = new MotorcycleInfo();
        public ElectricMotorcycle()
        {
            InstallWheels((int)eNumberOfWheels.MotorCycle, (float)eNumberOfWheels.MotorCycle);
            InitializeMaxBatteryTime(2.5f);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
