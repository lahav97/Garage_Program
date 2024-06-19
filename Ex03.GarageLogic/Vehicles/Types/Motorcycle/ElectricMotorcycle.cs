using GarageLogic.Vehicles.Types.Truck;

namespace GarageLogic.Vehicles.Types.Motorcycle
{
    public class ElectricMotorcycle : ElectricVehicle
    {
        public MotorcycleInfo m_MotorcycleInfo;

        public MotorcycleInfo MotorcycleInfo { get; set; }

        public ElectricMotorcycle()
        {
            InstallWheels((int)eNumberOfWheels.MotorCycle, (float)eNumberOfWheels.MotorCycle);
            InitializeMaxBatteryTime(2.5f);
            m_VehicleInfo = new MotorcycleInfo();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
