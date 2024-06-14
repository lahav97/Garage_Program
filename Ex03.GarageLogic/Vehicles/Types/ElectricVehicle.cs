using GarageLogic.Exceptions;
using System;

namespace GarageLogic.Vehicles.Types
{
    public abstract class ElectricVehicle : Vehicle
    {
        float m_MaxBatteryCapacity;
        float m_RemainingBattery;

        public float MaxBatteryCapacity
        {
            get { return m_MaxBatteryCapacity; }
            set { m_MaxBatteryCapacity = value; }
        }

        public float RemainingBattery
        {
            get
            {
                return m_RemainingBattery * VehicleInfo.EnergyPercentageLeft / 100;
            }
        }

        public void ChargeBattery(float i_ChargingTime)
        {
            ValidateOutOfRange(i_ChargingTime);
            VehicleInfo.EnergyPercentageLeft = (RemainingBattery + i_ChargingTime) / 100;
        }

        public void ValidateOutOfRange(float i_FuelToAdd)
        {
            if (i_FuelToAdd + RemainingBattery > m_MaxBatteryCapacity)
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
    }
}
