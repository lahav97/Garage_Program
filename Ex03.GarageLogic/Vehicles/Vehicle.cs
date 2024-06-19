using System;
using System.Collections.Generic;
using System.Text;
using GarageLogic.Vehicles.Types;
using GarageLogic.VehiclesInfo;

namespace GarageLogic.Vehicles
{
    public abstract class Vehicle
    {
        public VehicleInformation VehicleInfo { get; set; }

        public List<Wheel> Wheels { get; set; }

        public List<string> OutputPromptsList()
        {
            return VehicleInfo.AllInformationNeededForVehiclePrompts();
        }

        public List<string> OutputPromptListForWheel(out int o_numberOfWheels)
        {
            o_numberOfWheels = Wheels.Count;

            return Wheel.ListOfInformationNeededForWheels();
        }

        public void GatherInformationForVehicle(List<string> i_ListOfInformationToFill, string i_LeicensePlate)
        {
            VehicleInfo.FillVehicleInformation(i_ListOfInformationToFill, i_LeicensePlate);
            if(this is FuelVehicle fuelVehicle)
            {
                fuelVehicle.SetRemainingFuelFromEnergyPercentageLeft(this.VehicleInfo.EnergyPercentageLeft);
            }
            if(this is ElectricVehicle electricVehicle) 
            {
                electricVehicle.SetRemainingBatteryFromEnergyPercentageLeft(this.VehicleInfo.EnergyPercentageLeft);
            }
        }

        protected void InstallWheels(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            Wheels = new List<Wheel>(i_NumberOfWheels);

            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel(i_MaxAirPressure);
                Wheels.Add(wheel);
            }
        }

        public void EnterWheelsInformation(List<string> i_WheelsInformationList)
        {
            float airPressure;
            for (int i = 0; i < Wheels.Count; i++)
            {
                Wheels[i].ManufactureName = i_WheelsInformationList[i];
                if (float.TryParse(i_WheelsInformationList[i +1],out airPressure))
                {
                    Wheels[i].CurrentAirPressure = airPressure;
                }
                else
                {
                    throw new ArgumentException("Input of air Presure must be a number");
                }
            }
        }

        public void InflateAllWheelsToMaximum()
        {
            foreach (Wheel wheel in Wheels)
            {
                wheel.InflateToMaximum();
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleData = new StringBuilder();

            vehicleData.AppendLine(VehicleInfo.ToString())
              .AppendLine("Wheels Information:");

            foreach (Wheel wheel in Wheels)
            {
                vehicleData.AppendLine($"Wheel #{Wheels.IndexOf(wheel) + 1}:")
                .AppendLine(wheel.ToString());
            }

            return vehicleData.ToString();
        }
    }
}
