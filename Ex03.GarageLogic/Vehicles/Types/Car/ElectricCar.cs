using System;
using System.Text;
using GarageLogic.Exceptions;
using static GarageLogic.Vehicles.Types.FuelVehicle;

namespace GarageLogic.Vehicles.Types.Car
{
    public class ElectricCar : ElectricVehicle
    {
        public ElectricCar()
        {
            InstallWheels((int)eNumberOfWheels.Car, (float)eMaxWheelAirPressure.Car);
            InitializeMaxBatteryTime(3.5f);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
