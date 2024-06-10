using Garage;
using System;
using System.Collections.Generic;


namespace Vehicle
{

    public enum eGasTypes
    {
        Soler,
        Octan95,
        Octan96,
        Octan98
    }

    public enum eCarColors
    {
        Yellow,
        White,
        Red,
        Black
    }

    public enum eLicenseType
    {
        A,
        A1,
        AA,
        B1
    }

    internal abstract class Vehicle
    {
        string m_ModelName;
        string m_LicensePlateID;
        float m_EnergyPercentageLeft;
        Wheel[] m_Wheels;
        EnergyStorage m_VehicleTank;
        VehicleDetails m_VehicleDeta;
    }

     internal abstract class EnergyStorage
    {
        protected eGasTypes m_GasType;
        protected float m_MaxEnergyCapacity;
        protected float m_MinEnergyCapacity = 0;
        protected float m_CurrentEnergyStorage;

        protected abstract void Refueling(float i_EnergyPercentageToAdd, eGasTypes i_GasType);
        protected abstract void Refueling(float i_EnergyPercentageToAdd);
        protected bool IsOutOfRange(float i_EnergyPercentageToAdd)
        {
            return i_EnergyPercentageToAdd + m_CurrentEnergyStorage <= m_MaxEnergyCapacity;
        }
    }

    internal class Battery : EnergyStorage
    {
        protected override void Refueling(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();
        }
        protected override void Refueling(float i_EnergyPercentageToAdd)
        {
            if (IsOutOfRange(i_EnergyPercentageToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxEnergyCapacity, m_MinEnergyCapacity);
            }
            else
            {
                m_CurrentEnergyStorage += i_EnergyPercentageToAdd;
            }
        }
    }

    internal class FuelTank: EnergyStorage
    {
        protected override void Refueling(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();

        }
        protected override void Refueling(float i_EnergyPercentageToAdd)
        {
            if (IsOutOfRange(i_EnergyPercentageToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxEnergyCapacity, m_MinEnergyCapacity);
            }
            else
            {
                m_CurrentEnergyStorage += i_EnergyPercentageToAdd;
            }
        }
    }

    internal class Wheel
    {
        private string m_ManufactureName;
        private float m_AirPressure;
        private float m_MaxAirPressure;
        private float m_MinAirPressure;
        private void WheelInflation(float i_AirToAdd)
        {
            if (IsMoreThanMaxAirPressure(i_AirToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxAirPressure, m_MinAirPressure);
            }
            else
            {
                m_AirPressure += i_AirToAdd;
            }
        }
        private bool IsMoreThanMaxAirPressure(float i_AirToAdd)
        {
            return i_AirToAdd + m_AirPressure <= m_MaxAirPressure;
        }
    }

    internal class Motorcycle : Vehicle
    {
        eLicenseType m_LicenseType;
        int m_EngineVolume;
    }

    internal class car : Vehicle
    {
        eCarColors m_CarColor;
        int m_NumberOfDoors;


    }

    internal class Truck : Vehicle
    {
        bool m_TransportsHazardousMaterials;
        float m_CargoVolume;
    }
}
