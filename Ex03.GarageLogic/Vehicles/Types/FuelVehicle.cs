using System;
using GarageLogic.Exceptions;

namespace GarageLogic.Vehicles.Types
{
    public abstract class FuelVehicle : Vehicle
    {
        public enum eFuelTypes
        {
            Soler = 1,
            Octan95,
            Octan96,
            Octan98
        }

        float m_MaxFuelTank;
        float m_RemainingFuel;

        public eFuelTypes FuelType { get; set; }
        public float MaxFuelTank {
            get { return m_MaxFuelTank;} 
            set {  m_MaxFuelTank = value;}
        }

        public float RemainingFuel 
        {
            get 
            {
                return m_MaxFuelTank * VehicleInfo.EnergyPercentageLeft / 100; 
            } 
        }

        public void Refuel(float i_FuelToAdd, eFuelTypes i_FuelType)
        {
            ValidateFuelType(i_FuelType);
            ValidateOutOfRange(i_FuelToAdd);
            VehicleInfo.EnergyPercentageLeft = (RemainingFuel + i_FuelToAdd) / 100;
        }

        public void ValidateOutOfRange(float i_FuelToAdd)
        {
            if(i_FuelToAdd + RemainingFuel > MaxFuelTank)
            {
                throw new ValueOutOfRangeException("Fuel amount", MaxFuelTank, 0f);
            }
            else if(i_FuelToAdd <= 0) 
            {
                throw new ArgumentException("Can't add non-positive amount of fuel to the tank");
            }
        }

        private void ValidateFuelType(eFuelTypes i_FuelType)
        {
            if (!FuelType.Equals(i_FuelType))
            {
                throw new ArgumentException($"Invalid Fuel type for this licensed number :{VehicleInfo.LicensePlateID}");
            }

        }

        public void InitializeFuelTank(eFuelTypes i_FuelType, float i_MaxFuelTank)
        {
            FuelType = i_FuelType;
            MaxFuelTank = i_MaxFuelTank;
        }
    }
}
