using System.Collections.Generic;
using System.Text;
using GarageLogic.VehiclesInfo;

namespace GarageLogic.Vehicles
{
    public abstract class Vehicle
    {
        public VehicleInformation VehicleInfo { get; set; }

        public List<Wheel> Wheels { get; set; }

        protected void InstallWheels(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            Wheels = new List<Wheel>(i_NumberOfWheels);

            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel(i_MaxAirPressure);
                Wheels.Add(wheel);
            }
        }

        public void InflateAllWheelsToMaximum()
        {
            foreach (Wheel wheel in Wheels)
            {
                wheel.InflateWheels(wheel.MaxAirPressure);
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleData = new StringBuilder();

            vehicleData.AppendLine(VehicleInfo.ToString())
              .AppendLine("Wheels Information:");

            foreach (Wheel wheel in Wheels)
            {
                vehicleData.AppendLine($"Weel #{Wheels.IndexOf(wheel) + 1}:")
                .AppendLine(wheel.ToString());
            }

            return vehicleData.ToString();
        }
    }
}
