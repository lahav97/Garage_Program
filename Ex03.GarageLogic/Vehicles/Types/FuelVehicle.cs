using System;
using System.Text;
using GarageLogic.Exceptions;

namespace GarageLogic.Vehicles.Types
{
    public abstract class FuelVehicle : Vehicle
    {
        float m_MaxFuelTank;
        float m_RemainingFuel;

        internal eFuelTypes FuelType { get; set; }

        public float MaxFuelTank 
        {
            get { return m_MaxFuelTank;} 
            set {  m_MaxFuelTank = value;}
        }

        public float RemainingFuel 
        {
            get 
            {
                return m_RemainingFuel; 
            } 
            set
            {
                m_RemainingFuel = value;
            }
        }

        public float GetRemainingTankCapacityToRefuel()
        {
            return MaxFuelTank - RemainingFuel;
        }

        public void Refuel(float i_FuelToAdd, eFuelTypes i_FuelType)
        {
            validateFuelType(i_FuelType);
            validateOutOfRange(i_FuelToAdd);
            RemainingFuel += i_FuelToAdd;
            m_VehicleInfo.EnergyPercentageLeft = RemainingFuel * 100 / MaxFuelTank;
        }

        private void validateOutOfRange(float i_FuelToAdd)
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

        private void validateFuelType(eFuelTypes i_FuelType)
        {
            if (!FuelType.Equals(i_FuelType))
            {
                throw new ArgumentException($"Invalid fuel type for this licensed number :{m_VehicleInfo.LicensePlateID}");
            }

        }

        internal void SetRemainingFuelFromEnergyPercentageLeft(float i_EnergyPercentageLeft)
        {
            RemainingFuel = i_EnergyPercentageLeft * MaxFuelTank / 100;
        }

        public void InitializeFuelTank(eFuelTypes i_FuelType, float i_MaxFuelTank)
        {
            FuelType = i_FuelType;
            MaxFuelTank = i_MaxFuelTank;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine(base.ToString())
               .AppendLine($"Fuel type: {FuelType}")
              .AppendLine($"Max fuel tank: {MaxFuelTank} liters")
              .AppendLine($"Remaining fuel: {RemainingFuel} liters");

            return stringBuilder.ToString();
        }
    }
}
