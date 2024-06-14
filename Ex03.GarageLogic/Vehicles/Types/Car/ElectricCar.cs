using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
