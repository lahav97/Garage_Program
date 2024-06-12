using Garage;
using System;
using System.Collections.Generic;


namespace Vehicles
{

    public enum eGasTypes
    {
        Soler,
        Octan95,
        Octan96,
        Octan98,
        None
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
        protected string m_ModelName;
        protected string m_LicensePlateID;
        protected float m_EnergyPercentageLeft;
        protected Wheel[] m_Wheels;
        protected EnergyStorage m_VehicleTank;

        protected Vehicle(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels,
            bool isElectric, eGasTypes i_GasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
        {
            m_ModelName = i_ModelName;
            m_LicensePlateID = i_LicensePlateID;
            m_EnergyPercentageLeft = i_EnergyPercentageLeft;
            m_Wheels = i_Wheels;
            m_VehicleTank = isElectric ? (EnergyStorage)new Battery(i_MaxEnergyCapacity, i_CurrentEnergyStorage) : new FuelTank(i_GasType, i_MaxEnergyCapacity, i_CurrentEnergyStorage);
        }

        protected abstract float GetMaxEnergyCapacity();
        protected abstract float GetCurrentEnergyStorage();
    }

    internal abstract class EnergyStorage
    {
        protected eGasTypes m_GasType;
        protected float m_MaxEnergyCapacity;
        protected float m_MinEnergyCapacity = 0;
        protected float m_CurrentEnergyStorage;

        public EnergyStorage(eGasTypes i_GasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
        {
            m_GasType = i_GasType;
            m_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrentEnergyStorage = i_CurrentEnergyStorage;
        }

        protected abstract void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType);
        protected abstract void Refuel(float i_EnergyPercentageToAdd);
        protected bool IsOutOfRange(float i_EnergyPercentageToAdd)
        {
            return i_EnergyPercentageToAdd + m_CurrentEnergyStorage > m_MaxEnergyCapacity;
        }

        public float GetMaxEnergyCapacity()
        {
            return m_MaxEnergyCapacity;
        }

        public float GetCurrentEnergyStorage()
        {
            return m_CurrentEnergyStorage;
        }
    }

    internal class Battery : EnergyStorage
    {
        public Battery(float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
           : base(eGasTypes.None, i_MaxEnergyCapacity, i_CurrentEnergyStorage) { }

        protected override void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();
        }

        protected override void Refuel(float i_EnergyPercentageToAdd)
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


    internal class FuelTank : EnergyStorage
    {
        public FuelTank(eGasTypes i_GasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
            : base(i_GasType, i_MaxEnergyCapacity, i_CurrentEnergyStorage) { }

        protected override void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();
        }

        protected override void Refuel(float i_EnergyPercentageToAdd)
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
        public string m_WheelManufactureName { get; }
        public float m_WheelAirPressure { get; set; }
        public float m_WheelMaxAirPressure { get; }
        public float m_WheelMinAirPressure { get; }

        public Wheel(float i_AirPressure, float i_MaxAirPressure, string i_ManufactureName)
        {
            m_WheelMaxAirPressure = i_MaxAirPressure;
            m_WheelAirPressure = i_AirPressure;
            m_WheelManufactureName = i_ManufactureName;
            m_WheelMinAirPressure = 0;
        }

        private void InflateWheel(float i_AirToAdd)
        {
            if (IsMoreThanMaxAirPressure(i_AirToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(m_WheelMaxAirPressure, m_WheelMinAirPressure);
            }
            else
            {
                m_WheelAirPressure += i_AirToAdd;
            }
        }

        private bool IsMoreThanMaxAirPressure(float i_AirToAdd)
        {
            return i_AirToAdd + m_WheelAirPressure > m_WheelMaxAirPressure;
        }
    }


    internal class Motorcycle : Vehicle
    {
        private readonly eLicenseType m_LicenseType;
        private readonly int m_EngineVolume;
        private readonly float m_MaxAirPressure = 33;
        private readonly int m_MaxWheels = 2;

        public Motorcycle(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, eLicenseType i_eLicenseType, int i_EngineVolume,
            bool i_IsElectric, eGasTypes i_eGasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
           : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_IsElectric, i_eGasType, i_MaxEnergyCapacity, i_CurrentEnergyStorage)
        {
            m_LicenseType = i_eLicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_WheelMaxAirPressure > m_MaxAirPressure)
                {
                    airPressureValid = false;
                    break;
                }
            }
            return airPressureValid;
        }

        protected bool IsAmountOfWheelsValid(Wheel[] i_Wheels)
        {
            return i_Wheels.Length <= m_MaxWheels;
        }

        protected override float GetMaxEnergyCapacity()
        {
            return m_VehicleTank.GetMaxEnergyCapacity();
        }

        protected override float GetCurrentEnergyStorage()
        {
            return m_VehicleTank.GetCurrentEnergyStorage();
        }
    }

    internal class Car : Vehicle
    {
        private readonly eCarColors m_CarColor;
        private readonly int m_NumberOfDoors;
        private readonly float m_MaxAirPressure = 31;
        private readonly int m_MaxWheels = 5;

        public Car(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, eCarColors i_eCarColor, int i_NumberOfDoors, bool i_IsElectric, eGasTypes i_eGasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
            : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_IsElectric, i_eGasType, i_MaxEnergyCapacity, i_CurrentEnergyStorage)
        {
            m_CarColor = i_eCarColor;
            m_NumberOfDoors = i_NumberOfDoors;
        }
        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_WheelMaxAirPressure > m_MaxAirPressure)
                {
                    airPressureValid = false;
                    break;
                }
            }
            return airPressureValid;
        }

        protected bool IsAmountOfWheelsValid(Wheel[] i_Wheels)
        {
            return i_Wheels.Length <= m_MaxWheels;
        }

        protected override float GetMaxEnergyCapacity()
        {
            return m_VehicleTank.GetMaxEnergyCapacity();
        }

        protected override float GetCurrentEnergyStorage()
        {
            return m_VehicleTank.GetCurrentEnergyStorage();
        }



    }

    internal class Truck : Vehicle
    {
        private readonly bool m_TransportsHazardousMaterials;
        private readonly float m_CargoVolume;
        private readonly float m_MaxAirPressure = 28;
        private readonly int m_MaxWheels = 12;

        public Truck(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels,
            bool i_TransportsHazardousMaterials, float i_CargoVolume, bool i_IsElectric, eGasTypes i_eGasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
         : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_IsElectric, i_eGasType, i_MaxEnergyCapacity, i_CurrentEnergyStorage)
        {
            m_TransportsHazardousMaterials = i_TransportsHazardousMaterials;
            m_CargoVolume = i_CargoVolume;
        }

        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_WheelMaxAirPressure > m_MaxAirPressure)
                {
                    airPressureValid = false;
                    break;
                }
            }
            return airPressureValid;
        }

        protected bool IsAmountOfWheelsValid(Wheel[] i_Wheels)
        {
            return i_Wheels.Length <= m_MaxWheels;
        }

        protected override float GetMaxEnergyCapacity()
        {
            return m_VehicleTank.GetMaxEnergyCapacity();
        }

        protected override float GetCurrentEnergyStorage()
        {
            return m_VehicleTank.GetCurrentEnergyStorage();
        }
    }
}
