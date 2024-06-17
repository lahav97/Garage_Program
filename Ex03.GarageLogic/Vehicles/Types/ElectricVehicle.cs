using GarageLogic.Exceptions;
using System;
using static GarageLogic.Vehicles.Types.FuelVehicle;
using System.Text;

namespace GarageLogic.Vehicles.Types
{
    public abstract class ElectricVehicle : Vehicle
    {
        float m_MaxBatteryCapacity;
        float m_RemainingBatteryTime;

        public float MaxBatteryCapacity
        {
            get { return m_MaxBatteryCapacity; }
            set { m_MaxBatteryCapacity = value; }
        }

        public float RemainingBatteryTime
        {
            get
            {
                return m_RemainingBatteryTime * VehicleInfo.EnergyPercentageLeft / 100;
            }
        }

        public void ChargeBattery(float i_ChargingTime)
        {
            validateOutOfRange(i_ChargingTime);
            VehicleInfo.EnergyPercentageLeft = (RemainingBatteryTime + i_ChargingTime) / 100;
        }

        private void validateOutOfRange(float i_FuelToAdd)
        {
            if (i_FuelToAdd + RemainingBatteryTime > m_MaxBatteryCapacity)
            {
                throw new ValueOutOfRangeException("charging time", m_MaxBatteryCapacity, 0f);
            }
            else if (i_FuelToAdd <= 0)
            {
                throw new ArgumentException("Can't add non-positive amount of charging time to the battery");
            }
        }

        public void InitializeMaxBatteryTime(float i_MaxBatteryCapacity)
        {
            MaxBatteryCapacity = i_MaxBatteryCapacity;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
              .AppendLine($"Max battery capacity: {MaxBatteryCapacity}")
              .AppendLine($"Remaining battery time: {RemainingBatteryTime}");

            return stringBuilder.ToString();
        }
    }
}
