using System;
using System.Collections.Generic;
using System.Text;
using GarageLogic.Vehicles.Types;
using GarageLogic.VehiclesInfo;

namespace GarageLogic.Vehicles
{
    public abstract class Vehicle
    {
        public VehicleInformation m_VehicleInfo { get; set; }
        public List<Wheel> m_Wheels { get; set; }

        public List<string> OutputPromptsList()
        {
            return m_VehicleInfo.AllInformationNeededForVehiclePrompts();
        }

        public List<string> OutputPromptListForWheel(out int o_NumberOfWheels)
        {
            o_NumberOfWheels = m_Wheels.Count;

            return Wheel.ListOfInformationNeededForWheels();
        }

        public void GatherInformationForVehicle(List<string> i_ListOfInformationToFill, string i_LeicensePlate)
        {
            m_VehicleInfo.FillVehicleInformation(i_ListOfInformationToFill, i_LeicensePlate);
            if(this is FuelVehicle fuelVehicle)
            {
                fuelVehicle.SetRemainingFuelFromEnergyPercentageLeft(this.m_VehicleInfo.EnergyPercentageLeft);
            }

            if(this is ElectricVehicle electricVehicle) 
            {
                electricVehicle.SetRemainingBatteryFromEnergyPercentageLeft(this.m_VehicleInfo.EnergyPercentageLeft);
            }
        }

        protected void InstallWheels(int i_NumberOfWheels, float i_MaxAirPressure)
        {
            m_Wheels = new List<Wheel>(i_NumberOfWheels);

            for(int i = 0; i < i_NumberOfWheels; i++)
            {
                Wheel wheel = new Wheel(i_MaxAirPressure);
                m_Wheels.Add(wheel);
            }
        }

        public void EnterWheelsInformation(List<string> i_WheelsInformationList)
        {
            float airPressure;
            for (int i = 0; i < m_Wheels.Count; i++)
            {
                m_Wheels[i].ManufactureName = i_WheelsInformationList[i];
                if (float.TryParse(i_WheelsInformationList[i +1],out airPressure))
                {
                    m_Wheels[i].CurrentAirPressure = airPressure;
                }
                else
                {
                    throw new ArgumentException("Input of air Presure must be a number");
                }
            }
        }

        internal void InflateAllWheelsToMaximum()
        {
            foreach (Wheel wheel in m_Wheels)
            {
                wheel.InflateToMaximum();
            }
        }

        public override string ToString()
        {
            StringBuilder vehicleData = new StringBuilder();

            vehicleData.AppendLine(m_VehicleInfo.ToString())
              .AppendLine("m_Wheels Information:");

            foreach (Wheel wheel in m_Wheels)
            {
                vehicleData.AppendLine($"Wheel #{m_Wheels.IndexOf(wheel) + 1}:")
                .AppendLine(wheel.ToString());
            }

            return vehicleData.ToString();
        }
    }
}
