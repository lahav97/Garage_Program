using GarageLogic.Exceptions;
using System;
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
                return m_RemainingBatteryTime;
            }
            set
            {
                m_RemainingBatteryTime = value;
            }
        }

        internal void SetRemainingBatteryFromEnergyPercentageLeft(float i_EnergyPercentageLeft)
        {
            RemainingBatteryTime = i_EnergyPercentageLeft * MaxBatteryCapacity / 100;
        }

        public float GetRemainingBatteryCapacityToCharge()
        {
            return MaxBatteryCapacity - RemainingBatteryTime;
        }

        public void ChargeBattery(float i_ChargingTime)
        {
            validateOutOfRange(i_ChargingTime);
            RemainingBatteryTime += i_ChargingTime;
            m_VehicleInfo.EnergyPercentageLeft = (RemainingBatteryTime * 100) / MaxBatteryCapacity;
        }

        private void validateOutOfRange(float i_FuelToAdd)
        {
            if (i_FuelToAdd + RemainingBatteryTime > m_MaxBatteryCapacity)
            {
                throw new ValueOutOfRangeException("Charging time", m_MaxBatteryCapacity, 0f);
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
              .AppendLine($"Max battery capacity: {MaxBatteryCapacity} hours")
              .AppendLine($"Remaining battery time: {RemainingBatteryTime} hours");

            return stringBuilder.ToString();
        }
    }
}
