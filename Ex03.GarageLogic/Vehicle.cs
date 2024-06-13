using VehicleGarage;
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
        internal readonly string r_LicensePlateID;
        protected float m_EnergyPercentageLeft;
        internal Wheel[] m_Wheels;
        internal EnergyStorage m_VehicleTank;

        protected Vehicle(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, EnergyStorage i_VehicleTank)
        {
            m_ModelName = i_ModelName;
            r_LicensePlateID = i_LicensePlateID;
            m_EnergyPercentageLeft = i_EnergyPercentageLeft;
            m_Wheels = i_Wheels;
            m_VehicleTank = i_VehicleTank;
        }

        protected string ModelName
        {
            get { return m_ModelName; }
        }
        internal string LicensePlateID
        { 
            get { return r_LicensePlateID; }
        }
        protected float EnergyPercentageLeft
        {
            get { return m_EnergyPercentageLeft;}
        }
        internal Wheel[] Wheels
        {
            get { return m_Wheels; }
            set { m_Wheels = value; }
        }
        internal EnergyStorage VehicleTank
        { 
            get { return m_VehicleTank; } 
        }
    }
    internal abstract class EnergyStorage
    {
        private float m_MaxEnergyCapacity;
        private readonly float m_MinEnergyCapacity = 0;
        private float m_CurrentEnergyStorage;

        internal float MaxEnergyCapacity
        {
            set { m_MaxEnergyCapacity = value; }
            get { return m_MaxEnergyCapacity; }
        }
        internal float MinEnergyCapacity
        {
            get { return m_MinEnergyCapacity; }
        }
        internal float CurrentEnergyStorage
        {
            set 
            { 
                if(value > 0 && value <= MaxEnergyCapacity)
                {
                    m_CurrentEnergyStorage = value;
                }
                else
                {
                    throw new ValueOutOfRangeException.ValueOutOfRangeException(MaxEnergyCapacity, MinEnergyCapacity);
                }
            }
            get { return m_CurrentEnergyStorage; }
        }

        public EnergyStorage(float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
        {
            m_MaxEnergyCapacity = i_MaxEnergyCapacity;
            m_CurrentEnergyStorage = i_CurrentEnergyStorage;
        }
        internal abstract void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType);
        internal abstract void Refuel(float i_EnergyPercentageToAdd);

        public bool IsOutOfRange(float i_EnergyPercentageToAdd)
        {
            return i_EnergyPercentageToAdd + m_CurrentEnergyStorage <= m_MaxEnergyCapacity;
        }
    }

    internal class Battery : EnergyStorage
    {
        public Battery(float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
           : base(i_MaxEnergyCapacity, i_CurrentEnergyStorage) { }

        internal override void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();
        }

        internal override void Refuel(float i_EnergyPercentageToAdd)
        {
            if (IsOutOfRange(i_EnergyPercentageToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(MaxEnergyCapacity, MinEnergyCapacity);
            }
            else
            {
                CurrentEnergyStorage += i_EnergyPercentageToAdd;
            }
        }
    }


    internal class FuelTank : EnergyStorage
    {
        eGasTypes m_GasType;
        public FuelTank(eGasTypes i_GasType, float i_MaxEnergyCapacity, float i_CurrentEnergyStorage)
            : base(i_MaxEnergyCapacity, i_CurrentEnergyStorage)
        {
            m_GasType = i_GasType;
        }

        internal override void Refuel(float i_EnergyPercentageToAdd, eGasTypes i_GasType)
        {
            throw new NotImplementedException();
        }

        internal override void Refuel(float i_EnergyPercentageToAdd)
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
        internal float m_MaxAirPressure;
        internal float m_MinAirPressure = 0;
        internal string m_ManufactureName;
        internal float m_AirPressure;

        internal float MaxAirPressure
        {  
            get {  return m_MaxAirPressure; }
        }

        internal float MinAirPressure
        { 
            get { return m_MinAirPressure; } 
        }

        internal string ManufactureName
        {
            get { return m_ManufactureName; }
            set { m_ManufactureName = value; }
        }

        public float AirPressure
        {
            get { return m_AirPressure; }
            set
            {
                if (value >= 0 && value <= MaxAirPressure)
                {
                    m_AirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException.ValueOutOfRangeException(MaxAirPressure, MinAirPressure);
                }
            }
        }

        public Wheel(float i_AirPressure, float i_MaxAirPressure, string i_ManufactureName)
        {
            m_MaxAirPressure = i_MaxAirPressure;
            m_AirPressure = i_AirPressure;
            m_ManufactureName = i_ManufactureName;
        }
        
        public void InflateWheel(float i_AirToAdd)
        {
            if (IsMoreThanMaxAirPressure(i_AirToAdd))
            {
                throw new ValueOutOfRangeException.ValueOutOfRangeException(MaxAirPressure, MinAirPressure);
            }
            else
            {
                AirPressure += i_AirToAdd;
            }
        }

        private bool IsMoreThanMaxAirPressure(float i_AirToAdd)
        {
            return i_AirToAdd + AirPressure <= MaxAirPressure;
        }
        public void InflateToMaximum()
        {
            AirPressure = MaxAirPressure;
        }
    }


    internal class Motorcycle : Vehicle
    {
        private eLicenseType m_LicenseType;
        private int m_EngineVolume;
        int m_MarAirPressure = 33;
        int m_MaxWheels = 2;

        public Motorcycle(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, eLicenseType i_LicenseType, int i_EngineVolume, EnergyStorage i_VehicleTank)
            : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_VehicleTank)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolume = i_EngineVolume;
        }

        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_AirPressure > m_MarAirPressure)
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
    }

    internal class Car : Vehicle
    {
        private readonly eCarColors m_CarColor;
        private readonly int m_NumberOfDoors;
        private readonly float m_MaxAirPressure = 31;
        private readonly int m_MaxWheels = 5;

        public Car(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels, eCarColors i_eCarColor, int i_NumberOfDoors, EnergyStorage i_VehicleTank)
            : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_VehicleTank)
        {
            m_CarColor = i_eCarColor;
            m_NumberOfDoors = i_NumberOfDoors;
        }
        protected bool IsAirPressureValid(Wheel[] i_Wheels)
        {
            bool airPressureValid = true;
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.MaxAirPressure > m_MaxAirPressure)
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
    }

    internal class Truck : Vehicle
    {
        private readonly bool m_TransportsHazardousMaterials;
        private readonly float m_CargoVolume;
        private readonly float m_MaxAirPressure = 28;
        private readonly int m_MaxWheels = 12;

        public Truck(string i_ModelName, string i_LicensePlateID, float i_EnergyPercentageLeft, Wheel[] i_Wheels,
            bool i_TransportsHazardousMaterials, float i_CargoVolume, EnergyStorage i_VehicleTank)
         : base(i_ModelName, i_LicensePlateID, i_EnergyPercentageLeft, i_Wheels, i_VehicleTank)
        {
            m_TransportsHazardousMaterials = i_TransportsHazardousMaterials;
            m_CargoVolume = i_CargoVolume;
        }

        protected void CheckAirPressure(Wheel[] i_Wheels)
        {
            foreach (Wheel wheel in i_Wheels)
            {
                if (wheel.m_MaxAirPressure > m_MaxAirPressure)
                {
                    throw new ValueOutOfRangeException.ValueOutOfRangeException(m_MaxAirPressure, wheel.m_MinAirPressure);
                }
            }
            
        }

        protected bool IsAmountOfWheelsValid(Wheel[] i_Wheels)
        {
            return i_Wheels.Length <= m_MaxWheels;
        }
    }
}
