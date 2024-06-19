namespace GarageLogic.Vehicles.Types.Motorcycle
{
    public class FuelMotorcycle : FuelVehicle
    {
        public MotorcycleInfo m_MotorcycleInfo;

        public MotorcycleInfo MotorcycleInfo { get; set; }

        public FuelMotorcycle()
        {
            InstallWheels((int)eNumberOfWheels.MotorCycle, (float)eMaxWheelAirPressure.Motorcycle);
            InitializeFuelTank(eFuelTypes.Octan98, 5.5f);
            m_VehicleInfo = new MotorcycleInfo();
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}


